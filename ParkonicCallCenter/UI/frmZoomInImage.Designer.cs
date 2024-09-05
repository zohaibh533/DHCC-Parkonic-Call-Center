namespace ParkonicCallCenter.UI
{
    partial class frmZoomInImage
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
            DevExpress.XtraEditors.Repository.TrackBarLabel trackBarLabel1 = new DevExpress.XtraEditors.Repository.TrackBarLabel();
            DevExpress.XtraEditors.Repository.TrackBarLabel trackBarLabel2 = new DevExpress.XtraEditors.Repository.TrackBarLabel();
            DevExpress.XtraEditors.Repository.TrackBarLabel trackBarLabel3 = new DevExpress.XtraEditors.Repository.TrackBarLabel();
            this.tbSlider = new DevExpress.XtraEditors.TrackBarControl();
            this.picMain = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.tbSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSlider.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).BeginInit();
            this.SuspendLayout();
            // 
            // tbSlider
            // 
            this.tbSlider.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbSlider.EditValue = 100;
            this.tbSlider.Location = new System.Drawing.Point(5, 0);
            this.tbSlider.Name = "tbSlider";
            this.tbSlider.Properties.LabelAppearance.Options.UseTextOptions = true;
            this.tbSlider.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            trackBarLabel1.Label = "Original";
            trackBarLabel1.Value = 100;
            trackBarLabel2.Label = "Max Brightness";
            trackBarLabel2.Value = 1000;
            trackBarLabel3.Label = "Min Brightness";
            trackBarLabel3.Value = -50;
            this.tbSlider.Properties.Labels.AddRange(new DevExpress.XtraEditors.Repository.TrackBarLabel[] {
            trackBarLabel1,
            trackBarLabel2,
            trackBarLabel3});
            this.tbSlider.Properties.Maximum = 1000;
            this.tbSlider.Properties.Minimum = -50;
            this.tbSlider.Properties.ShowLabels = true;
            this.tbSlider.Size = new System.Drawing.Size(1151, 72);
            this.tbSlider.TabIndex = 4;
            this.tbSlider.Value = 100;
            this.tbSlider.EditValueChanged += new System.EventHandler(this.tbSlider_EditValueChanged);
            // 
            // picMain
            // 
            this.picMain.BackColor = System.Drawing.Color.White;
            this.picMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.picMain.Location = new System.Drawing.Point(5, 78);
            this.picMain.Name = "picMain";
            this.picMain.Size = new System.Drawing.Size(1151, 624);
            this.picMain.TabIndex = 0;
            this.picMain.TabStop = false;
            this.picMain.Click += new System.EventHandler(this.picMain_Click);
            this.picMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picMain_MouseClick);
            // 
            // frmZoomInImage
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 707);
            this.Controls.Add(this.tbSlider);
            this.Controls.Add(this.picMain);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmZoomInImage";
            this.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmLiveExitImage_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmLiveExitImage_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.tbSlider.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picMain;
        private DevExpress.XtraEditors.TrackBarControl tbSlider;
    }
}