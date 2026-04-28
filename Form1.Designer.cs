namespace PatientEligibilityTranslator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.btnTranslateTo270 = new System.Windows.Forms.Button();
            this.btnTranslate271Response = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.lblInput = new System.Windows.Forms.Label();
            this.lblOutput = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();

            // lblFilePath
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(12, 12);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(57, 15);
            this.lblFilePath.TabIndex = 0;
            this.lblFilePath.Text = "File Path:";

            // txtFilePath
            this.txtFilePath.Location = new System.Drawing.Point(75, 12);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(500, 23);
            this.txtFilePath.TabIndex = 1;
            this.txtFilePath.ReadOnly = true;

            // btnSelectFile
            this.btnSelectFile.Location = new System.Drawing.Point(585, 12);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(100, 23);
            this.btnSelectFile.TabIndex = 2;
            this.btnSelectFile.Text = "Select File";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.BtnSelectFile_Click);

            // lblInput
            this.lblInput.AutoSize = true;
            this.lblInput.Location = new System.Drawing.Point(12, 50);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(38, 15);
            this.lblInput.TabIndex = 3;
            this.lblInput.Text = "Input:";

            // txtInput
            this.txtInput.Location = new System.Drawing.Point(12, 70);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInput.Size = new System.Drawing.Size(380, 300);
            this.txtInput.TabIndex = 4;

            // lblOutput
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(410, 50);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(47, 15);
            this.lblOutput.TabIndex = 5;
            this.lblOutput.Text = "Output:";

            // txtOutput
            this.txtOutput.Location = new System.Drawing.Point(410, 70);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(380, 300);
            this.txtOutput.TabIndex = 6;

            // btnTranslateTo270
            this.btnTranslateTo270.Location = new System.Drawing.Point(12, 385);
            this.btnTranslateTo270.Name = "btnTranslateTo270";
            this.btnTranslateTo270.Size = new System.Drawing.Size(120, 40);
            this.btnTranslateTo270.TabIndex = 7;
            this.btnTranslateTo270.Text = "Translate to 270";
            this.btnTranslateTo270.UseVisualStyleBackColor = true;
            this.btnTranslateTo270.Click += new System.EventHandler(this.BtnTranslateTo270_Click);

            // btnTranslate271Response
            this.btnTranslate271Response.Location = new System.Drawing.Point(140, 385);
            this.btnTranslate271Response.Name = "btnTranslate271Response";
            this.btnTranslate271Response.Size = new System.Drawing.Size(120, 40);
            this.btnTranslate271Response.TabIndex = 8;
            this.btnTranslate271Response.Text = "Translate 271";
            this.btnTranslate271Response.UseVisualStyleBackColor = true;
            this.btnTranslate271Response.Click += new System.EventHandler(this.BtnTranslate271Response_Click);

            // Form1
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.lblInput);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnTranslateTo270);
            this.Controls.Add(this.btnTranslate271Response);
            this.Name = "Form1";
            this.Text = "Patient Eligibility EDI Translator";
            this.ResumeLayout(true);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Button btnTranslateTo270;
        private System.Windows.Forms.Button btnTranslate271Response;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.OpenFileDialog openFileDialog;

        #endregion
    }
}
