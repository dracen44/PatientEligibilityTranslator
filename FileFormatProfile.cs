namespace PatientEligibilityTranslator
{
    public class FileFormatProfile
    {
        [System.Text.Json.Serialization.JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [System.Text.Json.Serialization.JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [System.Text.Json.Serialization.JsonPropertyName("fieldDelimiter")]
        public string FieldDelimiter { get; set; } = ":";

        [System.Text.Json.Serialization.JsonPropertyName("lineDelimiter")]
        public string LineDelimiter { get; set; } = "\\n";

        [System.Text.Json.Serialization.JsonPropertyName("patientDelimiter")]
        public string PatientDelimiter { get; set; } = "---PATIENT---";

        [System.Text.Json.Serialization.JsonPropertyName("fieldMappings")]
        public Dictionary<string, string> FieldMappings { get; set; } = new()
        {
            { "firstname", "FirstName" },
            { "lastname", "LastName" },
            { "dob", "DateOfBirth" },
            { "dateofbirth", "DateOfBirth" },
            { "memberid", "MemberId" },
            { "groupid", "GroupId" },
            { "planholder", "PlanHolder" },
            { "relationship", "Relationship" }
        };

        [System.Text.Json.Serialization.JsonPropertyName("columnIndexMappings")]
        public Dictionary<int, string> ColumnIndexMappings { get; set; } = new();

        [System.Text.Json.Serialization.JsonPropertyName("useLineDelimiterForPatients")]
        public bool UseLineDelimiterForPatients { get; set; } = false;

        [System.Text.Json.Serialization.JsonPropertyName("firstLineIsHeader")]
        public bool FirstLineIsHeader { get; set; } = false;

        public enum FileFormatType
        {
            KeyValue,
            Delimited,
            Csv,
            FixedWidth
        }

        [System.Text.Json.Serialization.JsonPropertyName("formatType")]
        public FileFormatType FormatType { get; set; } = FileFormatType.KeyValue;

        [System.Text.Json.Serialization.JsonPropertyName("isDefault")]
        public bool IsDefault { get; set; } = false;

        public static FileFormatProfile CreateDefault()
        {
            return new FileFormatProfile
            {
                Name = "Default",
                Description = "Default input format (Key: Value pairs)",
                FieldDelimiter = ":",
                LineDelimiter = "\\n",
                PatientDelimiter = "---PATIENT---",
                IsDefault = true,
                FieldMappings = new()
                {
                    { "firstname", "FirstName" },
                    { "lastname", "LastName" },
                    { "dob", "DateOfBirth" },
                    { "dateofbirth", "DateOfBirth" },
                    { "memberid", "MemberId" },
                    { "groupid", "GroupId" },
                    { "planholder", "PlanHolder" },
                    { "relationship", "Relationship" }
                }
            };
        }

        public override string ToString()
        {
            return $"{Name} - {Description}";
        }
    }
}
