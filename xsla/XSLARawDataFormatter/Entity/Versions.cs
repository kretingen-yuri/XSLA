namespace XSLARowDataFormatter.Entity;

public class AllowedValue
{
    public string self { get; set; }
    public string id { get; set; }
    public string description { get; set; }
    public string name { get; set; }
    public bool archived { get; set; }
    public bool released { get; set; }
    public string startDate { get; set; }
    public string releaseDate { get; set; }
    public bool overdue { get; set; }
    public string userStartDate { get; set; }
    public string userReleaseDate { get; set; }
    public int projectId { get; set; }
}

public class Root
{
    public Versions versions { get; set; }
}

public class Schema
{
    public string type { get; set; }
    public string items { get; set; }
    public string system { get; set; }
}

public class Versions
{
    public bool required { get; set; }
    public Schema schema { get; set; }
    public string name { get; set; }
    public string key { get; set; }
    public List<string> operations { get; set; }
    public List<AllowedValue> allowedValues { get; set; }
}