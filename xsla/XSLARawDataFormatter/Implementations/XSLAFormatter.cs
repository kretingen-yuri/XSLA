using System.Text.RegularExpressions;
using NLog;
using XSLARawDataFormatter;
using XSLARowDataFormatter.Entity;
using Version = XSLARowDataFormatter.Entity.Version;

namespace XSLARowDataFormatter.Implementations
{
    public enum OsTypes
    {
        NotDefined,
        Windows,
        Linux,
        Both
    };
    public class XSLAFormatter: IXSLAFormatter<Issue>
    {
        private readonly Logger m_logger = LogManager.GetCurrentClassLogger();
        private readonly IKnownVersions m_knownVersions;

        public XSLAFormatter(IKnownVersions knownVersions)
        {
            m_knownVersions = knownVersions;
        }

        public Record Format(Issue rawData)
        {
            try
            {
                string? knownVersion = null;
                Version version = rawData.fields.versions.FirstOrDefault();
                if (version != null)
                {
                    knownVersion = version.name;
                }

                var baseTeam = rawData.fields.customfield_10272 != null
                    ? rawData.fields.customfield_10272.name
                    : rawData.fields.customfield_10101 != null
                        ? rawData.fields.customfield_10101.name
                        : "none";
                List<Component>? sortedComponents = rawData.fields.components;

                sortedComponents?.Sort();

                string? knownComponents = null;
                if (sortedComponents != null)
                {
                    knownComponents = string.Join(";", sortedComponents.Select(c=>c.name));
                }


                return new Record
                {
                    ToPlatform = rawData.fields.customfield_10347 != null
                        ? rawData.fields.customfield_10347.First().value
                        : null,
                    FromPlatform = rawData.fields.customfield_10348 != null
                        ? rawData.fields.customfield_10348.First().value
                        : null,
                    CustomerName = GetValue(rawData.fields, "customer"),
                    Description = rawData.fields.description,
                    AffectedVersion = knownVersion,
                    IsEscalationTeam = baseTeam != null
                        ? baseTeam.Equals("Team - Eng - Dev Escalation")
                        : null,
                    CreatedMonth = rawData.fields.created.Month,
                    DaysSinceRelease = !string.IsNullOrEmpty(knownVersion)
                        ? m_knownVersions.NumberOfDaysSinceRelease(rawData.fields.created, knownVersion)
                        : 0,
                    PingPong = rawData.fields.customfield_10436 != null
                        ? double.Parse(rawData.fields.customfield_10436.ToString())
                        : -1,
                    Severity = rawData.fields.customfield_10082?.value,
                    Priority = rawData.fields.priority.name,
                    Initiative = rawData.fields.customfield_10276?.value,
                    ZVMZCAOsType = GetOsType(rawData.fields.customfield_10388),
                    Component = knownComponents,
                    NumberOfWorkingDays = (rawData.fields.updated - rawData.fields.created).TotalDays
                };
            }
            catch (Exception e)
            {
                m_logger.Warn(e, "Error occurred");
                throw;
            }
        }

        private OsTypes GetOsType(List<Customfield10388>? osTypes)
        {
            var ostypes = new List<string> { "windows", "linux" };

            if (osTypes == null)
            {
                return OsTypes.NotDefined;
            }

            if (osTypes.Any(p => p.value.Equals(ostypes[0], StringComparison.OrdinalIgnoreCase)))
            {
                return OsTypes.Windows;
            }

            if (osTypes.Any(p => p.value.Equals(ostypes[1], StringComparison.OrdinalIgnoreCase)))
            {
                return OsTypes.Linux;
            }

            if (osTypes.Any(p => p.value.Equals(ostypes[1], StringComparison.OrdinalIgnoreCase)) &&
                osTypes.Any(p => p.value.Equals(ostypes[1], StringComparison.OrdinalIgnoreCase)))
            {
                return OsTypes.Both;
            }

            return OsTypes.NotDefined;
        }

        private string GetValue(Fields rawDataFields, string whatNeeded)
        {
            switch (whatNeeded)
            {
                case "customer":
                {
                    var m = Regex.Match(rawDataFields.summary, @"(?<=\().+?(?=\))|(?<=\[).+?(?=\])");
                    if (m.Success)
                    {
                        return m.Value;
                    }

                    return string.Empty;
                }
            }

            return string.Empty;
        }
    }
}