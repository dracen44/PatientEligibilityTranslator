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

            // CSV mode: header row defines columns, each subsequent line is one patient
            if (Profile?.FirstLineIsHeader == true)
            {
                return ParseCsvPatients(inputData);
            }

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

        private List<PatientInfo> ParseCsvPatients(string inputData)
        {
            var patients = new List<PatientInfo>();
            string fieldDelimiter = UnescapeString(Profile?.FieldDelimiter ?? ",");
            string lineDelimiter = UnescapeString(Profile?.LineDelimiter ?? "\\n");

            var lines = inputData.Split(new[] { lineDelimiter }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length < 2)
                return patients;

            // Build column index → property name map from header row
            var headerFields = lines[0].Split(new[] { fieldDelimiter }, StringSplitOptions.None);
            var columnMappings = new Dictionary<int, string>();
            for (int i = 0; i < headerFields.Length; i++)
            {
                string headerFieldName = headerFields[i].Trim().ToLower();
                columnMappings[i] = Profile?.FieldMappings != null && Profile.FieldMappings.ContainsKey(headerFieldName)
                    ? Profile.FieldMappings[headerFieldName]
                    : headerFieldName;
            }

            // One patient per data row
            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;

                var info = new PatientInfo();
                var fields = lines[i].Split(new[] { fieldDelimiter }, StringSplitOptions.None);
                for (int colIndex = 0; colIndex < fields.Length; colIndex++)
                {
                    if (columnMappings.ContainsKey(colIndex))
                        SetPatientProperty(ref info, columnMappings[colIndex], fields[colIndex].Trim());
                }

                if (!string.IsNullOrEmpty(info.MemberId) || !string.IsNullOrEmpty(info.FirstName))
                    patients.Add(info);
            }

            return patients;
        }

        private PatientInfo ParsePatientSection(string sectionData)
        {
            var info = new PatientInfo();
            
            // Use profile if available
            string fieldDelimiter = Profile?.FieldDelimiter ?? ":";
            string lineDelimiter = Profile?.LineDelimiter ?? "\\n";
            bool firstLineIsHeader = Profile?.FirstLineIsHeader ?? false;
            
            // Unescape delimiters
            fieldDelimiter = UnescapeString(fieldDelimiter);
            lineDelimiter = UnescapeString(lineDelimiter);
            
            var lines = sectionData.Split(new[] { lineDelimiter }, StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length == 0)
                return info;

            // If we have a header line, parse it to get column names
            Dictionary<int, string> columnMappings = new Dictionary<int, string>();
            int startLineIndex = 0;

            if (firstLineIsHeader)
            {
                var headerLine = lines[0];
                var headerFields = headerLine.Split(new[] { fieldDelimiter }, StringSplitOptions.None);
                
                // Create mapping from column index to field name
                for (int i = 0; i < headerFields.Length; i++)
                {
                    string headerFieldName = headerFields[i].Trim().ToLower();
                    
                    // Check if this header field has a mapping in the profile
                    if (Profile?.FieldMappings != null && Profile.FieldMappings.ContainsKey(headerFieldName))
                    {
                        columnMappings[i] = Profile.FieldMappings[headerFieldName];
                    }
                    else
                    {
                        // Use the header field name directly as the property name
                        columnMappings[i] = headerFieldName;
                    }
                }
                
                startLineIndex = 1;
            }

            // Parse data lines
            for (int i = startLineIndex; i < lines.Length; i++)
            {
                var line = lines[i];
                if (string.IsNullOrWhiteSpace(line)) continue;
                
                var fields = line.Split(new[] { fieldDelimiter }, StringSplitOptions.None);

                // If we have column mappings (CSV with header), use them
                if (columnMappings.Count > 0)
                {
                    for (int colIndex = 0; colIndex < fields.Length; colIndex++)
                    {
                        if (!columnMappings.ContainsKey(colIndex))
                            continue;

                        string propertyName = columnMappings[colIndex];
                        string value = fields[colIndex].Trim();

                        SetPatientProperty(ref info, propertyName, value);
                    }
                }
                else
                {
                    // Key-value format: each field is "key:value"
                    var parts = fields;
                    if (parts.Length < 2) continue;

                    string key = parts[0].Trim().ToLower();
                    string value = parts[1].Trim();

                    // Use field mappings from profile if available
                    string mappedKey = key;
                    if (Profile?.FieldMappings != null && Profile.FieldMappings.ContainsKey(key))
                    {
                        mappedKey = Profile.FieldMappings[key];
                    }

                    SetPatientProperty(ref info, mappedKey, value);
                }
            }

            return info;
        }

        private void SetPatientProperty(ref PatientInfo info, string propertyName, string value)
        {
            switch (propertyName)
            {
                case "FirstName":
                case "firstname":
                    info.FirstName = value;
                    break;
                case "LastName":
                case "lastname":
                    info.LastName = value;
                    break;
                case "DateOfBirth":
                case "dateofbirth":
                    info.DateOfBirth = value;
                    break;
                case "MemberId":
                case "memberid":
                    info.MemberId = value;
                    break;
                case "GroupId":
                case "groupid":
                    info.GroupId = value;
                    break;
                case "PlanHolder":
                case "planholder":
                    info.PlanHolder = value;
                    break;
                case "Relationship":
                case "relationship":
                    info.Relationship = value;
                    break;
            }
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
