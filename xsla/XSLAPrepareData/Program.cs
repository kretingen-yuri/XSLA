using Newtonsoft.Json;
using NLog;
using XSLARawDataFormatter;
using XSLARowDataFormatter.Entity;
using XSLARowDataFormatter.Implementations;


Logger logger = LogManager.GetCurrentClassLogger();

var knownVersion = new KnownVersions(Path.Combine("/Users/yuri.kretingen/Downloads", "versions.json"));
var output = Path.Combine("/Users/yuri.kretingen/Downloads", "result.csv");

IFileBuilder builder = new CsvBuilder(new XSLAFormatter(knownVersion), output);

var inputFileName = Path.Combine("/Users/yuri.kretingen/Downloads","DevEscalationIssues.json");
var text = File.ReadAllText(inputFileName);
var mydictionary = JsonConvert.DeserializeObject<Dictionary<string, Issue>>(text);

if (mydictionary != null)
{
    try
    {
        builder.Build(mydictionary);
    }
    catch (Exception e)
    {
        logger.Error(e,"error occurred");
    }
}
