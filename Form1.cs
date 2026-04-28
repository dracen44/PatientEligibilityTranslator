using PatientEligibilityTranslator.EDI;

namespace PatientEligibilityTranslator
{
    public partial class Form1 : Form
    {
        private string _currentFilePath = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Text files (*.txt)|*.txt|EDI files (*.edi)|*.edi|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _currentFilePath = openFileDialog.FileName;
                txtFilePath.Text = _currentFilePath;

                try
                {
                    string fileContent = File.ReadAllText(_currentFilePath);
                    txtInput.Text = fileContent;
                    txtOutput.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnTranslateTo270_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtInput.Text))
                {
                    MessageBox.Show("Please load a file or enter input data.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string inputData = txtInput.Text;
                var translator = new EDI270Translator();
                string edi270 = translator.Translate(inputData);
                txtOutput.Text = edi270;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error translating to 270: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnTranslate271Response_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtInput.Text))
                {
                    MessageBox.Show("Please load a 271 EDI file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string edi271Content = txtInput.Text;
                var translator = new EDI271Translator();
                string result = translator.Translate(edi271Content);
                txtOutput.Text = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error translating 271: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
