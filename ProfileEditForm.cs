namespace PatientEligibilityTranslator
{
    public partial class ProfileEditForm : Form
    {
        private FileFormatProfile? _profile;

        public FileFormatProfile? Profile => _profile;

        public ProfileEditForm(FileFormatProfile? profileToEdit)
        {
            InitializeComponent();
            _profile = profileToEdit != null ? new FileFormatProfile
            {
                Name = profileToEdit.Name,
                Description = profileToEdit.Description,
                FieldDelimiter = profileToEdit.FieldDelimiter,
                LineDelimiter = profileToEdit.LineDelimiter,
                IsDefault = profileToEdit.IsDefault,
                FieldMappings = new Dictionary<string, string>(profileToEdit.FieldMappings),
                ColumnIndexMappings = new Dictionary<int, string>(profileToEdit.ColumnIndexMappings),
                FirstLineIsHeader = profileToEdit.FirstLineIsHeader
            } : new FileFormatProfile();

            LoadProfileData();

            // Subscribe to dropdown change events
            cbFieldDelimiter.SelectedIndexChanged += CbFieldDelimiter_SelectedIndexChanged;
            cbLineDelimiter.SelectedIndexChanged += CbLineDelimiter_SelectedIndexChanged;
        }

        private void LoadProfileData()
        {
            if (_profile == null) return;

            txtName.Text = _profile.Name;
            txtName.Enabled = _profile.IsDefault == false;
            txtDescription.Text = _profile.Description;

            // initialize field delimiter dropdown with common defaults and a Custom option
            cbFieldDelimiter.Items.Clear();
            cbFieldDelimiter.Items.AddRange(new object[] { ",", "|", ";", "\t", "Custom" });
            var fd = UnescapeString(_profile.FieldDelimiter);
            if (cbFieldDelimiter.Items.Contains(fd))
            {
                cbFieldDelimiter.SelectedItem = fd;
                txtCustomFieldDelimiter.Visible = false;
                txtCustomFieldDelimiter.Enabled = false;
                txtCustomFieldDelimiter.Text = string.Empty;
            }
            else
            {
                cbFieldDelimiter.SelectedItem = "Custom";
                txtCustomFieldDelimiter.Visible = true;
                txtCustomFieldDelimiter.Enabled = true;
                txtCustomFieldDelimiter.Text = fd;
            }

            // initialize line delimiter dropdown with common defaults and a Custom option
            cbLineDelimiter.Items.Clear();
            cbLineDelimiter.Items.AddRange(new object[] { "LF (\\n)", "CRLF (\\r\\n)", "CR (\\r)", "Custom" });
            var ld = UnescapeString(_profile.LineDelimiter);
            
            // Map the actual delimiter to its display label
            string selectedLabel = ld switch
            {
                "\n" => "LF (\\n)",
                "\r\n" => "CRLF (\\r\\n)",
                "\r" => "CR (\\r)",
                _ => "Custom"
            };
            
            if (cbLineDelimiter.Items.Contains(selectedLabel) && selectedLabel != "Custom")
            {
                cbLineDelimiter.SelectedItem = selectedLabel;
                txtCustomLineDelimiter.Visible = false;
                txtCustomLineDelimiter.Enabled = false;
                txtCustomLineDelimiter.Text = string.Empty;
            }
            else
            {
                cbLineDelimiter.SelectedItem = "Custom";
                txtCustomLineDelimiter.Visible = true;
                txtCustomLineDelimiter.Enabled = true;
                txtCustomLineDelimiter.Text = ld;
            }

            chkFirstLineIsHeader.Checked = _profile.FirstLineIsHeader;
        }

        private string UnescapeString(string value)
        {
            return value.Replace("\\n", "\n").Replace("\\t", "\t").Replace("\\r", "\r");
        }

        private string EscapeString(string value)
        {
            return value.Replace("\n", "\\n").Replace("\t", "\\t").Replace("\r", "\\r");
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Profile name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_profile == null)
            {
                _profile = new FileFormatProfile();
            }

            _profile.Name = txtName.Text.Trim();
            _profile.Description = txtDescription.Text.Trim();

            // determine field delimiter from dropdown or custom
            var selField = cbFieldDelimiter.SelectedItem?.ToString() ?? ",";
            string chosenFieldDelimiter;
            if (selField == "Custom")
            {
                chosenFieldDelimiter = txtCustomFieldDelimiter.Text ?? string.Empty;
            }
            else
            {
                chosenFieldDelimiter = selField;
            }

            _profile.FieldDelimiter = EscapeString(chosenFieldDelimiter);

            // determine line delimiter from dropdown or custom
            var selLine = cbLineDelimiter.SelectedItem?.ToString() ?? "LF (\\n)";
            string chosenLineDelimiter;
            if (selLine == "Custom")
            {
                chosenLineDelimiter = txtCustomLineDelimiter.Text ?? string.Empty;
            }
            else
            {
                // Map display label back to actual delimiter
                chosenLineDelimiter = selLine switch
                {
                    "LF (\\n)" => "\n",
                    "CRLF (\\r\\n)" => "\r\n",
                    "CR (\\r)" => "\r",
                    _ => "\n"
                };
            }

            _profile.LineDelimiter = EscapeString(chosenLineDelimiter);

            _profile.FirstLineIsHeader = chkFirstLineIsHeader.Checked;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void CbFieldDelimiter_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = cbFieldDelimiter.SelectedItem?.ToString();
            if (selected == "Custom")
            {
                txtCustomFieldDelimiter.Visible = true;
                txtCustomFieldDelimiter.Enabled = true;
                txtCustomFieldDelimiter.Focus();
            }
            else
            {
                txtCustomFieldDelimiter.Visible = false;
                txtCustomFieldDelimiter.Enabled = false;
                txtCustomFieldDelimiter.Text = string.Empty;
            }
        }

        private void CbLineDelimiter_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = cbLineDelimiter.SelectedItem?.ToString();
            if (selected == "Custom")
            {
                txtCustomLineDelimiter.Visible = true;
                txtCustomLineDelimiter.Enabled = true;
                txtCustomLineDelimiter.Focus();
            }
            else
            {
                txtCustomLineDelimiter.Visible = false;
                txtCustomLineDelimiter.Enabled = false;
                txtCustomLineDelimiter.Text = string.Empty;
            }
        }
    }
}
