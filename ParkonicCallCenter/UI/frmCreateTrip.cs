using DevExpress.Utils;
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
    public partial class frmCreateTrip : DevExpress.XtraEditors.XtraForm
    {
        public bool IsHTTPs;
        public string ServerIP;
        public string Code;
        public string PlateNo;

        public frmCreateTrip()
        {
            InitializeComponent();
        }

        private void frmCreateTrip_Load(object sender, EventArgs e)
        {
            try
            {
                ppMainWait.Size = new Size(161, 44);
                ppMainWait.Location = new Point(((this.Width / 2) - (ppMainWait.Width / 2)), ((this.Height / 2) - (ppMainWait.Height / 2)));
                ppMainWait.Visible = false;

                txtPlateCode.Text = Code;
                txtPlateNo.Text = PlateNo;
                txtPlateCode.Focus();
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error Plate Search : {0}", ee.Message));
            }
        }

        public async void btnCreateTrip_Click(object sender, EventArgs e)
        {
            try
            {
                string  city = "";
                DateTime dtTime = DateTime.Now;
                txtPlateCode.InvokeControl(l => Code = l.Text);
                txtPlateNo.InvokeControl(l => PlateNo = l.Text);
                cmbPNFCity.InvokeControl(l => city = l.Text);
                dtEntryTime.InvokeControl(l => dtTime = l.Value);

                if (city != "" && PlateNo != "")
                {
                    ppMainWait.InvokeControl(l => l.Visible = true);
                    btnCreateTrip.InvokeControl(l => l.Enabled = false);
                    LogFile.UpdateLogFile(string.Format("Manual Entry : {0}-{1} {2} at {3}", Code, PlateNo, city, dtTime));

                    //call api
                    IRestResponse resp = await APIs_DAL.CreateEntryTicket(Code, PlateNo, city, dtTime,
                        ServerIP, IsHTTPs);

                    if (resp.IsSuccessful && resp.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ppMainWait.InvokeControl(l => l.Visible = false);
                        btnCreateTrip.InvokeControl(l => l.Enabled = true);
                       
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        LogFile.UpdateLogFile(string.Format("Error : Server is not accessible for create trip api"));
                        MessageBox.Show("Server is not accessible", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                    MessageBox.Show("Please enter plate details to create the trip", "Invalid Plate Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error btnCreateTrip_Click : {0}", ee.Message));
            }
            ppMainWait.InvokeControl(l => l.Visible = false);
            btnCreateTrip.InvokeControl(l => l.Enabled = true);
        }

    }
}