using System.Collections.Concurrent;
using System.Net;
using Newtonsoft.Json;
using NLog;
using XSLARowDataFormatter.Entity;

namespace XSLAClient
{

    public class XSLAClient
    {
        private readonly Logger m_logger = LogManager.GetCurrentClassLogger();
        private readonly string m_jiraUrl;

        public XSLAClient(string jiraUrl)
        {
            m_jiraUrl = jiraUrl;
        }

        public async Task<Issue> RetrieveEscalationAsync(string escalation)
        {
            try
            {
                using HttpClient client = new();

                var request = new HttpRequestMessage(HttpMethod.Get, $"{m_jiraUrl}/rest/api/2/issue/{escalation}");
                request.Headers.Add("Authorization",
                    "Basic eXVyaS5rcmV0aW5nZW5AemVydG8uY29tOkFUQVRUM3hGZkdGMFBmVTNVR0VvNEhWMjRCOThaVVJXQkd2eDZKamI0UzU5S2p5Q2pqQWlfby04SDN5TEk1QV9jYXo5d1FXeEhMR3E2TldIeVdtQmcxOEhIV253akxLaVUtNDA0WDYwVi1PQ1FodUJVMS1ueVVJbXo2MjdnLVhBQTcxWGpBV0FOa211Z3JUaHF6SVdMX3VQS0h1QWVrX3ZkbGdJN2huc21RWjhjaGdTSXhyNk1VOD04RkI1ODlBNw==");

                var dataStream = await client.SendAsync(request);
                using StreamReader reader = new StreamReader(await dataStream.Content.ReadAsStreamAsync());
                var json = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<Issue>(json);
            }
            catch (Exception ex)
            {
                m_logger.Error(ex, "Error occurred");
                throw;
            }
        }
        
        public async Task<Dictionary<string, Issue>> RetrieveClosedEscalationsAsync()
        {
            try
            {
                var startAt = 0;
                ConcurrentDictionary<string, Issue> allEscalations = new();

                var issues = await GetJiraIssues(startAt, allEscalations);

                var listSteps = new ConcurrentBag<int>();
                for (int i = issues.maxResults; i < issues.total; i += issues.maxResults)
                {
                    listSteps.Add(i);
                }

                var source = new CancellationTokenSource();
                source.CancelAfter(TimeSpan.FromSeconds(40));
                await Parallel.ForEachAsync(listSteps,source.Token,  async (index, token) =>
                {
                    try
                    {
                        await GetJiraIssues(index, allEscalations, token);
                    }
                    catch (TimeoutException t)
                    {
                        m_logger.Warn(t, "Error occurred");
                    }
                    catch (Exception ex)
                    {
                        m_logger.Warn(ex, "Error occurred");
                    }
                });

                return allEscalations.ToDictionary(i=>i.Key,v=>v.Value);
            }
            catch (Exception e)
            {
                m_logger.Error(e, "error occurred");
                throw;
            }
        }

        private async Task<IssuesList> GetJiraIssues(int startAt, ConcurrentDictionary<string, Issue> allEscalations, CancellationToken token=default)
        {
            using HttpClient client = new();

            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{m_jiraUrl}/rest/api/latest/search?jql=project= Zerto AND issuetype = DevEscalation order by created DESC&maxResults=100&startAt={startAt}");
            request.Headers.Add("Authorization",
                "Basic eXVyaS5rcmV0aW5nZW5AemVydG8uY29tOkFUQVRUM3hGZkdGMFBmVTNVR0VvNEhWMjRCOThaVVJXQkd2eDZKamI0UzU5S2p5Q2pqQWlfby04SDN5TEk1QV9jYXo5d1FXeEhMR3E2TldIeVdtQmcxOEhIV253akxLaVUtNDA0WDYwVi1PQ1FodUJVMS1ueVVJbXo2MjdnLVhBQTcxWGpBV0FOa211Z3JUaHF6SVdMX3VQS0h1QWVrX3ZkbGdJN2huc21RWjhjaGdTSXhyNk1VOD04RkI1ODlBNw==");

            var dataStream = await client.SendAsync(request);
            using StreamReader reader = new StreamReader(await dataStream.Content.ReadAsStreamAsync());
            var json = await reader.ReadToEndAsync();
            IssuesList issues = JsonConvert.DeserializeObject<IssuesList>(json);

            foreach (var issue in issues.issues)
            {
                if (token != default)
                {
                    token.ThrowIfCancellationRequested();
                }
                allEscalations[issue.key] = issue;
            }

            return issues;
        }
    }
}