namespace PatientEligibilityTranslator
{
    public partial class ProfileManagementForm : Form
    {
        private List<FileFormatProfile> _profiles;
        private FileFormatProfile? _currentProfile;

        public List<FileFormatProfile> Profiles => _profiles;
        public FileFormatProfile? SelectedProfile => _currentProfile;

        public ProfileManagementForm(List<FileFormatProfile> profiles)
        {
            InitializeComponent();
            _profiles = new List<FileFormatProfile>(profiles);
            LoadProfiles();
        }

        private void LoadProfiles()
        {
            lstProfiles.Items.Clear();
            foreach (var profile in _profiles.Where(p => !p.IsDefault))
            {
                lstProfiles.Items.Add(profile.Name);
            }
        }

        private void LstProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstProfiles.SelectedIndex >= 0)
            {
                string selectedName = lstProfiles.SelectedItem?.ToString() ?? string.Empty;
                _currentProfile = _profiles.FirstOrDefault(p => p.Name == selectedName);
                DisplayProfileDetails();
                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
            }
            else
            {
                ClearProfileDetails();
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
            }
        }

        private void DisplayProfileDetails()
        {
            if (_currentProfile == null) return;

            lblProfileName.Text = $"Name: {_currentProfile.Name}";
            lblDescription.Text = $"Description: {_currentProfile.Description}";
            lblFieldDelimiter.Text = $"Field Delimiter: {_currentProfile.FieldDelimiter}";
            
            string patientDelimiterDisplay = _currentProfile.UseLineDelimiterForPatients 
                ? $"Patient Delimiter: (Same as Line Delimiter)" 
                : $"Patient Delimiter: {_currentProfile.PatientDelimiter}";
            lblPatientDelimiter.Text = patientDelimiterDisplay;
        }

        private void ClearProfileDetails()
        {
            lblProfileName.Text = "Name: -";
            lblDescription.Text = "Description: -";
            lblFieldDelimiter.Text = "Field Delimiter: -";
            lblPatientDelimiter.Text = "Patient Delimiter: -";
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            var editForm = new ProfileEditForm(null);
            if (editForm.ShowDialog(this) == DialogResult.OK)
            {
                var newProfile = editForm.Profile;
                if (newProfile != null && !_profiles.Any(p => p.Name == newProfile.Name))
                {
                    _profiles.Add(newProfile);
                    LoadProfiles();
                    lstProfiles.SelectedItem = newProfile.Name;
                }
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (_currentProfile == null) return;

            var editForm = new ProfileEditForm(_currentProfile);
            if (editForm.ShowDialog(this) == DialogResult.OK)
            {
                var updatedProfile = editForm.Profile;
                if (updatedProfile != null)
                {
                    int index = _profiles.IndexOf(_currentProfile);
                    if (index >= 0)
                    {
                        _profiles[index] = updatedProfile;
                        _currentProfile = updatedProfile;
                        LoadProfiles();
                        lstProfiles.SelectedItem = updatedProfile.Name;
                        DisplayProfileDetails();
                    }
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_currentProfile == null) return;

            var result = MessageBox.Show(
                $"Are you sure you want to delete the profile '{_currentProfile.Name}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _profiles.Remove(_currentProfile);
                _currentProfile = null;
                LoadProfiles();
                ClearProfileDetails();
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
