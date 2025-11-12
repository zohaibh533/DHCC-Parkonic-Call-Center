namespace ParkonicCallCenter.UI
{
    partial class frmCreateTrip
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtPlateCode = new DevExpress.XtraEditors.TextEdit();
            this.txtPlateNo = new DevExpress.XtraEditors.TextEdit();
            this.lblDeviceName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreateTrip = new System.Windows.Forms.Button();
            this.ppMainWait = new DevExpress.XtraWaitForm.ProgressPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbPNFCity = new DevExpress.XtraEditors.ComboBoxEdit();
            this.dtEntryTime = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlateCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlateNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPNFCity.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPlateCode
            // 
            this.txtPlateCode.EditValue = "";
            this.txtPlateCode.Location = new System.Drawing.Point(38, 71);
            this.txtPlateCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPlateCode.Name = "txtPlateCode";
            this.txtPlateCode.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlateCode.Properties.Appearance.Options.UseFont = true;
            this.txtPlateCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtPlateCode.Properties.NullText = "Code";
            this.txtPlateCode.Properties.NullValuePrompt = "Code";
            this.txtPlateCode.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtPlateCode.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPlateCode.Size = new System.Drawing.Size(212, 42);
            this.txtPlateCode.TabIndex = 0;
            // 
            // txtPlateNo
            // 
            this.txtPlateNo.EditValue = "";
            this.txtPlateNo.Location = new System.Drawing.Point(298, 71);
            this.txtPlateNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPlateNo.Name = "txtPlateNo";
            this.txtPlateNo.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlateNo.Properties.Appearance.Options.UseFont = true;
            this.txtPlateNo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtPlateNo.Properties.NullText = "Plate Number";
            this.txtPlateNo.Properties.NullValuePrompt = "Plate Number";
            this.txtPlateNo.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtPlateNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPlateNo.Size = new System.Drawing.Size(315, 42);
            this.txtPlateNo.TabIndex = 1;
            // 
            // lblDeviceName
            // 
            this.lblDeviceName.AutoSize = true;
            this.lblDeviceName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.lblDeviceName.Location = new System.Drawing.Point(38, 24);
            this.lblDeviceName.Name = "lblDeviceName";
            this.lblDeviceName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblDeviceName.Size = new System.Drawing.Size(133, 29);
            this.lblDeviceName.TabIndex = 38;
            this.lblDeviceName.Text = "Plate Code";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label1.Location = new System.Drawing.Point(298, 24);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(161, 29);
            this.label1.TabIndex = 39;
            this.label1.Text = "Plate Number";
            // 
            // btnCreateTrip
            // 
            this.btnCreateTrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(25)))), ((int)(((byte)(54)))));
            this.btnCreateTrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCreateTrip.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateTrip.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCreateTrip.FlatAppearance.BorderSize = 0;
            this.btnCreateTrip.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(25)))), ((int)(((byte)(54)))));
            this.btnCreateTrip.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(25)))), ((int)(((byte)(54)))));
            this.btnCreateTrip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateTrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateTrip.ForeColor = System.Drawing.Color.White;
            this.btnCreateTrip.Location = new System.Drawing.Point(152, 276);
            this.btnCreateTrip.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCreateTrip.Name = "btnCreateTrip";
            this.btnCreateTrip.Size = new System.Drawing.Size(347, 59);
            this.btnCreateTrip.TabIndex = 4;
            this.btnCreateTrip.Text = "CREATE TRIP";
            this.btnCreateTrip.UseVisualStyleBackColor = false;
            this.btnCreateTrip.Click += new System.EventHandler(this.btnCreateTrip_Click);
            // 
            // ppMainWait
            // 
            this.ppMainWait.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ppMainWait.Appearance.ForeColor = System.Drawing.Color.Black;
            this.ppMainWait.Appearance.Options.UseBackColor = true;
            this.ppMainWait.Appearance.Options.UseForeColor = true;
            this.ppMainWait.AppearanceCaption.ForeColor = System.Drawing.Color.Black;
            this.ppMainWait.AppearanceCaption.Options.UseForeColor = true;
            this.ppMainWait.AppearanceDescription.ForeColor = System.Drawing.Color.Black;
            this.ppMainWait.AppearanceDescription.Options.UseForeColor = true;
            this.ppMainWait.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.ppMainWait.Location = new System.Drawing.Point(98, 121);
            this.ppMainWait.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ppMainWait.Name = "ppMainWait";
            this.ppMainWait.Size = new System.Drawing.Size(194, 53);
            this.ppMainWait.TabIndex = 82;
            this.ppMainWait.WaitAnimationType = DevExpress.Utils.Animation.WaitingAnimatorType.Ring;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label2.Location = new System.Drawing.Point(38, 154);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(53, 29);
            this.label2.TabIndex = 83;
            this.label2.Text = "City";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label3.Location = new System.Drawing.Point(298, 154);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(129, 29);
            this.label3.TabIndex = 84;
            this.label3.Text = "Entry Time";
            // 
            // cmbPNFCity
            // 
            this.cmbPNFCity.EditValue = "Dubai";
            this.cmbPNFCity.Location = new System.Drawing.Point(38, 189);
            this.cmbPNFCity.Name = "cmbPNFCity";
            this.cmbPNFCity.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.cmbPNFCity.Properties.Appearance.Options.UseFont = true;
            this.cmbPNFCity.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.cmbPNFCity.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cmbPNFCity.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.cmbPNFCity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPNFCity.Properties.Items.AddRange(new object[] {
            "Dubai",
            "Abu Dhabi",
            "Sharjah",
            "Ajman",
            "RAK",
            "Fujairah",
            "UAQ",
            "Government",
            "KSA",
            "Qatar",
            "Oman",
            "Kuwait",
            "Bahrain"});
            this.cmbPNFCity.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Flat;
            this.cmbPNFCity.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbPNFCity.Size = new System.Drawing.Size(212, 44);
            this.cmbPNFCity.TabIndex = 2;
            // 
            // dtEntryTime
            // 
            this.dtEntryTime.CustomFormat = "dd-MMM-yyyy HH:mm:ss";
            this.dtEntryTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.dtEntryTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEntryTime.Location = new System.Drawing.Point(298, 193);
            this.dtEntryTime.Name = "dtEntryTime";
            this.dtEntryTime.Size = new System.Drawing.Size(315, 36);
            this.dtEntryTime.TabIndex = 3;
            // 
            // frmCreateTrip
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 366);
            this.Controls.Add(this.dtEntryTime);
            this.Controls.Add(this.cmbPNFCity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ppMainWait);
            this.Controls.Add(this.btnCreateTrip);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDeviceName);
            this.Controls.Add(this.txtPlateCode);
            this.Controls.Add(this.txtPlateNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCreateTrip";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Trip";
            this.Load += new System.EventHandler(this.frmCreateTrip_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtPlateCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlateNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPNFCity.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtPlateCode;
        private DevExpress.XtraEditors.TextEdit txtPlateNo;
        private System.Windows.Forms.Label lblDeviceName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreateTrip;
        public DevExpress.XtraWaitForm.ProgressPanel ppMainWait;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.ComboBoxEdit cmbPNFCity;
        private System.Windows.Forms.DateTimePicker dtEntryTime;
        //   private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
    }
}