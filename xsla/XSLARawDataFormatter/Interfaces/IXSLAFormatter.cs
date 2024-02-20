using XSLARowDataFormatter.Entity;

namespace XSLARawDataFormatter;

public interface IXSLAFormatter<in T>
{
    Record Format(T rawData);
}