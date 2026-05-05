namespace PatientEligibilityTranslator
{
    public partial class FieldMapperForm : Form
    {
        private Dictionary<string, string> _fieldMappings;
        private List<string> _availableFields;

        public Dictionary<string, string> FieldMappings => _fieldMappings;

        public FieldMapperForm(Dictionary<string, string> fieldMappings, List<string> availableFields = null)
        {
            InitializeComponent();
            _fieldMappings = new Dictionary<string, string>(fieldMappings);
            _availableFields = availableFields ?? new List<string>();
            LoadMappings();
        }

        private void LoadMappings()
        {
            dgvMappings.DataSource = null;
            var mappingList = _fieldMappings
                .Select(kvp => new { InputField = kvp.Key, OutputProperty = kvp.Value })
                .ToList();
            dgvMappings.DataSource = mappingList;
            
            // Format columns
            dgvMappings.Columns[0].HeaderText = "Input Field Name";
            dgvMappings.Columns[1].HeaderText = "Map To Property";
            dgvMappings.Columns[0].Width = 150;
            dgvMappings.Columns[1].Width = 150;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInputField.Text) || string.IsNullOrWhiteSpace(txtOutputProperty.Text))
            {
                MessageBox.Show("Please enter both input field and output property.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var inputKey = txtInputField.Text.Trim().ToLower();
            if (_fieldMappings.ContainsKey(inputKey))
            {
                MessageBox.Show($"Mapping for '{inputKey}' already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _fieldMappings[inputKey] = txtOutputProperty.Text.Trim();
            txtInputField.Clear();
            txtOutputProperty.Clear();
            LoadMappings();
            txtInputField.Focus();
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (dgvMappings.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a mapping to remove.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvMappings.SelectedRows[0];
            if (selectedRow.DataBoundItem != null)
            {
                var item = (dynamic)selectedRow.DataBoundItem;
                var inputField = (string)item.InputField;
                if (_fieldMappings.ContainsKey(inputField))
                {
                    _fieldMappings.Remove(inputField);
                    LoadMappings();
                }
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
