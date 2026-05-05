namespace PatientEligibilityTranslator
{
    public class AppSettings
    {
        [System.Text.Json.Serialization.JsonPropertyName("singlePatientPerRequest")]
        public bool SinglePatientPerRequest { get; set; } = false;

        [System.Text.Json.Serialization.JsonPropertyName("maxPatientsPerBatch")]
        public int MaxPatientsPerBatch { get; set; } = 50;

        [System.Text.Json.Serialization.JsonPropertyName("profiles")]
        public List<FileFormatProfile> Profiles { get; set; } = new();

        [System.Text.Json.Serialization.JsonPropertyName("activeProfileName")]
        public string ActiveProfileName { get; set; } = "Default";

        private static readonly string SettingsFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "PatientEligibilityTranslator",
            "settings.json");

        public static AppSettings Load()
        {
            try
            {
                if (File.Exists(SettingsFilePath))
                {
                    string json = File.ReadAllText(SettingsFilePath);
                    var settings = System.Text.Json.JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
                    
                    // Ensure default profile exists
                    if (!settings.Profiles.Any(p => p.IsDefault))
                    {
                        settings.Profiles.Insert(0, FileFormatProfile.CreateDefault());
                    }
                    
                    return settings;
                }
            }
            catch
            {
                // If loading fails, return default settings
            }

            var defaultSettings = new AppSettings();
            defaultSettings.Profiles.Add(FileFormatProfile.CreateDefault());
            return defaultSettings;
        }

        public void Save()
        {
            try
            {
                var directory = Path.GetDirectoryName(SettingsFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Ensure default profile is always present
                if (!Profiles.Any(p => p.IsDefault))
                {
                    Profiles.Insert(0, FileFormatProfile.CreateDefault());
                }

                string json = System.Text.Json.JsonSerializer.Serialize(this, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(SettingsFilePath, json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to save settings: {ex.Message}");
            }
        }

        public FileFormatProfile? GetActiveProfile()
        {
            return Profiles.FirstOrDefault(p => p.Name == ActiveProfileName) ?? Profiles.FirstOrDefault(p => p.IsDefault);
        }

        public void AddProfile(FileFormatProfile profile)
        {
            if (!Profiles.Any(p => p.Name == profile.Name))
            {
                Profiles.Add(profile);
            }
        }

        public void RemoveProfile(string profileName)
        {
            if (profileName != "Default")
            {
                Profiles.RemoveAll(p => p.Name == profileName);
                if (ActiveProfileName == profileName)
                {
                    ActiveProfileName = "Default";
                }
            }
        }
    }
}
