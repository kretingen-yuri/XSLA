using XSLARowDataFormatter.Entity;

namespace XSLARowDataFormatter.Interfaces
{
    public interface IFileBuilder
    {
        void Build(Dictionary<string, Issue> jiraData);
    }
}