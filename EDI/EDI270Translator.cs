namespace PatientEligibilityTranslator.EDI
{
    public class EDI270Translator
    {
        private const string Separator = "~";
        private const string ElementSeparator = "*";
        private const string SegmentTerminator = "~";

        public bool SinglePatientPerRequest { get; set; } = false;
        public int MaxPatientsPerBatch { get; set; } = 50;
        public FileFormatProfile? Profile { get; set; }

        public string Translate(string inputData)
        {
            var patients = ParseMultiplePatients(inputData);
            if (patients.Count == 0)
                return string.Empty;

            if (SinglePatientPerRequest)
            {
                return GenerateSinglePatientBatches(patients);
            }
            else
            {
                return GenerateMultiPatientBatches(patients);
            }
        }

        private string GenerateSinglePatientBatches(List<PatientInfo> patients)
        {
            var sb = new System.Text.StringBuilder();

            foreach (var patient in patients)
            {
                var singlePatientList = new List<PatientInfo> { patient };
                string batch = BuildEDI270Batch(singlePatientList, interchangeNumber: patients.IndexOf(patient) + 1);
                sb.Append(batch);
            }

            return sb.ToString();
        }

        private string GenerateMultiPatientBatches(List<PatientInfo> patients)
        {
            var sb = new System.Text.StringBuilder();
            int batchNumber = 1;

            for (int i = 0; i < patients.Count; i += MaxPatientsPerBatch)
            {
                int remainingCount = Math.Min(MaxPatientsPerBatch, patients.Count - i);
                var batchPatients = patients.GetRange(i, remainingCount);
                string batch = BuildEDI270Batch(batchPatients, interchangeNumber: batchNumber);
                sb.Append(batch);
                batchNumber++;
            }

            return sb.ToString();
        }

        private List<PatientInfo> ParseMultiplePatients(string inputData)
        {
            var patients = new List<PatientInfo>();
            
            // Use profile if available, otherwise use defaults
            string patientDelimiter = Profile?.PatientDelimiter ?? "---PATIENT---";
            
            // If using line delimiter for patients, convert it first
            if (Profile?.UseLineDelimiterForPatients == true)
            {
                patientDelimiter = Profile.LineDelimiter;
            }

            // Unescape the delimiter
            patientDelimiter = UnescapeString(patientDelimiter);

            // Split by patient delimiter or by blank lines
            string[] patientSections = inputData.Split(new[] { patientDelimiter }, StringSplitOptions.None);

            foreach (var section in patientSections)
            {
                var patient = ParsePatientSection(section);
                if (!string.IsNullOrEmpty(patient.MemberId) || !string.IsNullOrEmpty(patient.FirstName))
                {
                    patients.Add(patient);
                }
            }

            return patients;
        }

        private PatientInfo ParsePatientSection(string sectionData)
        {
            var info = new PatientInfo();
            
            // Use profile if available
            string fieldDelimiter = Profile?.FieldDelimiter ?? ":";
            string lineDelimiter = Profile?.LineDelimiter ?? "\\n";
            
            // Unescape delimiters
            fieldDelimiter = UnescapeString(fieldDelimiter);
            lineDelimiter = UnescapeString(lineDelimiter);
            
            var lines = sectionData.Split(new[] { lineDelimiter }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var parts = line.Split(new[] { fieldDelimiter }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 2) continue;

                string key = parts[0].Trim().ToLower();
                string value = parts[1].Trim();

                // Use field mappings from profile if available
                string mappedKey = key;
                if (Profile?.FieldMappings != null && Profile.FieldMappings.ContainsKey(key))
                {
                    mappedKey = Profile.FieldMappings[key];
                }

                switch (mappedKey)
                {
                    case "FirstName":
                        info.FirstName = value;
                        break;
                    case "LastName":
                        info.LastName = value;
                        break;
                    case "DateOfBirth":
                        info.DateOfBirth = value;
                        break;
                    case "MemberId":
                        info.MemberId = value;
                        break;
                    case "GroupId":
                        info.GroupId = value;
                        break;
                    case "PlanHolder":
                        info.PlanHolder = value;
                        break;
                    case "Relationship":
                        info.Relationship = value;
                        break;
                }
            }

            return info;
        }

        private string UnescapeString(string value)
        {
            return value.Replace("\\n", "\n").Replace("\\t", "\t").Replace("\\r", "\r");
        }

        private string BuildEDI270Batch(List<PatientInfo> patients, int interchangeNumber = 1)
        {
            var sb = new System.Text.StringBuilder();
            string timestamp = DateTime.Now.ToString("yyMMdd");
            string timeValue = DateTime.Now.ToString("HHmm");

            // ISA Segment (Interchange Control Header)
            sb.Append($"ISA*00*          *00*          *ZZ*SENDER         *ZZ*RECEIVER       *{timestamp}*{timeValue}*U*00401*{interchangeNumber:D9}*0*P*:" + SegmentTerminator);

            // GS Segment (Functional Group Header)
            sb.Append($"GS*HS*APP SENDER*APP RECEIVER*{timestamp}*{timeValue.Substring(0, 4)}*1*X*004010" + SegmentTerminator);

            int transactionCount = 0;
            int segmentCount = 0;

            foreach (int index in Enumerable.Range(0, patients.Count))
            {
                var patient = patients[index];
                transactionCount++;

                // ST Segment (Transaction Set Header)
                string transactionNum = (index + 1).ToString("D4");
                sb.Append($"ST*270*{transactionNum}*005010X222" + SegmentTerminator);
                segmentCount += 1;

                // BHT Segment (Beginning of Hierarchical Transaction)
                sb.Append($"BHT*0019*13*{transactionNum}*{timestamp}*{timeValue}*CH" + SegmentTerminator);
                segmentCount += 1;

                // HL Segment (Hierarchical Level - Subscriber)
                sb.Append("HL*1**40*1" + SegmentTerminator);
                segmentCount += 1;

                // NM1 Segment (Subscriber Name)
                sb.Append($"NM1*IL*1*{patient.LastName}*{patient.FirstName}****MI*{patient.MemberId}" + SegmentTerminator);
                segmentCount += 1;

                // DMG Segment (Subscriber Demographic Information)
                if (!string.IsNullOrEmpty(patient.DateOfBirth))
                {
                    string dob = CleanDate(patient.DateOfBirth);
                    sb.Append($"DMG*D8*{dob}*M" + SegmentTerminator);
                    segmentCount += 1;
                }

                // NM1 Segment (Responsible Party/Plan Holder)
                if (!string.IsNullOrEmpty(patient.PlanHolder))
                {
                    sb.Append($"NM1*PR*2*{patient.PlanHolder}****HR*{patient.GroupId}" + SegmentTerminator);
                    segmentCount += 1;
                }

                // HL Segment (Hierarchical Level - Dependent/Patient)
                sb.Append("HL*2*1*23*0" + SegmentTerminator);
                segmentCount += 1;

                // NM1 Segment (Patient Name)
                sb.Append($"NM1*IL*1*{patient.LastName}*{patient.FirstName}****MI*{patient.MemberId}" + SegmentTerminator);
                segmentCount += 1;

                // REF Segment (Service Authorization Exception Code)
                sb.Append("REF*0F*11" + SegmentTerminator);
                segmentCount += 1;

                // SE Segment (Transaction Set Trailer)
                int segmentsInTransaction = segmentCount + 1; // +1 for SE itself
                sb.Append($"SE*{segmentsInTransaction}*{transactionNum}" + SegmentTerminator);
                segmentCount = 0;
            }

            // GE Segment (Functional Group Trailer)
            sb.Append($"GE*{transactionCount}*1" + SegmentTerminator);

            // IEA Segment (Interchange Control Trailer)
            sb.Append($"IEA*1*{interchangeNumber:D9}" + SegmentTerminator);

            return sb.ToString();
        }

        private string CleanDate(string date)
        {
            // Convert various date formats to CCYYMMDD format
            date = date.Replace("/", "").Replace("-", "").Replace(" ", "");

            if (DateTime.TryParse(date, out var parsedDate))
            {
                return parsedDate.ToString("yyyyMMdd");
            }

            // If it's already in CCYYMMDD format, return as is
            if (date.Length == 8 && long.TryParse(date, out _))
            {
                return date;
            }

            return DateTime.Now.ToString("yyyyMMdd");
        }
    }

    public class PatientInfo
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DateOfBirth { get; set; } = string.Empty;
        public string MemberId { get; set; } = string.Empty;
        public string GroupId { get; set; } = string.Empty;
        public string PlanHolder { get; set; } = string.Empty;
        public string Relationship { get; set; } = "Self";
    }
}
