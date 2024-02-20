using System.Collections.Concurrent;
using System.Globalization;
using System.Text.Json;
using CsvHelper;
using XSLARawDataFormatter;
using XSLARowDataFormatter.Entity;

namespace XSLARowDataFormatter.Implementations
{
    public class CsvBuilder : IFileBuilder
    {
        private readonly IXSLAFormatter<Issue> m_xslaFormatter;
        private readonly string m_outputFilepath;
        
        public CsvBuilder(IXSLAFormatter<Issue> xslaFormatter, string outputFilepath)
        {
            m_xslaFormatter = xslaFormatter;
            m_outputFilepath = outputFilepath;
        }

        public void Build(Dictionary<string,Issue> jiraData)
        {
            List<Record> listFormattedData = jiraData.Select(j => m_xslaFormatter.Format(j.Value)).ToList();

            Save(listFormattedData);
        }

        private void Save(List<Record> listFormattedData)
        {
            using var writer = new StreamWriter(m_outputFilepath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(listFormattedData);
        }
    }
}