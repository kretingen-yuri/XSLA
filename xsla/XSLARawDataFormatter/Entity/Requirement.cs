namespace XSLARowDataFormatter.Entity;

public class Requirement
{
    public string Name { get; set; }
}

public class Formula
{
    public List<string> Included { get; set; }
    public List<string> Except { get; set; }
}

public class CustomRequirement:Requirement
{
    public string VisibleName { get; set; }
    public Formula Formula { get; set; }
}