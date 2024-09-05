namespace ParkonicCallCenter.UI
{
    partial class frmSearchedPlates
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.septLine = new DevExpress.XtraEditors.SeparatorControl();
            this.gcPlates = new DevExpress.XtraGrid.GridControl();
            this.gvPlates = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ppMainWait = new DevExpress.XtraWaitForm.ProgressPanel();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlateCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlateNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.septLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPlates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPlates)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPlateCode
            // 
            this.txtPlateCode.EditValue = "";
            this.txtPlateCode.Location = new System.Drawing.Point(26, 68);
            this.txtPlateCode.Name = "txtPlateCode";
            this.txtPlateCode.Properties.Appearance.Font = new System.Drawing.Font("Poppins SemiBold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlateCode.Properties.Appearance.Options.UseFont = true;
            this.txtPlateCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtPlateCode.Properties.NullText = "Code";
            this.txtPlateCode.Properties.NullValuePrompt = "Code";
            this.txtPlateCode.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtPlateCode.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPlateCode.Size = new System.Drawing.Size(234, 48);
            this.txtPlateCode.TabIndex = 0;
            // 
            // txtPlateNo
            // 
            this.txtPlateNo.EditValue = "";
            this.txtPlateNo.Location = new System.Drawing.Point(309, 68);
            this.txtPlateNo.Name = "txtPlateNo";
            this.txtPlateNo.Properties.Appearance.Font = new System.Drawing.Font("Poppins SemiBold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlateNo.Properties.Appearance.Options.UseFont = true;
            this.txtPlateNo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtPlateNo.Properties.NullText = "Plate Number";
            this.txtPlateNo.Properties.NullValuePrompt = "Plate Number";
            this.txtPlateNo.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtPlateNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPlateNo.Size = new System.Drawing.Size(234, 48);
            this.txtPlateNo.TabIndex = 1;
            // 
            // lblDeviceName
            // 
            this.lblDeviceName.AutoSize = true;
            this.lblDeviceName.Font = new System.Drawing.Font("Poppins", 14F);
            this.lblDeviceName.Location = new System.Drawing.Point(20, 30);
            this.lblDeviceName.Name = "lblDeviceName";
            this.lblDeviceName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblDeviceName.Size = new System.Drawing.Size(120, 33);
            this.lblDeviceName.TabIndex = 38;
            this.lblDeviceName.Text = "Plate Code";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Poppins", 14F);
            this.label1.Location = new System.Drawing.Point(303, 30);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(145, 33);
            this.label1.TabIndex = 39;
            this.label1.Text = "Plate Number";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(25)))), ((int)(((byte)(54)))));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(25)))), ((int)(((byte)(54)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(25)))), ((int)(((byte)(54)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Poppins", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(592, 69);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(234, 48);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "SEARCH";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // septLine
            // 
            this.septLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.septLine.LineStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.septLine.LineThickness = 1;
            this.septLine.Location = new System.Drawing.Point(26, 131);
            this.septLine.Name = "septLine";
            this.septLine.Size = new System.Drawing.Size(800, 2);
            this.septLine.TabIndex = 47;
            // 
            // gcPlates
            // 
            this.gcPlates.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gcPlates.Location = new System.Drawing.Point(0, 152);
            this.gcPlates.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.gcPlates.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gcPlates.MainView = this.gvPlates;
            this.gcPlates.Name = "gcPlates";
            this.gcPlates.Size = new System.Drawing.Size(851, 483);
            this.gcPlates.TabIndex = 48;
            this.gcPlates.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPlates});
            // 
            // gvPlates
            // 
            this.gvPlates.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gvPlates.Appearance.Empty.Options.UseBackColor = true;
            this.gvPlates.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gvPlates.Appearance.EvenRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.gvPlates.Appearance.EvenRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.gvPlates.Appearance.EvenRow.Options.UseBackColor = true;
            this.gvPlates.Appearance.EvenRow.Options.UseFont = true;
            this.gvPlates.Appearance.EvenRow.Options.UseForeColor = true;
            this.gvPlates.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.gvPlates.Appearance.FilterCloseButton.Options.UseFont = true;
            this.gvPlates.Appearance.FilterPanel.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.gvPlates.Appearance.FilterPanel.Options.UseFont = true;
            this.gvPlates.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gvPlates.Appearance.FocusedCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.gvPlates.Appearance.FocusedCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.gvPlates.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvPlates.Appearance.FocusedCell.Options.UseFont = true;
            this.gvPlates.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvPlates.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gvPlates.Appearance.FocusedRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.gvPlates.Appearance.FocusedRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.gvPlates.Appearance.FocusedRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.gvPlates.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvPlates.Appearance.FocusedRow.Options.UseBorderColor = true;
            this.gvPlates.Appearance.FocusedRow.Options.UseFont = true;
            this.gvPlates.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvPlates.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(25)))), ((int)(((byte)(54)))));
            this.gvPlates.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(25)))), ((int)(((byte)(54)))));
            this.gvPlates.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.Transparent;
            this.gvPlates.Appearance.HeaderPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.gvPlates.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvPlates.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvPlates.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvPlates.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvPlates.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gvPlates.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.gvPlates.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.gvPlates.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gvPlates.Appearance.HideSelectionRow.Options.UseFont = true;
            this.gvPlates.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gvPlates.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gvPlates.Appearance.OddRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.gvPlates.Appearance.OddRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.gvPlates.Appearance.OddRow.Options.UseBackColor = true;
            this.gvPlates.Appearance.OddRow.Options.UseFont = true;
            this.gvPlates.Appearance.OddRow.Options.UseForeColor = true;
            this.gvPlates.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gvPlates.Appearance.Preview.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.gvPlates.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.gvPlates.Appearance.Preview.Options.UseBackColor = true;
            this.gvPlates.Appearance.Preview.Options.UseFont = true;
            this.gvPlates.Appearance.Preview.Options.UseForeColor = true;
            this.gvPlates.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gvPlates.Appearance.Row.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.gvPlates.Appearance.Row.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.gvPlates.Appearance.Row.Options.UseBackColor = true;
            this.gvPlates.Appearance.Row.Options.UseFont = true;
            this.gvPlates.Appearance.Row.Options.UseForeColor = true;
            this.gvPlates.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.gvPlates.Appearance.RowSeparator.ForeColor = System.Drawing.Color.Black;
            this.gvPlates.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gvPlates.Appearance.SelectedRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.gvPlates.Appearance.SelectedRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.gvPlates.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gvPlates.Appearance.SelectedRow.Options.UseFont = true;
            this.gvPlates.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gvPlates.Appearance.TopNewRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gvPlates.Appearance.TopNewRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.gvPlates.Appearance.TopNewRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.gvPlates.Appearance.TopNewRow.Options.UseBackColor = true;
            this.gvPlates.Appearance.TopNewRow.Options.UseFont = true;
            this.gvPlates.Appearance.TopNewRow.Options.UseForeColor = true;
            this.gvPlates.Appearance.VertLine.BackColor = System.Drawing.Color.Transparent;
            this.gvPlates.Appearance.VertLine.ForeColor = System.Drawing.Color.Black;
            this.gvPlates.Appearance.VertLine.Options.UseBackColor = true;
            this.gvPlates.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gvPlates.ColumnPanelRowHeight = 60;
            this.gvPlates.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gvPlates.DetailHeight = 400;
            this.gvPlates.FixedLineWidth = 1;
            this.gvPlates.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.gvPlates.GridControl = this.gcPlates;
            this.gvPlates.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            this.gvPlates.Name = "gvPlates";
            this.gvPlates.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvPlates.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvPlates.OptionsBehavior.Editable = false;
            this.gvPlates.OptionsBehavior.ReadOnly = true;
            this.gvPlates.OptionsCustomization.AllowFilter = false;
            this.gvPlates.OptionsCustomization.AllowSort = false;
            this.gvPlates.OptionsFind.AllowFindPanel = false;
            this.gvPlates.OptionsFind.ClearFindOnClose = false;
            this.gvPlates.OptionsFind.FindDelay = 100;
            this.gvPlates.OptionsFind.ShowClearButton = false;
            this.gvPlates.OptionsFind.ShowCloseButton = false;
            this.gvPlates.OptionsFind.ShowFindButton = false;
            this.gvPlates.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvPlates.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            this.gvPlates.OptionsView.EnableAppearanceEvenRow = true;
            this.gvPlates.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gvPlates.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gvPlates.OptionsView.ShowGroupPanel = false;
            this.gvPlates.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.True;
            this.gvPlates.OptionsView.ShowIndicator = false;
            this.gvPlates.OptionsView.ShowPreviewRowLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvPlates.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvPlates.RowHeight = 60;
            this.gvPlates.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveVertScroll;
            this.gvPlates.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvPlates_RowClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.BorderColor = System.Drawing.Color.Transparent;
            this.gridColumn1.AppearanceHeader.Options.UseBorderColor = true;
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 175;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "gridColumn2";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 676;
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
            this.ppMainWait.Location = new System.Drawing.Point(506, -3);
            this.ppMainWait.Name = "ppMainWait";
            this.ppMainWait.Size = new System.Drawing.Size(246, 66);
            this.ppMainWait.TabIndex = 82;
            this.ppMainWait.WaitAnimationType = DevExpress.Utils.Animation.WaitingAnimatorType.Ring;
            // 
            // frmSearchedPlates
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 635);
            this.Controls.Add(this.ppMainWait);
            this.Controls.Add(this.gcPlates);
            this.Controls.Add(this.septLine);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDeviceName);
            this.Controls.Add(this.txtPlateCode);
            this.Controls.Add(this.txtPlateNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSearchedPlates";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search Vehicle Plate";
            this.Load += new System.EventHandler(this.frmSearchedPlates_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtPlateCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlateNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.septLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPlates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPlates)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtPlateCode;
        private DevExpress.XtraEditors.TextEdit txtPlateNo;
        private System.Windows.Forms.Label lblDeviceName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private DevExpress.XtraEditors.SeparatorControl septLine;
        private DevExpress.XtraGrid.GridControl gcPlates;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPlates;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        public DevExpress.XtraWaitForm.ProgressPanel ppMainWait;
        //   private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
    }
}