namespace PatientEligibilityTranslator.EDI
{
    public class EDI271Translator
    {
        public string Translate(string edi271Content)
        {
            var segments = ParseEDI271(edi271Content);
            return FormatEligibilityResponse(segments);
        }

        private Dictionary<string, List<Dictionary<string, string>>> ParseEDI271(string edi271Content)
        {
            var segments = new Dictionary<string, List<Dictionary<string, string>>>();

            // Split by segment terminator (~)
            string[] segmentLines = edi271Content.Split('~');

            foreach (var segment in segmentLines)
            {
                var trimmedSegment = segment.Trim();
                if (string.IsNullOrWhiteSpace(trimmedSegment)) continue;

                string[] parts = trimmedSegment.Split('*');
                if (parts.Length == 0) continue;

                string segmentId = parts[0];

                if (!segments.ContainsKey(segmentId))
                {
                    segments[segmentId] = new List<Dictionary<string, string>>();
                }

                var segmentData = new Dictionary<string, string>();
                for (int i = 1; i < parts.Length; i++)
                {
                    segmentData[$"Field{i}"] = parts[i];
                }

                segments[segmentId].Add(segmentData);
            }

            return segments;
        }

        private string FormatEligibilityResponse(Dictionary<string, List<Dictionary<string, string>>> segments)
        {
            var sb = new System.Text.StringBuilder();

            sb.AppendLine("=== EDI 271 ELIGIBILITY RESPONSE ===");
            sb.AppendLine();

            // Extract ISA information
            if (segments.ContainsKey("ISA") && segments["ISA"].Count > 0)
            {
                sb.AppendLine("--- INTERCHANGE HEADER ---");
                var isaData = segments["ISA"][0];
                sb.AppendLine($"Interchange Sender: {isaData.GetValueOrDefault("Field8", "N/A")}");
                sb.AppendLine($"Interchange Receiver: {isaData.GetValueOrDefault("Field10", "N/A")}");
                sb.AppendLine($"Interchange Date: {isaData.GetValueOrDefault("Field9", "N/A")}");
                sb.AppendLine($"Interchange Time: {isaData.GetValueOrDefault("Field10", "N/A")}");
                sb.AppendLine();
            }

            // Extract GS information
            if (segments.ContainsKey("GS") && segments["GS"].Count > 0)
            {
                sb.AppendLine("--- FUNCTIONAL GROUP ---");
                var gsData = segments["GS"][0];
                sb.AppendLine($"Application Sender: {gsData.GetValueOrDefault("Field2", "N/A")}");
                sb.AppendLine($"Application Receiver: {gsData.GetValueOrDefault("Field3", "N/A")}");
                sb.AppendLine();
            }

            // Extract NM1 segments (names)
            if (segments.ContainsKey("NM1"))
            {
                sb.AppendLine("--- PATIENT/MEMBER INFORMATION ---");
                foreach (var nm1Data in segments["NM1"])
                {
                    string entityType = nm1Data.GetValueOrDefault("Field1", "Unknown");
                    string lastName = nm1Data.GetValueOrDefault("Field3", "");
                    string firstName = nm1Data.GetValueOrDefault("Field4", "");
                    string middleName = nm1Data.GetValueOrDefault("Field5", "");
                    string id = nm1Data.GetValueOrDefault("Field9", "");

                    sb.AppendLine($"Entity Type: {GetEntityTypeDescription(entityType)}");
                    sb.AppendLine($"Name: {firstName} {middleName} {lastName}".Trim());
                    if (!string.IsNullOrEmpty(id))
                    {
                        sb.AppendLine($"Member ID: {id}");
                    }
                    sb.AppendLine();
                }
            }

            // Extract DMG segments (demographics)
            if (segments.ContainsKey("DMG"))
            {
                sb.AppendLine("--- DEMOGRAPHICS ---");
                foreach (var dmgData in segments["DMG"])
                {
                    string dateFormat = dmgData.GetValueOrDefault("Field1", "D8");
                    string dateOfBirth = dmgData.GetValueOrDefault("Field2", "");
                    string gender = dmgData.GetValueOrDefault("Field3", "");

                    if (!string.IsNullOrEmpty(dateOfBirth))
                    {
                        sb.AppendLine($"Date of Birth: {FormatDate(dateOfBirth)}");
                    }
                    if (!string.IsNullOrEmpty(gender))
                    {
                        sb.AppendLine($"Gender: {GetGenderDescription(gender)}");
                    }
                }
                sb.AppendLine();
            }

            // Extract EB segments (eligibility or benefit information)
            if (segments.ContainsKey("EB"))
            {
                sb.AppendLine("--- ELIGIBILITY/BENEFIT INFORMATION ---");
                foreach (var ebData in segments["EB"])
                {
                    string eligibilityStatus = ebData.GetValueOrDefault("Field1", "");
                    string coverageType = ebData.GetValueOrDefault("Field2", "");
                    string serviceType = ebData.GetValueOrDefault("Field3", "");

                    if (!string.IsNullOrEmpty(eligibilityStatus))
                    {
                        sb.AppendLine($"Eligibility Status: {GetEligibilityStatus(eligibilityStatus)}");
                    }
                    if (!string.IsNullOrEmpty(coverageType))
                    {
                        sb.AppendLine($"Coverage Type: {coverageType}");
                    }
                    if (!string.IsNullOrEmpty(serviceType))
                    {
                        sb.AppendLine($"Service Type: {GetServiceType(serviceType)}");
                    }
                }
                sb.AppendLine();
            }

            // Extract AAA segments (request validation)
            if (segments.ContainsKey("AAA"))
            {
                sb.AppendLine("--- VALIDATION/STATUS ---");
                foreach (var aaaData in segments["AAA"])
                {
                    string validationStatus = aaaData.GetValueOrDefault("Field1", "");
                    if (!string.IsNullOrEmpty(validationStatus))
                    {
                        sb.AppendLine($"Validation Result: {validationStatus}");
                    }
                }
                sb.AppendLine();
            }

            // Extract MSG segments (messages)
            if (segments.ContainsKey("MSG"))
            {
                sb.AppendLine("--- MESSAGES ---");
                foreach (var msgData in segments["MSG"])
                {
                    string message = msgData.GetValueOrDefault("Field1", "");
                    if (!string.IsNullOrEmpty(message))
                    {
                        sb.AppendLine($"Message: {message}");
                    }
                }
                sb.AppendLine();
            }

            sb.AppendLine("=== END OF RESPONSE ===");

            return sb.ToString();
        }

        private string GetEntityTypeDescription(string code)
        {
            return code switch
            {
                "IL" => "Insured/Member",
                "PR" => "Payer",
                "PT" => "Patient",
                "D8" => "Dependent",
                _ => code
            };
        }

        private string GetGenderDescription(string code)
        {
            return code switch
            {
                "M" => "Male",
                "F" => "Female",
                "U" => "Unknown",
                _ => code
            };
        }

        private string GetEligibilityStatus(string code)
        {
            return code switch
            {
                "A" => "Active",
                "I" => "Inactive",
                "C" => "Conditional",
                "P" => "Pending",
                _ => code
            };
        }

        private string GetServiceType(string code)
        {
            return code switch
            {
                "01" => "Medical Care",
                "04" => "Dental Care",
                "05" => "Dental - Preventive",
                "06" => "Dental - Basic",
                "07" => "Dental - Major",
                "11" => "Chiropractic Care",
                "47" => "Pharmacy",
                "62" => "Mail Order Pharmacy",
                _ => code
            };
        }

        private string FormatDate(string date)
        {
            // Input format: CCYYMMDD or MMDDYY
            if (date.Length == 8 && long.TryParse(date, out _))
            {
                try
                {
                    // Try CCYYMMDD format
                    int year = int.Parse(date.Substring(0, 4));
                    int month = int.Parse(date.Substring(4, 2));
                    int day = int.Parse(date.Substring(6, 2));
                    return new DateTime(year, month, day).ToString("MM/dd/yyyy");
                }
                catch
                {
                    return date;
                }
            }
            return date;
        }
    }
}
