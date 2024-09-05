﻿using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using Newtonsoft.Json;
using ParkonicCallCenter.Logic;
using RestSharp;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace ParkonicCallCenter.UI
{
    public partial class frmSearchedPlates : DevExpress.XtraEditors.XtraForm
    {
        bool IsHTTPs;
        string ServerIP;

        public void Show(bool _IsHTTPs, string _ServerIP)
        {
            IsHTTPs = _IsHTTPs;
            ServerIP = _ServerIP;
            this.ShowDialog();
        }

        public frmSearchedPlates()
        {
            InitializeComponent();
        }

        private void frmSearchedPlates_Load(object sender, EventArgs e)
        {
            try
            {
                ppMainWait.Size = new Size(161, 44);
                ppMainWait.Location = new Point(((this.Width / 2) - (ppMainWait.Width / 2)), ((this.Height / 2) - (ppMainWait.Height / 2)));
                ppMainWait.Visible = false;
                gcPlates.Visible = false;
                txtPlateCode.Focus();
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error Plate Search : {0}", ee.Message));
            }
        }

        private void InvokeGUIThread(Action action)
        {
            Invoke(action);
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPlateCode.Text != "" || txtPlateNo.Text != "")
                {
                    ppMainWait.Visible = true;
                    btnSearch.Enabled = false;

                    string keyword_en = "", keyword_ar = "";
                    InvokeGUIThread(() => { keyword_en = string.Format("{0}{1}", txtPlateCode.Text, txtPlateNo.Text); });
                    keyword_ar = Utilis.GetArabicPlateFromEng(keyword_en);

                    //call api
                    IRestResponse resp = await APIs_DAL.GetSearchPlateResponse(keyword_en, keyword_ar, ServerIP, IsHTTPs);
                    if (resp.IsSuccessful && resp.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        SearchDetails details = JsonConvert.DeserializeObject<SearchDetails>(resp.Content);

                        if (details != null && details.data != null && details.data.Count > 0)
                        {
                            ppMainWait.Visible = false;
                            btnSearch.Enabled = true;
                            DisplayPlatesData(details);
                        }
                        else
                            MessageBox.Show("Your car plate number was not found", "No Plate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        LogFile.UpdateLogFile(string.Format("Error : Server is not accessible for Search API"));
                        MessageBox.Show("Server is not accessible", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                    MessageBox.Show("Please enter your number plate detail to search your car", "Invalid Plate Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error Plate Search : {0}", ee.Message));
            }
            ppMainWait.Visible = false;
            btnSearch.Enabled = true;
        }

        private void DisplayPlatesData(SearchDetails details)
        {
            try
            {
                BindingList<PlatesData> lst = new BindingList<PlatesData>();
                //build image url
                foreach (PlateDetails de in details.data)
                {
                    string folderPath = de.url.Replace("\\", "/").ToLower();
                    if (folderPath.IndexOf("parkonic") >= 0)
                    {
                        folderPath = folderPath.Substring(folderPath.IndexOf("parkonic"));
                        de.url = string.Format("http{3}://{0}/{1}/{2}_frame.jpg", ServerIP,
                        folderPath, de.transactionid, IsHTTPs ? "s" : "");
                    }
                    else
                        de.url = "";

                    lst.Add(new PlatesData(de.checkinat, de.url, string.Format("{0} {1}", de.platecode, de.plate), de.city, de.transactionid));
                }

                gvPlates.Columns.Clear();

                gvPlates.OptionsView.RowAutoHeight = false;
                gvPlates.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.AnimateAllContent;
                gcPlates.DataSource = lst;

                gvPlates.OptionsView.ColumnAutoWidth = true;
                gvPlates.BestFitColumns();
                FormatSearchGridColumns();

                lst.ListChanged += (s, args) =>
                {
                    if (args.PropertyDescriptor.Name == "plateImage")
                        gvPlates.LayoutChanged();
                };
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Plates Detail List Error: {0}", ee.Message));
            }
        }

        private void FormatSearchGridColumns()
        {
            try
            {
                gcPlates.Visible = true;
                gvPlates.Columns["transID"].Visible = false;
                gvPlates.Columns["PlateImgURL"].Visible = false;

                gvPlates.Columns["checkinat"].Caption = "Entry Time";
                gvPlates.Columns["plateDetail"].Caption = "Plate Number";
                gvPlates.Columns["city"].Caption = "Emirate";
                gvPlates.Columns["plateImage"].Caption = "Plate Image";

                gvPlates.Columns["plateImage"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gvPlates.Columns["plateImage"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

                gvPlates.Columns["checkinat"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gvPlates.Columns["plateDetail"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gvPlates.Columns["city"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

                gvPlates.Columns["plateDetail"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gvPlates.Columns["city"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error FormatSearchGridColumns : {0}", ee.Message));
            }
        }

        public class PlatesData : INotifyPropertyChanged
        {
            public string checkinat { get; set; }
            public string city { get; set; }
            public string plateDetail { get; set; }
            public string transID { get; set; }
            public string PlateImgURL { get; set; }
            public Image plateImage { get; set; }
            public event PropertyChangedEventHandler PropertyChanged;

            public PlatesData(string _checkinat, string _imageUrl, string _plateDetail, string _city, string _transID)
            {
                try
                {
                    checkinat = _checkinat;
                    plateDetail = _plateDetail;
                    transID = _transID;
                    city = _city;
                    PlateImgURL = _imageUrl;

                    plateImage = ResourceImageHelper.CreateImageFromResources("DevExpress.XtraEditors.Images.loading.gif", typeof(BackgroundImageLoader).Assembly);
                    BackgroundImageLoader bg = new BackgroundImageLoader();
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                    bg.Load(_imageUrl);
                    bg.Loaded += (s, e) =>
                    {
                        plateImage = bg.Result;
                        if (!(plateImage is Image)) plateImage = ResourceImageHelper.CreateImageFromResources("DevExpress.XtraEditors.Images.Error.png", typeof(BackgroundImageLoader).Assembly);
                        PropertyChanged(this, new PropertyChangedEventArgs("plateImage"));
                        bg.Dispose();
                    };
                }
                catch (Exception ee)
                {
                    LogFile.UpdateLogFile(string.Format("Error PlatesData class : {0}", ee.Message));
                }
            }
        }

        private void gvPlates_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                PlatesData de = gvPlates.GetFocusedRow() as PlatesData;
                if (de != null && !string.IsNullOrEmpty(de.transID))
                {
                    LogFile.UpdateLogFile(string.Format("Transaction passed to the device to Init the payment : {0}", de.transID));
                    frmDashboard frmDash = (this.Tag as frmDashboard);
                    frmDash.SendSIPMessage(frmDashboard.SIPMessageType.InitTransaction, de.transID, de.PlateImgURL);
                    this.Close();
                }
                else
                    LogFile.UpdateLogFile("gvLiveExit_RowClick Row is null");
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error gvLiveExit_RowClick : {0}", ee.Message));
            }
        }

    }
}