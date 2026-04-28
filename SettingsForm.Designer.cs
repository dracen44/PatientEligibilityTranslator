namespace PatientEligibilityTranslator
{
    partial class SettingsForm
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
            this.chkSinglePatient = new System.Windows.Forms.CheckBox();
            this.lblMaxPatients = new System.Windows.Forms.Label();
            this.txtMaxPatients = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbEDI270Settings = new System.Windows.Forms.GroupBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);

            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.gbEDI270Settings.SuspendLayout();

            // gbEDI270Settings
            this.gbEDI270Settings.Controls.Add(this.chkSinglePatient);
            this.gbEDI270Settings.Controls.Add(this.lblMaxPatients);
            this.gbEDI270Settings.Controls.Add(this.txtMaxPatients);
            this.gbEDI270Settings.Location = new System.Drawing.Point(12, 12);
            this.gbEDI270Settings.Name = "gbEDI270Settings";
            this.gbEDI270Settings.Size = new System.Drawing.Size(460, 100);
            this.gbEDI270Settings.TabIndex = 0;
            this.gbEDI270Settings.TabStop = false;
            this.gbEDI270Settings.Text = "EDI 270 Settings";

            // chkSinglePatient
            this.chkSinglePatient.AutoSize = true;
            this.chkSinglePatient.Location = new System.Drawing.Point(15, 25);
            this.chkSinglePatient.Name = "chkSinglePatient";
            this.chkSinglePatient.Size = new System.Drawing.Size(145, 19);
            this.chkSinglePatient.TabIndex = 0;
            this.chkSinglePatient.Text = "Single Patient Per Request";
            this.chkSinglePatient.UseVisualStyleBackColor = true;
            this.chkSinglePatient.CheckedChanged += new System.EventHandler(this.ChkSinglePatient_CheckedChanged);

            // lblMaxPatients
            this.lblMaxPatients.AutoSize = true;
            this.lblMaxPatients.Location = new System.Drawing.Point(15, 60);
            this.lblMaxPatients.Name = "lblMaxPatients";
            this.lblMaxPatients.Size = new System.Drawing.Size(108, 15);
            this.lblMaxPatients.TabIndex = 1;
            this.lblMaxPatients.Text = "Max Patients/Batch:";

            // txtMaxPatients
            this.txtMaxPatients.Location = new System.Drawing.Point(130, 56);
            this.txtMaxPatients.Name = "txtMaxPatients";
            this.txtMaxPatients.Size = new System.Drawing.Size(60, 23);
            this.txtMaxPatients.TabIndex = 2;
            this.txtMaxPatients.Text = "50";
            this.txtMaxPatients.Validating += new System.ComponentModel.CancelEventHandler(this.TxtMaxPatients_Validating);

            // btnOK
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(316, 130);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);

            // btnCancel
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(397, 130);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;

            // errorProvider
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;

            // SettingsForm
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(484, 165);
            this.Controls.Add(this.gbEDI270Settings);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EDI Translator Settings";

            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.gbEDI270Settings.ResumeLayout(false);
            this.gbEDI270Settings.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.CheckBox chkSinglePatient;
        private System.Windows.Forms.Label lblMaxPatients;
        private System.Windows.Forms.TextBox txtMaxPatients;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox gbEDI270Settings;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
