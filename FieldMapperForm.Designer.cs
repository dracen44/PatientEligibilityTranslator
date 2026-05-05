namespace PatientEligibilityTranslator
{
    partial class FieldMapperForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblInputField = new Label();
            txtInputField = new TextBox();
            lblOutputProperty = new Label();
            txtOutputProperty = new TextBox();
            btnAdd = new Button();
            btnRemove = new Button();
            dgvMappings = new DataGridView();
            btnOK = new Button();
            btnCancel = new Button();
            gbMappingInput = new GroupBox();
            gbMappingInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMappings).BeginInit();
            SuspendLayout();

            // lblInputField
            lblInputField.AutoSize = true;
            lblInputField.Location = new Point(15, 25);
            lblInputField.Name = "lblInputField";
            lblInputField.Size = new Size(77, 15);
            lblInputField.TabIndex = 0;
            lblInputField.Text = "Input Field:";

            // txtInputField
            txtInputField.Location = new Point(100, 21);
            txtInputField.Name = "txtInputField";
            txtInputField.Size = new Size(150, 23);
            txtInputField.TabIndex = 1;

            // lblOutputProperty
            lblOutputProperty.AutoSize = true;
            lblOutputProperty.Location = new Point(15, 55);
            lblOutputProperty.Name = "lblOutputProperty";
            lblOutputProperty.Size = new Size(105, 15);
            lblOutputProperty.TabIndex = 2;
            lblOutputProperty.Text = "Output Property:";

            // txtOutputProperty
            txtOutputProperty.Location = new Point(100, 51);
            txtOutputProperty.Name = "txtOutputProperty";
            txtOutputProperty.Size = new Size(150, 23);
            txtOutputProperty.TabIndex = 3;

            // btnAdd
            btnAdd.Location = new Point(260, 21);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(60, 23);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += BtnAdd_Click;

            // btnRemove
            btnRemove.Location = new Point(260, 51);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(60, 23);
            btnRemove.TabIndex = 5;
            btnRemove.Text = "Remove";
            btnRemove.UseVisualStyleBackColor = true;
            btnRemove.Click += BtnRemove_Click;

            // gbMappingInput
            gbMappingInput.Controls.Add(btnRemove);
            gbMappingInput.Controls.Add(btnAdd);
            gbMappingInput.Controls.Add(txtOutputProperty);
            gbMappingInput.Controls.Add(lblOutputProperty);
            gbMappingInput.Controls.Add(txtInputField);
            gbMappingInput.Controls.Add(lblInputField);
            gbMappingInput.Location = new Point(12, 12);
            gbMappingInput.Name = "gbMappingInput";
            gbMappingInput.Size = new Size(330, 85);
            gbMappingInput.TabIndex = 0;
            gbMappingInput.TabStop = false;
            gbMappingInput.Text = "Add/Remove Mappings";

            // dgvMappings
            dgvMappings.AllowUserToAddRows = false;
            dgvMappings.AllowUserToDeleteRows = false;
            dgvMappings.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMappings.Location = new Point(12, 105);
            dgvMappings.Name = "dgvMappings";
            dgvMappings.ReadOnly = true;
            dgvMappings.RowTemplate.Height = 25;
            dgvMappings.Size = new Size(330, 200);
            dgvMappings.TabIndex = 1;

            // btnOK
            btnOK.Location = new Point(186, 315);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 23);
            btnOK.TabIndex = 2;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += BtnOK_Click;

            // btnCancel
            btnCancel.Location = new Point(267, 315);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += BtnCancel_Click;

            // FieldMapperForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(354, 350);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(dgvMappings);
            Controls.Add(gbMappingInput);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FieldMapperForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Configure Field Mappings";
            gbMappingInput.ResumeLayout(false);
            gbMappingInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMappings).EndInit();
            ResumeLayout(false);
        }

        private Label lblInputField;
        private TextBox txtInputField;
        private Label lblOutputProperty;
        private TextBox txtOutputProperty;
        private Button btnAdd;
        private Button btnRemove;
        private DataGridView dgvMappings;
        private Button btnOK;
        private Button btnCancel;
        private GroupBox gbMappingInput;
    }
}
