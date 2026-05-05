namespace PatientEligibilityTranslator
{
    partial class ProfileEditForm
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
            lblName = new Label();
            txtName = new TextBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            lblFieldDelimiter = new Label();
            cbFieldDelimiter = new ComboBox();
            txtCustomFieldDelimiter = new TextBox();
            lblLineDelimiter = new Label();
            cbLineDelimiter = new ComboBox();
            txtCustomLineDelimiter = new TextBox();
            chkFirstLineIsHeader = new CheckBox();
            btnOK = new Button();
            btnCancel = new Button();
            gbProfileSettings = new GroupBox();
            gbProfileSettings.SuspendLayout();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(15, 25);
            lblName.Name = "lblName";
            lblName.Size = new Size(42, 15);
            lblName.TabIndex = 0;
            lblName.Text = "Name:";
            // 
            // txtName
            // 
            txtName.Location = new Point(120, 21);
            txtName.Name = "txtName";
            txtName.Size = new Size(220, 23);
            txtName.TabIndex = 1;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(15, 55);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(70, 15);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "Description:";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(120, 51);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(220, 50);
            txtDescription.TabIndex = 3;
            // 
            // lblFieldDelimiter
            // 
            lblFieldDelimiter.AutoSize = true;
            lblFieldDelimiter.Location = new Point(15, 110);
            lblFieldDelimiter.Name = "lblFieldDelimiter";
            lblFieldDelimiter.Size = new Size(86, 15);
            lblFieldDelimiter.TabIndex = 4;
            lblFieldDelimiter.Text = "Field Delimiter:";
            // 
            // cbFieldDelimiter
            // 
            cbFieldDelimiter.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFieldDelimiter.FormattingEnabled = true;
            cbFieldDelimiter.Location = new Point(120, 106);
            cbFieldDelimiter.Name = "cbFieldDelimiter";
            cbFieldDelimiter.Size = new Size(150, 23);
            cbFieldDelimiter.TabIndex = 5;
            // 
            // txtCustomFieldDelimiter
            // 
            txtCustomFieldDelimiter.Location = new Point(276, 106);
            txtCustomFieldDelimiter.Name = "txtCustomFieldDelimiter";
            txtCustomFieldDelimiter.Size = new Size(64, 23);
            txtCustomFieldDelimiter.TabIndex = 6;
            txtCustomFieldDelimiter.Visible = false;
            // 
            // lblLineDelimiter
            // 
            lblLineDelimiter.AutoSize = true;
            lblLineDelimiter.Location = new Point(15, 140);
            lblLineDelimiter.Name = "lblLineDelimiter";
            lblLineDelimiter.Size = new Size(83, 15);
            lblLineDelimiter.TabIndex = 6;
            lblLineDelimiter.Text = "Line Delimiter:";
            // 
            // cbLineDelimiter
            // 
            cbLineDelimiter.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLineDelimiter.FormattingEnabled = true;
            cbLineDelimiter.Location = new Point(120, 136);
            cbLineDelimiter.Name = "cbLineDelimiter";
            cbLineDelimiter.Size = new Size(150, 23);
            cbLineDelimiter.TabIndex = 8;
            // 
            // txtCustomLineDelimiter
            // 
            txtCustomLineDelimiter.Location = new Point(276, 136);
            txtCustomLineDelimiter.Name = "txtCustomLineDelimiter";
            txtCustomLineDelimiter.Size = new Size(64, 23);
            txtCustomLineDelimiter.TabIndex = 9;
            txtCustomLineDelimiter.Visible = false;
            // 
            // chkFirstLineIsHeader
            // 
            chkFirstLineIsHeader.AutoSize = true;
            chkFirstLineIsHeader.Location = new Point(15, 170);
            chkFirstLineIsHeader.Name = "chkFirstLineIsHeader";
            chkFirstLineIsHeader.Size = new Size(150, 19);
            chkFirstLineIsHeader.TabIndex = 10;
            chkFirstLineIsHeader.Text = "First line is header";
            chkFirstLineIsHeader.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(216, 295);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 23);
            btnOK.TabIndex = 11;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += BtnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(297, 295);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 12;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += BtnCancel_Click;
            // 
            // gbProfileSettings
            // 
            gbProfileSettings.Controls.Add(txtCustomLineDelimiter);
            gbProfileSettings.Controls.Add(cbLineDelimiter);
            gbProfileSettings.Controls.Add(lblLineDelimiter);
            gbProfileSettings.Controls.Add(txtCustomFieldDelimiter);
            gbProfileSettings.Controls.Add(cbFieldDelimiter);
            gbProfileSettings.Controls.Add(lblFieldDelimiter);
            gbProfileSettings.Controls.Add(txtDescription);
            gbProfileSettings.Controls.Add(lblDescription);
            gbProfileSettings.Controls.Add(txtName);
            gbProfileSettings.Controls.Add(lblName);
            gbProfileSettings.Controls.Add(chkFirstLineIsHeader);
            gbProfileSettings.Location = new Point(12, 12);
            gbProfileSettings.Name = "gbProfileSettings";
            gbProfileSettings.Size = new Size(360, 200);
            gbProfileSettings.TabIndex = 0;
            gbProfileSettings.TabStop = false;
            gbProfileSettings.Text = "Profile Settings";
            // 
            // (removed format and mapping controls - kept only up to chkFirstLineIsHeader)
            // 
            // ProfileEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 260);
            Controls.Add(gbProfileSettings);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ProfileEditForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edit Profile";
            gbProfileSettings.ResumeLayout(false);
            gbProfileSettings.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblFieldDelimiter;
        private System.Windows.Forms.ComboBox cbFieldDelimiter;
        private System.Windows.Forms.TextBox txtCustomFieldDelimiter;
        private System.Windows.Forms.Label lblLineDelimiter;
        private System.Windows.Forms.ComboBox cbLineDelimiter;
        private System.Windows.Forms.TextBox txtCustomLineDelimiter;
        private System.Windows.Forms.CheckBox chkFirstLineIsHeader;
        // removed format/mapping controls
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox gbProfileSettings;
    }
}
