using Newtonsoft.Json;
using ParkonicCallCenter.Logic;
using RestSharp;
using System;
using System.Drawing;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParkonicCallCenter.UI
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                GrantAccess();

                LogFile.UpdateLogFile("frmLogin_Load");
                if (Properties.Settings.Default.UserName != string.Empty)
                {
                    txtUserName.Text = Properties.Settings.Default.UserName;
                    txtPassword.Text = Properties.Settings.Default.Password;
                    chkMemberMe.Checked = true;
                }

                // pnlWait.Size = new Size(246, 50);
                pnlWait.Location = new Point(((this.Width / 2) - (pnlWait.Width / 2)), ((this.Height / 2) - (pnlWait.Height / 2)));

                this.Size = new System.Drawing.Size(770, 494);
                this.picClose.Location = new System.Drawing.Point(740, 5);
                this.pictureBox1.Location = new System.Drawing.Point(373, 51);
                this.pictureBox2.Size = new System.Drawing.Size(367, 494);
                this.pictureBox1.Size = new System.Drawing.Size(402, 60);


                txtUserName.Focus();
                txtUserName.Select(txtUserName.Text.Length, 0);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void GrantAccess()
        {
            try
            {
                string file = Application.StartupPath;
                bool exists = System.IO.Directory.Exists(file);
                if (!exists)
                {
                    DirectoryInfo di = System.IO.Directory.CreateDirectory(file);
                    Console.WriteLine("The Folder is created Sucessfully");
                }
                else
                {
                    Console.WriteLine("The Folder already exists");
                }
                DirectoryInfo dInfo = new DirectoryInfo(file);
                DirectorySecurity dSecurity = dInfo.GetAccessControl();
                dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                    FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                dInfo.SetAccessControl(dSecurity);
            }
            catch (Exception ee)
            {
                MessageBox.Show(string.Format("Open application using Run as Administrator.\n{0}", ee.Message), "Required Admin Access", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            //  Exit();
        }

        private void Exit()
        {
            Application.Exit();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                LogFile.UpdateLogFile("btnLogin_Click");
                if (ValidateForm())
                {
                    pnlWait.Visible = ppWait.Visible = true;

                    IRestResponse resp = await AWS_APIs.GetAuthenticationResponce(txtUserName.Text.Trim(), txtPassword.Text);

                    LogFile.UpdateLogFile("Api Resp " + resp.StatusCode + " " + resp.StatusDescription + " " + resp.IsSuccessful + " " + resp.Content);
                    if (resp.IsSuccessful && resp.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        LoginResponce rest = JsonConvert.DeserializeObject<LoginResponce>(resp.Content);
                        if (rest != null && rest.status)
                        {
                            Utilis.UserName = txtUserName.Text.Trim();
                            Utilis.Password = txtPassword.Text;
                            Utilis.UserFullName = rest.data.name;
                            Utilis.AWSToken = rest.data.token;
                            Utilis.CallTypes = rest.data.call_types;

                            SaveCredentialSettings();
                            this.Hide();
                            frmDashboard frm = new frmDashboard();
                            frm.Show();
                        }
                        else
                        {
                            MessageBox.Show(string.Format("Invalid User Name and Password"), "Invalid Credentials", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtUserName.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show(string.Format("Server is not accessible"), "Out of Reach", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(string.Format("Error : {0}", ee.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            pnlWait.Visible = ppWait.Visible = false;
        }

        private bool ValidateForm()
        {
            if (txtUserName.Text.Trim() == "")
            {
                MessageBox.Show("Username is required", "Invalid Credentials", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserName.Focus();
                return false;
            }
            else if (txtPassword.Text == "")
            {
                MessageBox.Show("Password is required", "Invalid Credentials", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }
            return true;
        }

        private void SaveCredentialSettings()
        {
            Task.Run(() =>
            {
                if (chkMemberMe.Checked)
                {
                    Properties.Settings.Default.UserName = txtUserName.Text;
                    Properties.Settings.Default.Password = txtPassword.Text;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Properties.Settings.Default.UserName = "";
                    Properties.Settings.Default.Password = "";
                    Properties.Settings.Default.Save();
                }
            });
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            Exit();
        }
    }
}