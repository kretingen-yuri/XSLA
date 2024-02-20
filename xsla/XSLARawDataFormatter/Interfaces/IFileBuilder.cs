using XSLARowDataFormatter.Entity;

namespace XSLARawDataFormatter;

public interface IFileBuilder
{
    void Build(Dictionary<string,Issue> jiraData);
}