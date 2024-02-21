using Newtonsoft.Json;
using XSLARowDataFormatter.Entity;

namespace XSLARowDataFormatter.Implementations
{

    public interface IKnownVersions
    {
        double NumberOfDaysSinceRelease(DateTime created, string? version);
    }

    public class KnownVersions : IKnownVersions
    {
        private readonly string m_versionFile;
        private Lazy<Root> m_knownVersions;

        public KnownVersions(string versionFile)
        {
            m_versionFile = versionFile;
            m_knownVersions = new Lazy<Root>(Load);
        }

        public double NumberOfDaysSinceRelease(DateTime created, string? version)
        {
            var found = m_knownVersions.Value.versions.allowedValues.FirstOrDefault(v => v.name.Equals(version));
            if (found != null)
            {
                DateTime.TryParse((ReadOnlySpan<char>)found.releaseDate, out var releaseDay);
                return (created - releaseDay).TotalDays;
            }

            return 0;
        }

        private Root Load()
        {
            var text = File.ReadAllText(m_versionFile);
            return JsonConvert.DeserializeObject<Root>(text);
        }

    }
}