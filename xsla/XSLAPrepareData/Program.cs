using System.Diagnostics;
using Newtonsoft.Json;
using NLog;
using NLog.Fluent;
using XSLARowDataFormatter.Entity;
using XSLARowDataFormatter.Implementations;
using XSLARowDataFormatter.Interfaces;


Logger logger = LogManager.GetCurrentClassLogger();

var knownVersion = new KnownVersions(Path.Combine("/Users/yuri.kretingen/Downloads", "versions.json"));
var output = Path.Combine(Environment.CurrentDirectory, "result.csv");

IFileBuilder builder = new CsvBuilder(new XSLAFormatter(knownVersion), output);

var inputFileName = Path.Combine("/Users/yuri.kretingen/Downloads","DevEscalationIssues.json");

logger.Info("Get closed escalations");
XSLAClient.XSLAClient client = new("https://zerto.atlassian.net");

// var text = File.ReadAllText(inputFileName);
// var mapEscalationsToJiraIssues = JsonConvert.DeserializeObject<Dictionary<string, Issue>>(text);
var mapEscalationsToJiraIssues = await client.RetrieveClosedEscalationsAsync();

try
{
    logger.Info("Build CSV file");
    builder.Build(mapEscalationsToJiraIssues);
    logger.Info("Done");
    logger.Info("Run fixer");
    Fixer(output, "allFixedData.csv", false);
    logger.Info("Done");
    Console.WriteLine("Enter a new escalation jira number:");
    var escalation = Console.ReadLine();
    var rawData = await client.RetrieveEscalationAsync(escalation);
    builder.Build(new Dictionary<string, Issue> { { escalation, rawData } });
    Fixer(output, "escalation.csv", false);
}
catch (Exception e)
{
    logger.Error(e, "error occurred");
}


void Fixer(string inputFilename, string outputFilename, bool showPlot)
{
    var showplot = showPlot ? "true" : "false";
    ProcessStartInfo start = new ProcessStartInfo();
    start.FileName = "python3";
    start.Arguments = $"python_script/csvmodifider.py {inputFilename} {outputFilename} {showplot}";
    start.UseShellExecute = false;
    start.WorkingDirectory = "";
    start.RedirectStandardOutput = true;
    start.CreateNoWindow = false;
    using Process process = Process.Start(start);
    using StreamReader reader = process.StandardOutput;
    string result = reader.ReadToEnd();
    logger.Info(result);
}