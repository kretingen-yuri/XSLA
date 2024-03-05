using System.Diagnostics;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using NLog;
using XSLAPrepareData.Model;
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
    Console.WriteLine("Build CSV file");
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
    logger.Info("Running AI");
    using var reader = new StreamReader("escalation.csv");
    
    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        MissingFieldFound = null
    };
    
    using var csv = new CsvReader(reader, config);
    
    csv.Read();
    csv.ReadHeader();
    csv.Read();
    MLModel1.ModelInput record = csv.GetRecord<MLModel1.ModelInput>();
    RunModel(record);
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
    //logger.Info(result);
}

void RunModel(MLModel1.ModelInput realData)
{
    // Create single instance of sample data from first line of dataset for model input
    MLModel1.ModelInput sampleData = new MLModel1.ModelInput()
    {
        ToPlatform = realData.ToPlatform,
        FromPlatform = realData.FromPlatform,
        CreatedMonth = realData.CreatedMonth,
        DaysSinceRelease = (float)realData.DaysSinceRelease,
        Severity = realData.Severity,
        Priority = realData.Priority,
        Initiative = realData.Initiative,
        ZVMZCAOsType = realData.ZVMZCAOsType,
        Sentimental = realData.Sentimental,
        CustomerName_int = realData.CustomerName_int,
        AffectedVersion_int = realData.AffectedVersion_int,
        Initiative_int = realData.Initiative_int,
        Component_int = realData.Component_int,
        // Install = true,
        // Upgrade = false,
        // Wa = true,
        // ProblematicPlatform = true
    };



    Console.WriteLine("Using model to make single prediction -- Comparing actual NumberOfWorkingDays with predicted NumberOfWorkingDays from sample data...\n\n");


    Console.WriteLine($"ToPlatform: {@"---"}");
    Console.WriteLine($"FromPlatform: {@"Azure"}");
    Console.WriteLine($"CreatedMonth: {2F}");
    Console.WriteLine($"DaysSinceRelease: {4.00491F}");
    Console.WriteLine($"Severity: {@"Normal"}");
    Console.WriteLine($"Priority: {3F}");
    Console.WriteLine($"Initiative: {@"ZVML"}");
    Console.WriteLine($"ZVMZCAOsType: {@"Linux"}");
    Console.WriteLine($"Component: {@"ZVM azure"}");
    // Console.WriteLine($"NumberOfWorkingDays: {2.571286F}");
    Console.WriteLine($"Sentimental: {0F}");
    Console.WriteLine($"CustomerName_int: {1F}");
    Console.WriteLine($"AffectedVersion_int: {1F}");
    Console.WriteLine($"Initiative_int: {1F}");
    Console.WriteLine($"ZVMZCAOsType_int: {1F}");
    Console.WriteLine($"Component_int: {1F}");

    try
    {
        var predictionResult = MLModel1.Predict(sampleData);
        Console.WriteLine($"\n\nPredicted NumberOfWorkingDays: {predictionResult.Score}\n\n");

        Console.WriteLine("=============== End of process, hit any key to finish ===============");
        Console.ReadKey();
    }
    catch (Exception ex)
    {
        logger.Warn(ex, "Error occurred");
        throw;
    }
}