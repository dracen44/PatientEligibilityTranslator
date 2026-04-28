namespace PatientEligibilityTranslator
{
    public partial class SettingsForm : Form
    {
        private bool _singlePatientPerRequest;
        private int _maxPatientsPerBatch;

        public bool SinglePatientPerRequest => _singlePatientPerRequest;
        public int MaxPatientsPerBatch => _maxPatientsPerBatch;

        public SettingsForm(bool singlePatient, int maxPatients)
        {
            InitializeComponent();
            _singlePatientPerRequest = singlePatient;
            _maxPatientsPerBatch = maxPatients;
            LoadSettings();
        }

        private void LoadSettings()
        {
            chkSinglePatient.Checked = _singlePatientPerRequest;
            txtMaxPatients.Text = _maxPatientsPerBatch.ToString();
            UpdateMaxPatientsFieldState();
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

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaxPatients.Text, out int value) || value <= 0)
            {
                MessageBox.Show("Max Patients/Batch must be a positive integer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _singlePatientPerRequest = chkSinglePatient.Checked;
            _maxPatientsPerBatch = value;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
