namespace PatientEligibilityTranslator
{
    partial class ProfileManagementForm
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
            this.components = new System.ComponentModel.Container();
            this.lstProfiles = new System.Windows.Forms.ListBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.gbProfiles = new System.Windows.Forms.GroupBox();
            this.gbDetails = new System.Windows.Forms.GroupBox();
            this.lblProfileName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblFieldDelimiter = new System.Windows.Forms.Label();
            this.lblPatientDelimiter = new System.Windows.Forms.Label();

            this.gbProfiles.SuspendLayout();
            this.gbDetails.SuspendLayout();
            this.SuspendLayout();

            // gbProfiles
            this.gbProfiles.Controls.Add(this.lstProfiles);
            this.gbProfiles.Location = new System.Drawing.Point(12, 12);
            this.gbProfiles.Name = "gbProfiles";
            this.gbProfiles.Size = new System.Drawing.Size(300, 300);
            this.gbProfiles.TabIndex = 0;
            this.gbProfiles.TabStop = false;
            this.gbProfiles.Text = "Profiles";

            // lstProfiles
            this.lstProfiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstProfiles.FormattingEnabled = true;
            this.lstProfiles.ItemHeight = 15;
            this.lstProfiles.Location = new System.Drawing.Point(3, 19);
            this.lstProfiles.Name = "lstProfiles";
            this.lstProfiles.Size = new System.Drawing.Size(294, 278);
            this.lstProfiles.TabIndex = 0;
            this.lstProfiles.SelectedIndexChanged += new System.EventHandler(this.LstProfiles_SelectedIndexChanged);

            // gbDetails
            this.gbDetails.Controls.Add(this.lblPatientDelimiter);
            this.gbDetails.Controls.Add(this.lblFieldDelimiter);
            this.gbDetails.Controls.Add(this.lblDescription);
            this.gbDetails.Controls.Add(this.lblProfileName);
            this.gbDetails.Location = new System.Drawing.Point(318, 12);
            this.gbDetails.Name = "gbDetails";
            this.gbDetails.Size = new System.Drawing.Size(300, 200);
            this.gbDetails.TabIndex = 1;
            this.gbDetails.TabStop = false;
            this.gbDetails.Text = "Profile Details";

            // lblProfileName
            this.lblProfileName.AutoSize = true;
            this.lblProfileName.Location = new System.Drawing.Point(15, 25);
            this.lblProfileName.Name = "lblProfileName";
            this.lblProfileName.Size = new System.Drawing.Size(57, 15);
            this.lblProfileName.TabIndex = 0;
            this.lblProfileName.Text = "Name: -";

            // lblDescription
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(15, 55);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(107, 15);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Description: -";

            // lblFieldDelimiter
            this.lblFieldDelimiter.AutoSize = true;
            this.lblFieldDelimiter.Location = new System.Drawing.Point(15, 85);
            this.lblFieldDelimiter.Name = "lblFieldDelimiter";
            this.lblFieldDelimiter.Size = new System.Drawing.Size(127, 15);
            this.lblFieldDelimiter.TabIndex = 2;
            this.lblFieldDelimiter.Text = "Field Delimiter: -";

            // lblPatientDelimiter
            this.lblPatientDelimiter.AutoSize = true;
            this.lblPatientDelimiter.Location = new System.Drawing.Point(15, 115);
            this.lblPatientDelimiter.Name = "lblPatientDelimiter";
            this.lblPatientDelimiter.Size = new System.Drawing.Size(137, 15);
            this.lblPatientDelimiter.TabIndex = 3;
            this.lblPatientDelimiter.Text = "Patient Delimiter: -";

            // btnNew
            this.btnNew.Location = new System.Drawing.Point(318, 225);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(70, 23);
            this.btnNew.TabIndex = 2;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.BtnNew_Click);

            // btnEdit
            this.btnEdit.Enabled = false;
            this.btnEdit.Location = new System.Drawing.Point(394, 225);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(70, 23);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);

            // btnDelete
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(470, 225);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);

            // btnClose
            this.btnClose.Location = new System.Drawing.Point(543, 290);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);

            // ProfileManagementForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 330);
            this.Controls.Add(this.gbProfiles);
            this.Controls.Add(this.gbDetails);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProfileManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage File Format Profiles";

            this.gbProfiles.ResumeLayout(false);
            this.gbDetails.ResumeLayout(false);
            this.gbDetails.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.ListBox lstProfiles;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox gbProfiles;
        private System.Windows.Forms.GroupBox gbDetails;
        private System.Windows.Forms.Label lblProfileName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblFieldDelimiter;
        private System.Windows.Forms.Label lblPatientDelimiter;
    }
}
