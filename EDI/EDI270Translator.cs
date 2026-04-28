namespace PatientEligibilityTranslator.EDI
{
    public class EDI270Translator
    {
        private const string Separator = "~";
        private const string ElementSeparator = "*";
        private const string SegmentTerminator = "~";

        public string Translate(string inputData)
        {
            var patientInfo = ParseInput(inputData);
            return BuildEDI270(patientInfo);
        }

        private PatientInfo ParseInput(string inputData)
        {
            var lines = inputData.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var info = new PatientInfo();

            foreach (var line in lines)
            {
                var parts = line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 2) continue;

                string key = parts[0].Trim().ToLower();
                string value = parts[1].Trim();

                switch (key)
                {
                    case "firstname":
                        info.FirstName = value;
                        break;
                    case "lastname":
                        info.LastName = value;
                        break;
                    case "dob":
                    case "dateofbirth":
                        info.DateOfBirth = value;
                        break;
                    case "memberid":
                        info.MemberId = value;
                        break;
                    case "groupid":
                        info.GroupId = value;
                        break;
                    case "planholder":
                        info.PlanHolder = value;
                        break;
                    case "relationship":
                        info.Relationship = value;
                        break;
                }
            }

            return info;
        }

        private string BuildEDI270(PatientInfo info)
        {
            var sb = new System.Text.StringBuilder();

            // ISA Segment (Interchange Control Header)
            sb.Append("ISA*00*          *00*          *ZZ*SENDER         *ZZ*RECEIVER       *200101*0000*U*00401*000000001*0*P*:" + SegmentTerminator);

            // GS Segment (Functional Group Header)
            sb.Append("GS*HS*APP SENDER*APP RECEIVER*20200101*000000*1*X*004010" + SegmentTerminator);

            // ST Segment (Transaction Set Header)
            sb.Append("ST*270*0001*005010X222" + SegmentTerminator);

            // BHT Segment (Beginning of Hierarchical Transaction)
            sb.Append("BHT*0019*13*244*20200101*0000*CH" + SegmentTerminator);

            // HL Segment (Hierarchical Level - Subscriber)
            sb.Append("HL*1**40*1" + SegmentTerminator);

            // NM1 Segment (Subscriber Name)
            sb.Append($"NM1*IL*1*{info.LastName}*{info.FirstName}****MI*{info.MemberId}" + SegmentTerminator);

            // DMG Segment (Subscriber Demographic Information)
            if (!string.IsNullOrEmpty(info.DateOfBirth))
            {
                string dob = CleanDate(info.DateOfBirth);
                sb.Append($"DMG*D8*{dob}*M" + SegmentTerminator);
            }

            // NM1 Segment (Responsible Party/Plan Holder)
            if (!string.IsNullOrEmpty(info.PlanHolder))
            {
                sb.Append($"NM1*PR*2*{info.PlanHolder}****HR*{info.GroupId}" + SegmentTerminator);
            }

            // HL Segment (Hierarchical Level - Dependent/Patient)
            sb.Append("HL*2*1*23*0" + SegmentTerminator);

            // NM1 Segment (Patient Name - may be same as subscriber or dependent)
            sb.Append($"NM1*IL*1*{info.LastName}*{info.FirstName}****MI*{info.MemberId}" + SegmentTerminator);

            // REF Segment (Service Authorization Exception Code)
            sb.Append("REF*0F*11" + SegmentTerminator);

            // SE Segment (Transaction Set Trailer)
            sb.Append("SE*13*0001" + SegmentTerminator);

            // GE Segment (Functional Group Trailer)
            sb.Append("GE*1*1" + SegmentTerminator);

            // IEA Segment (Interchange Control Trailer)
            sb.Append("IEA*1*000000001" + SegmentTerminator);

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
