using System.Globalization;
using CsvHelper;
using NLog;
using NLog.Fluent;
using XSLARawDataFormatter;
using XSLARowDataFormatter.Entity;
using XSLARowDataFormatter.Interfaces;

namespace XSLARowDataFormatter.Implementations
{
    public class CsvBuilder : IFileBuilder
    {
        private readonly Logger m_logger = LogManager.GetCurrentClassLogger();
        private readonly IXSLAFormatter<Issue> m_xslaFormatter;
        private readonly string m_outputFilepath;
        
        public CsvBuilder(IXSLAFormatter<Issue> xslaFormatter, string outputFilepath)
        {
            m_xslaFormatter = xslaFormatter;
            m_outputFilepath = outputFilepath;
        }

        public void Build(Dictionary<string,Issue> jiraData)
        {
            m_logger.Info($"Number of records: {jiraData.Count}");
            
            List<Record> listFormattedData = jiraData.Select(j => m_xslaFormatter.Format(j.Value))
                .Where(AllFieldsArePresent).ToList();

            if (listFormattedData.Count == 0)
            {
                m_logger.Warn("Data was rejected(some of required fields were null)");
                return;
            }
            m_logger.Info($"Found valid records:{listFormattedData.Count}");
            
            Save(listFormattedData);
        }

        bool AllFieldsArePresent(Record record)
        {
            return record.GetType().GetProperties().All(propertyInfo => propertyInfo.GetValue(record, null) != null);
        }

        private void Save(List<Record> listFormattedData)
        {
            m_logger.Info($"saving to : {m_outputFilepath}");
            using var writer = new StreamWriter(m_outputFilepath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(listFormattedData);
        }
    }
}