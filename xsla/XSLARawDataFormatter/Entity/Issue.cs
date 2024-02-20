// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

using Newtonsoft.Json;

namespace XSLARowDataFormatter.Entity;
 
    public class Aggregateprogress
    {
        public int progress { get; set; }
        public int total { get; set; }
    }

    public class Assignee
    {
        public string self { get; set; }
        public string accountId { get; set; }
        public string emailAddress { get; set; }
        public AvatarUrls avatarUrls { get; set; }
        public string displayName { get; set; }
        public bool active { get; set; }
        public string timeZone { get; set; }
        public string accountType { get; set; }
    }

    public class AvatarUrls
    {
        [JsonProperty("48x48")]
        public string _48x48 { get; set; }

        [JsonProperty("24x24")]
        public string _24x24 { get; set; }

        [JsonProperty("16x16")]
        public string _16x16 { get; set; }

        [JsonProperty("32x32")]
        public string _32x32 { get; set; }
    }

    public class Component: IComparable
    {
        public string self { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        
        public int CompareTo(object? obj)
        {
            var second = obj as Component;
            return String.Compare(name, second?.name, StringComparison.Ordinal);
        }
    }

    public class Creator
    {
        public string self { get; set; }
        public string accountId { get; set; }
        public string emailAddress { get; set; }
        public AvatarUrls avatarUrls { get; set; }
        public string displayName { get; set; }
        public bool active { get; set; }
        public string timeZone { get; set; }
        public string accountType { get; set; }
    }

    public class Customfield10018
    {
        public bool hasEpicLinkFieldDependency { get; set; }
        public bool showField { get; set; }
        public NonEditableReason nonEditableReason { get; set; }
    }

    public class Customfield10057
    {
        public string self { get; set; }
        public string value { get; set; }
        public string id { get; set; }
    }

    public class Customfield10061
    {
        public string self { get; set; }
        public string value { get; set; }
        public string id { get; set; }
    }

    public class Customfield10081
    {
        public string self { get; set; }
        public string value { get; set; }
        public string id { get; set; }
    }

    public class Customfield10082
    {
        public string self { get; set; }
        public string value { get; set; }
        public string id { get; set; }
    }

    public class Customfield10121
    {
        public string self { get; set; }
        public string value { get; set; }
        public string id { get; set; }
    }

    public class Customfield10161
    {
        public string self { get; set; }
        public string value { get; set; }
        public string id { get; set; }
    }

    public class Customfield10163
    {
        public string self { get; set; }
        public string value { get; set; }
        public string id { get; set; }
    }

    public class Customfield10272
    {
        public string name { get; set; }
        public string groupId { get; set; }
        public string self { get; set; }
    }

    public class Customfield10276
    {
        public string self { get; set; }
        public string value { get; set; }
        public string id { get; set; }
    }

    public class Customfield10311
    {
        public string self { get; set; }
        public string value { get; set; }
        public string id { get; set; }
    }

    public class Customfield10325
    {
        public string self { get; set; }
        public string accountId { get; set; }
        public string emailAddress { get; set; }
        public AvatarUrls avatarUrls { get; set; }
        public string displayName { get; set; }
        public bool active { get; set; }
        public string timeZone { get; set; }
        public string accountType { get; set; }
    }

    public class Customfield10330
    {
        public string self { get; set; }
        public string value { get; set; }
        public string id { get; set; }
    }

    public class Customfield10347
    {
        public string self { get; set; }
        public string value { get; set; }
        public string id { get; set; }
    }

    public class Customfield10348
    {
        public string self { get; set; }
        public string value { get; set; }
        public string id { get; set; }
    }

    public class Customfield10388
    {
        public string self { get; set; }
        public string value { get; set; }
        public string id { get; set; }
    }

    public class Customfield10413
    {
        public string self { get; set; }
        public string accountId { get; set; }
        public string emailAddress { get; set; }
        public AvatarUrls avatarUrls { get; set; }
        public string displayName { get; set; }
        public bool active { get; set; }
        public string timeZone { get; set; }
        public string accountType { get; set; }
    }

    public class Customfield10417
    {
        public string name { get; set; }
        public string groupId { get; set; }
        public string self { get; set; }
    }

    public class Fields
    {
        public object customfield_10192 { get; set; }
        public object customfield_10071 { get; set; }
        public object customfield_10073 { get; set; }
        public object customfield_10075 { get; set; }
        public object customfield_10078 { get; set; }
        public object resolution { get; set; }
        public object lastViewed { get; set; }
        public object customfield_10180 { get; set; }
        public object customfield_10182 { get; set; }
        public Customfield10061 customfield_10061 { get; set; }
        public object customfield_10184 { get; set; }
        public object customfield_10064 { get; set; }
        public object customfield_10186 { get; set; }
        public object customfield_10065 { get; set; }
        public object customfield_10067 { get; set; }
        public object customfield_10188 { get; set; }
        public object customfield_10068 { get; set; }
        public string customfield_10069 { get; set; }
        public List<object> labels { get; set; }
        public object aggregatetimeoriginalestimate { get; set; }
        public List<object> issuelinks { get; set; }
        public Assignee assignee { get; set; }
        public List<Component> components { get; set; }
        public object customfield_10290 { get; set; }
        public object customfield_10050 { get; set; }
        public object customfield_10051 { get; set; }
        public string customfield_10294 { get; set; }
        public object customfield_10052 { get; set; }
        public object customfield_10296 { get; set; }
        public object customfield_10297 { get; set; }
        public double? customfield_10298 { get; set; }
        public object customfield_10056 { get; set; }
        public Customfield10057 customfield_10057 { get; set; }
        public object customfield_10058 { get; set; }
        public List<object> subtasks { get; set; }
        public Customfield10161 customfield_10161 { get; set; }
        public Customfield10163 customfield_10163 { get; set; }
        public object customfield_10285 { get; set; }
        public Reporter reporter { get; set; }
        public object customfield_10159 { get; set; }
        public Progress progress { get; set; }
        public Votes votes { get; set; }
        public object worklog { get; set; }
        public Issuetype issuetype { get; set; }
        public object customfield_10390 { get; set; }
        public object customfield_10270 { get; set; }
        public object customfield_10391 { get; set; }
        public Customfield10272 customfield_10271 { get; set; }
        public object customfield_10392 { get; set; }
        public Customfield10272 customfield_10272 { get; set; }
        public object customfield_10394 { get; set; }
        public Project project { get; set; }
        public object customfield_10395 { get; set; }
        public object customfield_10275 { get; set; }
        public object customfield_10397 { get; set; }
        public Customfield10276 customfield_10276 { get; set; }
        public object customfield_10398 { get; set; }
        public object resolutiondate { get; set; }
        public Watches watches { get; set; }
        public object customfield_10380 { get; set; }
        public object customfield_10381 { get; set; }
        public object customfield_10260 { get; set; }
        public object customfield_10382 { get; set; }
        public object customfield_10140 { get; set; }
        public object customfield_10020 { get; set; }
        public object customfield_10383 { get; set; }
        public object customfield_10141 { get; set; }
        public object customfield_10262 { get; set; }
        public object customfield_10142 { get; set; }
        public object customfield_10021 { get; set; }
        public object customfield_10384 { get; set; }
        public object customfield_10385 { get; set; }
        public object customfield_10022 { get; set; }
        public object customfield_10023 { get; set; }
        public object customfield_10387 { get; set; }
        public List<Customfield10388> customfield_10388 { get; set; }
        public object customfield_10389 { get; set; }
        public object customfield_10379 { get; set; }
        public object customfield_10016 { get; set; }
        public object customfield_10017 { get; set; }
        public Customfield10018 customfield_10018 { get; set; }
        public string customfield_10019 { get; set; }
        public DateTime updated { get; set; }
        public object timeoriginalestimate { get; set; }
        public object customfield_10250 { get; set; }
        public string description { get; set; }
        public object customfield_10251 { get; set; }
        public object customfield_10010 { get; set; }
        public object customfield_10373 { get; set; }
        public object customfield_10374 { get; set; }
        public object customfield_10253 { get; set; }
        public string customfield_10375 { get; set; }
        public object customfield_10254 { get; set; }
        public object customfield_10376 { get; set; }
        public object customfield_10255 { get; set; }
        public object customfield_10014 { get; set; }
        public List<object> customfield_10377 { get; set; }
        public object customfield_10378 { get; set; }
        public object customfield_10015 { get; set; }
        public object customfield_10257 { get; set; }
        public object timetracking { get; set; }
        public object customfield_10248 { get; set; }
        public string summary { get; set; }
        public string customfield_10000 { get; set; }
        public Customfield10121 customfield_10121 { get; set; }
        public object customfield_10001 { get; set; }
        public object customfield_10002 { get; set; }
        public object customfield_10236 { get; set; }
        public object customfield_10115 { get; set; }
        public object customfield_10237 { get; set; }
        public object customfield_10117 { get; set; }
        public object customfield_10118 { get; set; }
        public object comment { get; set; }
        public DateTime statuscategorychangedate { get; set; }
        public object customfield_10350 { get; set; }
        public object customfield_10351 { get; set; }
        public object customfield_10230 { get; set; }
        public object customfield_10352 { get; set; }
        public List<object> fixVersions { get; set; }
        public object customfield_10231 { get; set; }
        public object customfield_10353 { get; set; }
        public object customfield_10233 { get; set; }
        public object customfield_10234 { get; set; }
        public object customfield_10355 { get; set; }
        public string customfield_10113 { get; set; }
        public string customfield_10114 { get; set; }
        public object customfield_10104 { get; set; }
        public List<Customfield10347> customfield_10347 { get; set; }
        public object customfield_10106 { get; set; }
        public List<Customfield10348> customfield_10348 { get; set; }
        public object customfield_10227 { get; set; }
        public object customfield_10349 { get; set; }
        public object customfield_10340 { get; set; }
        public object customfield_10341 { get; set; }
        public Priority priority { get; set; }
        public object customfield_10342 { get; set; }
        public object customfield_10221 { get; set; }
        public object customfield_10343 { get; set; }
        public object customfield_10222 { get; set; }
        public Customfield10272 customfield_10101 { get; set; }
        public object customfield_10344 { get; set; }
        public object customfield_10102 { get; set; }
        public object customfield_10103 { get; set; }
        public object customfield_10224 { get; set; }
        public object customfield_10335 { get; set; }
        public object customfield_10215 { get; set; }
        public object customfield_10336 { get; set; }
        public object customfield_10337 { get; set; }
        public object customfield_10217 { get; set; }
        public object customfield_10338 { get; set; }
        public object customfield_10339 { get; set; }
        public object timeestimate { get; set; }
        public object customfield_10218 { get; set; }
        public List<Version> versions { get; set; }
        public object customfield_10219 { get; set; }
        public Status status { get; set; }
        public Customfield10330 customfield_10330 { get; set; }
        public object customfield_10331 { get; set; }
        public object customfield_10332 { get; set; }
        public object customfield_10333 { get; set; }
        public object customfield_10334 { get; set; }
        public Customfield10325 customfield_10325 { get; set; }
        public object customfield_10326 { get; set; }
        public object customfield_10327 { get; set; }
        public object aggregatetimeestimate { get; set; }
        public object customfield_10328 { get; set; }
        public Creator creator { get; set; }
        public Aggregateprogress aggregateprogress { get; set; }
        public object customfield_10440 { get; set; }
        public object customfield_10441 { get; set; }
        public object customfield_10313 { get; set; }
        public string customfield_10434 { get; set; }
        public object customfield_10436 { get; set; }
        public object customfield_10315 { get; set; }
        public object customfield_10316 { get; set; }
        public object customfield_10438 { get; set; }
        public object customfield_10439 { get; set; }
        public object timespent { get; set; }
        public object customfield_10430 { get; set; }
        public object aggregatetimespent { get; set; }
        public object customfield_10431 { get; set; }
        public object customfield_10310 { get; set; }
        public Customfield10311 customfield_10311 { get; set; }
        public string customfield_10432 { get; set; }
        public object customfield_10433 { get; set; }
        public object customfield_10427 { get; set; }
        public object customfield_10307 { get; set; }
        public object customfield_10428 { get; set; }
        public object customfield_10429 { get; set; }
        public int workratio { get; set; }
        public object issuerestriction { get; set; }
        public DateTime created { get; set; }
        public object customfield_10422 { get; set; }
        public Customfield10413 customfield_10413 { get; set; }
        public object customfield_10414 { get; set; }
        public object customfield_10415 { get; set; }
        public object customfield_10416 { get; set; }
        public List<Customfield10417> customfield_10417 { get; set; }
        public object customfield_10418 { get; set; }
        public object customfield_10419 { get; set; }
        public object customfield_10097 { get; set; }
        public object customfield_10098 { get; set; }
        public object customfield_10401 { get; set; }
        public object customfield_10402 { get; set; }
        public object security { get; set; }
        public object customfield_10404 { get; set; }
        public object attachment { get; set; }
        public object customfield_10405 { get; set; }
        public object customfield_10406 { get; set; }
        public object customfield_10407 { get; set; }
        public object customfield_10408 { get; set; }
        public Customfield10081 customfield_10081 { get; set; }
        public Customfield10082 customfield_10082 { get; set; }
        public string customfield_10083 { get; set; }
        public object customfield_10086 { get; set; }
        public object customfield_10087 { get; set; }
        public string customfield_10088 { get; set; }
        public object customfield_10400 { get; set; }
    }

    public class Issuetype
    {
        public string self { get; set; }
        public string id { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public bool subtask { get; set; }
        public int avatarId { get; set; }
        public int hierarchyLevel { get; set; }
    }

    public class NonEditableReason
    {
        public string reason { get; set; }
        public string message { get; set; }
    }

    public class Priority
    {
        public string self { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public string id { get; set; }
    }

    public class Progress
    {
        public int progress { get; set; }
        public int total { get; set; }
    }

    public class Project
    {
        public string self { get; set; }
        public string id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string projectTypeKey { get; set; }
        public bool simplified { get; set; }
        public AvatarUrls avatarUrls { get; set; }
        public ProjectCategory projectCategory { get; set; }
    }

    public class ProjectCategory
    {
        public string self { get; set; }
        public string id { get; set; }
        public string description { get; set; }
        public string name { get; set; }
    }

    public class Reporter
    {
        public string self { get; set; }
        public string accountId { get; set; }
        public string emailAddress { get; set; }
        public AvatarUrls avatarUrls { get; set; }
        public string displayName { get; set; }
        public bool active { get; set; }
        public string timeZone { get; set; }
        public string accountType { get; set; }
    }

    public class Issue
    {
        public string expand { get; set; }
        public string id { get; set; }
        public string self { get; set; }
        public string key { get; set; }
        public Fields fields { get; set; }
    }

    public class Status
    {
        public string self { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public StatusCategory statusCategory { get; set; }
    }

    public class StatusCategory
    {
        public string self { get; set; }
        public int id { get; set; }
        public string key { get; set; }
        public string colorName { get; set; }
        public string name { get; set; }
    }

    public class Version
    {
        public string self { get; set; }
        public string id { get; set; }
        public string description { get; set; }
        public string? name { get; set; }
        public bool archived { get; set; }
        public bool released { get; set; }
        public string releaseDate { get; set; }
    }

    public class Votes
    {
        public string self { get; set; }
        public int votes { get; set; }
        public bool hasVoted { get; set; }
    }

    public class Watches
    {
        public string self { get; set; }
        public int watchCount { get; set; }
        public bool isWatching { get; set; }
    }

