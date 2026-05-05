namespace PatientEligibilityTranslator
{
    public partial class SettingsForm : Form
    {
        private bool _singlePatientPerRequest;
        private int _maxPatientsPerBatch;
        private List<FileFormatProfile> _profiles;
        private string _activeProfileName;

        public bool SinglePatientPerRequest => _singlePatientPerRequest;
        public int MaxPatientsPerBatch => _maxPatientsPerBatch;
        public List<FileFormatProfile> Profiles => _profiles;
        public string ActiveProfileName => _activeProfileName;

        public SettingsForm(bool singlePatient, int maxPatients, List<FileFormatProfile> profiles, string activeProfileName)
        {
            InitializeComponent();
            _singlePatientPerRequest = singlePatient;
            _maxPatientsPerBatch = maxPatients;
            _profiles = new List<FileFormatProfile>(profiles);
            _activeProfileName = activeProfileName;
            LoadSettings();
        }

        private void LoadSettings()
        {
            chkSinglePatient.Checked = _singlePatientPerRequest;
            txtMaxPatients.Text = _maxPatientsPerBatch.ToString();
            UpdateMaxPatientsFieldState();
            LoadProfiles();
        }

        private void LoadProfiles()
        {
            cbProfiles.Items.Clear();
            foreach (var profile in _profiles)
            {
                cbProfiles.Items.Add(profile.Name);
            }

            // Select active profile
            if (!string.IsNullOrEmpty(_activeProfileName) && cbProfiles.Items.Contains(_activeProfileName))
            {
                cbProfiles.SelectedItem = _activeProfileName;
            }
            else if (cbProfiles.Items.Count > 0)
            {
                cbProfiles.SelectedIndex = 0;
            }
        }

        private void ChkSinglePatient_CheckedChanged(object sender, EventArgs e)
        {
            UpdateMaxPatientsFieldState();
        }

        private void UpdateMaxPatientsFieldState()
        {
            txtMaxPatients.Enabled = !chkSinglePatient.Checked;
            lblMaxPatients.Enabled = !chkSinglePatient.Checked;
        }

        private void TxtMaxPatients_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!int.TryParse(txtMaxPatients.Text, out int value) || value <= 0)
            {
                e.Cancel = true;
                txtMaxPatients.BackColor = System.Drawing.Color.LightCoral;
                errorProvider.SetError(txtMaxPatients, "Must be a positive integer");
            }
            else
            {
                e.Cancel = false;
                txtMaxPatients.BackColor = System.Drawing.SystemColors.Window;
                errorProvider.SetError(txtMaxPatients, "");
            }
        }

        private void BtnManageProfiles_Click(object sender, EventArgs e)
        {
            var managementForm = new ProfileManagementForm(_profiles);
            if (managementForm.ShowDialog(this) == DialogResult.OK)
            {
                _profiles = managementForm.Profiles;
                LoadProfiles();
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaxPatients.Text, out int value) || value <= 0)
            {
                MessageBox.Show("Max Patients/Batch must be a positive integer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _singlePatientPerRequest = chkSinglePatient.Checked;
            _maxPatientsPerBatch = value;
            _activeProfileName = cbProfiles.SelectedItem?.ToString() ?? "Default";
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
