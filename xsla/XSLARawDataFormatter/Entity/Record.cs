using XSLARowDataFormatter.Implementations;

namespace XSLARowDataFormatter.Entity;

public class Record
{
    public string ToPlatform { get; set; }
    public string FromPlatform { get; set; }
    public string CustomerName { get; set; }
    public string? AffectedVersion { get; set; }
    public bool? IsEscalationTeam { get; set; }
    public int CreatedMonth { get; set; }
    public double? DaysSinceRelease { get; set; }
    public double? PingPong { get; set; }
    public string Description { get; set; }
    public string? Severity { get; set; }
    public string Priority { get; set; }
    public string? Initiative { get; set; }
    public OsTypes ZVMZCAOsType { get; set; }
    public string? Component { get; set; }
    public double NumberOfWorkingDays { get; set; }
}