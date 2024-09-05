using Newtonsoft.Json;
using Ozeki.Media.MediaHandlers;
using Ozeki.Media.MediaHandlers.Video;
using Ozeki.Media.Video.Controls;
using Ozeki.VoIP;
using Ozeki.VoIP.SDK;
using Ozeki.VoIP.SDK.Protection;
using Ozeki.VoIP.SIP;
using ParkonicCallCenter.Logic;
using ParkonicCallCenter.Properties;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Media;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Linq;
using Ozeki.Media;


namespace ParkonicCallCenter.UI
{
    public partial class frmDashboard : DevExpress.XtraEditors.XtraForm
    {
        #region Global Variables

        //misc
        System.Timers.Timer tmCounter;
        System.Timers.Timer tmCallDuration;
        SoundPlayer spRingTone;
        private ISoftPhone softPhone;
        private IPhoneLine phoneLine;
        private RegState SIPAcConnectivityState;
        private IPhoneCall call;
        private IPhoneCall callWaiting;

        //for audio calling
        private Microphone microphone = Microphone.GetDefaultDevice();
        private Speaker speaker = Speaker.GetDefaultDevice();
        MediaConnector connector = new MediaConnector();
        PhoneCallAudioSender mediaSender = new PhoneCallAudioSender();
        PhoneCallAudioReceiver mediaReceiver = new PhoneCallAudioReceiver();
        private bool inComingCall;

        //for video call
        PhoneCallVideoSender mediaSenderVid = new PhoneCallVideoSender();
        PhoneCallVideoReceiver mediaReceiverVid = new PhoneCallVideoReceiver();
        private DrawingImageProvider _imageProvider = new DrawingImageProvider();
        private VideoViewerWF _videoViewer;
        private WebCamera webCamera = WebCamera.GetDefaultDevice();

        #endregion

        #region General 

        public frmDashboard()
        {
            InitializeComponent();
            //for video calling
            _videoViewer = new VideoViewerWF();
            _videoViewer.Name = "videoViewer1";
            pnlVideoCall.Controls.Add(_videoViewer);
            _videoViewer.SetImageProvider(_imageProvider);
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            try
            {
                InitInterCom();
                spRingTone = new SoundPlayer(Properties.Resources.OldPhoneRingTone);
                FillCallTypes();

                LoadAccessPointsData();
                FillDevices(Convert.ToInt32(ConfigurationManager.AppSettings["DevicesLocID"]));
                FillLocations();

                ResetControlValues();
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error frmDashboard_Load : {0}", ee.Message));
            }
        }

        SitesAndAccessPointsResponse APdata = new SitesAndAccessPointsResponse();
        private async void LoadAccessPointsData()
        {
            try
            {
                IRestResponse resp = await AWS_APIs.GetSitesAndAccessPoints();
                if (resp.IsSuccessful && resp.StatusCode == HttpStatusCode.OK)
                {
                    APdata = JsonConvert.DeserializeObject<SitesAndAccessPointsResponse>(resp.Content);
                }
                else
                {
                    MessageBox.Show(string.Format("Sites API is not accessible"), "Out of Reach", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(string.Format("Sites API Error : {0}", ee.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillAccessPoints(int LocID)
        {
            try
            {
                if (APdata != null && APdata.status)
                {
                    LocationAWS lo = APdata.data.FirstOrDefault(l => l.id == LocID);

                    //access points
                    List<AccessPoint> lstAP = new List<AccessPoint>();

                    foreach (AccessPoint ap in lo.gates)
                    {
                        lstAP.Add(new AccessPoint()
                        {
                            id = ap.id,
                            name = ap.name,// string.Format("{0} - {1} - {2}", ap.id, ap.name, lo.name),
                            is_exit = ap.is_exit
                        });
                    }

                    lstAP = lstAP.AsEnumerable().Select(ap => new AccessPoint()
                    {
                        id = ap.id,
                        name = ap.name,
                        is_exit = ap.is_exit
                    }).OrderBy(i => i.name).ToList();

                    cmbBarrier.DisplayMember = "name";
                    cmbBarrier.ValueMember = "id";
                    cmbBarrier.DataSource = lstAP;
                    cmbBarrier.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show(string.Format("Error while loading location and access points data."), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(string.Format("Sites API Error : {0}", ee.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetLocationIP(int LocID)
        {
            return grpLoc.Where(l => l.id == LocID).FirstOrDefault().ip;
        }

        List<GrpLocation> grpLoc = new List<GrpLocation>();
        private async void FillLocations()
        {
            try
            {
                IRestResponse resp = await AWS_APIs.GetLocationsByGroupResponse(Convert.ToInt32(ConfigurationManager.AppSettings["LocationGroupID"]));
                if (resp.IsSuccessful && resp.StatusCode == HttpStatusCode.OK)
                {
                    LocationGroups rest = JsonConvert.DeserializeObject<LocationGroups>(resp.Content);
                    if (rest != null && rest.status)
                    {
                        grpLoc = rest.data;
                        cmbLocation.DisplayMember = "name";
                        cmbLocation.ValueMember = "id";
                        cmbLocation.DataSource = grpLoc;
                    }
                    else
                        LogFile.UpdateLogFile(string.Format("Server is not accessible"));
                }
                else
                    LogFile.UpdateLogFile(string.Format("Server is not accessible"));
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error FillLocations : {0}", ee.Message));
            }
        }

        private void FillCallTypes()
        {
            try
            {
                cmbCallType.DataSource = Utilis.CallTypes;
                cmbCallType.DisplayMember = "name";
                cmbCallType.ValueMember = "id";
                cmbCallType.SelectedIndex = 0;
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error FillCallTypes : {0}", ee.Message));
            }
        }

        private void frmDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void picStatus_Click(object sender, EventArgs e)
        {
            try
            {
                if (call == null)
                    CreateSIPAccount();

                //frmSearchedPlates frm = new frmSearchedPlates();
                //frm.Tag = this;
                //frm.Show(false, "192.168.25.100");
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error picStatus_Click : {0}", ee.Message));
            }
        }

        #endregion

        #region Intercom Call Logic

        private void SetInterComValues()
        {
            try
            {
                SIPAccountConfig.DisplayName = ConfigurationManager.AppSettings["DisplayName"];
                SIPAccountConfig.UserName = ConfigurationManager.AppSettings["UserName"];
                SIPAccountConfig.RegisterName = ConfigurationManager.AppSettings["RegisterName"];
                SIPAccountConfig.RegisterPassword = ConfigurationManager.AppSettings["RegisterPassword"];
                SIPAccountConfig.DomainHost = ConfigurationManager.AppSettings["DomainHost"];
                SIPAccountConfig.DomainPort = Convert.ToInt32(ConfigurationManager.AppSettings["DomainPort"]);
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error SetInterComValues : {0}", ee.Message));
            }
        }

        private void InitInterCom()
        {
            try
            {
                SetInterComValues();
                LogFile.UpdateLogFile(string.Format("IP : {0}", SoftPhoneFactory.GetLocalIP().ToString()));
                LicenseManager.Instance.SetLicense(ConfigurationManager.AppSettings["SipUserID"], ConfigurationManager.AppSettings["SipKey"]);
                softPhone = SoftPhoneFactory.CreateSoftPhone(5000, 10000);//SoftPhoneFactory.GetLocalIP().ToString(),
                softPhone.IncomingCall += softPhone_inComingCall;
                CreateSIPAccount();
                ConnectMedia();
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error InitInterCom : {0}", ee.Message));
            }
        }

        private void ConnectMedia()
        {
            try
            {
                if (microphone != null)
                    connector.Connect(microphone, mediaSender);

                if (speaker != null)
                    connector.Connect(mediaReceiver, speaker);

                if (webCamera != null)
                    connector.Connect(webCamera, mediaSenderVid);

                if (_imageProvider != null)
                    connector.Connect(mediaReceiverVid, _imageProvider);
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error ConnectMedia : {0}", ee.Message));
            }
        }

        private void CreateSIPAccount()
        {
            try
            {
                if (phoneLine != null)
                {
                    phoneLine.RegistrationStateChanged -= phoneLine_RegistrationStateChanged;
                    phoneLine.InstantMessaging.MessageReceived -= InstantMessaging_MessageReceived;
                    //   phoneLine.InstantMessaging.ResponseReceived -= InstantMessaging_ResponseReceived;
                    phoneLine = null;
                }

                SIPAccount sa = new SIPAccount(true, SIPAccountConfig.DisplayName, SIPAccountConfig.UserName,
                    SIPAccountConfig.RegisterName, SIPAccountConfig.RegisterPassword, SIPAccountConfig.DomainHost, SIPAccountConfig.DomainPort);
                phoneLine = softPhone.CreatePhoneLine(sa);
                phoneLine.RegistrationStateChanged += phoneLine_RegistrationStateChanged;

                phoneLine.InstantMessaging.MessageReceived += InstantMessaging_MessageReceived;
                // phoneLine.InstantMessaging.ResponseReceived += InstantMessaging_ResponseReceived;

                softPhone.RegisterPhoneLine(phoneLine);

                InvokeGUIThread(() =>
                {
                    lblCallDuration.Text = lblCallingNo.Text = "";
                    pnlVideoAndPics.Visible = pnlCallerDetail.Visible = pnlIncomingCall.Visible = false;
                });

            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error CreateSIPAccount : {0}", ee.Message));
            }
        }

        private void phoneLine_RegistrationStateChanged(object sender, RegistrationStateChangedArgs e)
        {
            try
            {
                SIPAcConnectivityState = e.State;
                LogFile.UpdateLogFile(string.Format("SIP Account registration info : {0}", SIPAcConnectivityState.ToString()));

                if (SIPAcConnectivityState == RegState.Error || SIPAcConnectivityState == RegState.NotRegistered || SIPAcConnectivityState == RegState.UnregRequested)
                {
                    InvokeGUIThread(() =>
                    {
                        picStatus.BackgroundImage = Resources.StatusInActive;
                        lblMyExt.Text = "";
                    });

                    CreateSIPAccount();
                }
                else if (SIPAcConnectivityState == RegState.RegistrationRequested)
                {
                    InvokeGUIThread(() =>
                    {
                        lblMyExt.Text = "";
                        picStatus.BackgroundImage = Resources.StatusInActive;
                    });
                }
                else // RegistrationSucceeded
                {
                    //   softPhone.VideoEncoderQuality = Ozeki.Media.Video.VideoQuality.Medium;

                    //foreach (var codec in softPhone.Codecs)
                    //{
                    //    softPhone.DisableCodec(codec.PayloadType);

                    //    if (codec.PayloadType == 8)
                    //        softPhone.EnableCodec(codec.PayloadType);
                    //}

                    InvokeGUIThread(() =>
                    {
                        lblMyExt.Text = SIPAccountConfig.UserName;
                        picStatus.BackgroundImage = Resources.StatusActive;
                    });
                }
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error phoneLine_RegistrationStateChanged : {0}", ee.Message));
            }
        }

        private void InvokeGUIThread(Action action)
        {
            Invoke(action);
        }

        private void InitInComingCall(IPhoneCall _call)
        {
            try
            {
                call = _call;
                WireUpCallEvents();
                inComingCall = true;

                InvokeGUIThread(() => { lblCallingNo.Text = string.Format("{0} | {1}", call.DialInfo.CallerID, call.DialInfo.CallerDisplay); });
                ArrangeIncomingCall();
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error InitInComingCall : {0}", ee.Message));
            }
        }

        private void ArrangeIncomingCall()
        {
            try
            {
                InvokeGUIThread(() =>
                {
                    pnlIncomingCall.Size = new Size(509, 438);
                    pnlIncomingCall.Location = new Point(((this.Width / 2) - (pnlIncomingCall.Width / 2)), ((this.Height / 2) - (pnlIncomingCall.Height / 2)));
                    pnlIncomingCall.Visible = true;
                });
                InvokeGUIThread(() => { pnlCallerDetail.Visible = pnlVideoAndPics.Visible = false; });
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error ArrangeIncomingCall : {0}", ee.Message));
            }
        }

        private void softPhone_inComingCall(object sender, VoIPEventArgs<IPhoneCall> e)
        {
            try
            {
                LogFile.UpdateLogFile(string.Format("Incoming call from : {0}", e.Item.DialInfo.CallerID.ToString()));
                if (call == null)
                {
                    InitInComingCall(e.Item);
                }
                else if (e.Item != null && ConfigurationManager.AppSettings["CallForward"].ToString() != "") // in case of already on call, forward call to given no, 
                {
                    e.Item.Forward(ConfigurationManager.AppSettings["CallForward"].ToString());
                }
                else if (e.Item != null && ConfigurationManager.AppSettings["CallForward"].ToString() == "" && callWaiting == null)
                // in case of already on call, but no forward option is avaiable, than show to user this call is on hold
                {
                    LogFile.UpdateLogFile(string.Format("Call Waiting : {0}", e.Item.DialInfo.CallerID.ToString()));
                    callWaiting = e.Item;
                    callWaiting.CallStateChanged += callWaiting_CallStateChanged;

                    InvokeGUIThread(() =>
                    {
                        pnlCallForward.Location = new Point(((this.Width / 3) - (pnlCallForward.Width / 2)), 0);
                        lblCallWaitinginfo.Text = string.Format("{0} | {1}", e.Item.DialInfo.CallerID, e.Item.DialInfo.CallerDisplay);
                        pnlCallForward.Visible = true;
                    });
                }
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error softPhone_inComingCall : {0}", ee.Message));
            }
        }

        private void ArrangeAndDisplayCallerDetailPanel()
        {
            try
            {
                InvokeGUIThread(() =>
                {
                    pnlIncomingCall.Visible = false;
                    pnlVideoAndPics.Width = this.Width / 3;

                    pnlCallerDetail.Height = this.Height - grpHeader.Location.Y - grpHeader.Height - 60;
                    pnlCallerDetail.Visible = pnlVideoAndPics.Visible = true;

                    //bottom buttons
                    btnDispute.Width = btnHangUp.Width = pnlCallerDetail.Width / 3;

                    int by = pnlCallerDetail.Height - btnDispute.Height;
                    btnDispute.Location = new Point((pnlCallerDetail.Width / 7), by);
                    btnHangUp.Location = new Point((int)(pnlCallerDetail.Width / 1.8), by);

                    //search
                    septUpperLoc.Width = pnlCashDetail.Width = septLocDet.Width = pnlCallerDetail.Width - 5;

                    //right panel
                    pnlVideoCall.Height = (pnlVideoAndPics.Height / 2) - 10;
                    picPlateImage.Location = new Point(0, pnlVideoCall.Height + 5);
                    picCarFrame.Height = pnlVideoAndPics.Height - picPlateImage.Location.Y - picPlateImage.Height - 5;
                    _videoViewer.Size = pnlVideoCall.Size;

                    pnlActions.Location = new Point(pnlCallerDetail.Width - pnlActions.Width - 11, 2);
                });

                InvokeGUIThread(() =>
                {
                    txtForwardNo.Text = txtMainCallForward.Text = txtRemarks.Text = "";
                    cmbCallType.SelectedIndex = 0;
                    txtRemarks.Focus();
                });
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error ArrangeAndDisplayCallerDetailPanel : {0}", ee.Message));
            }
        }

        private void WireUpCallEvents()
        {
            try
            {
                call.CallStateChanged += (call_CallStateChanged);
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error WireUpCallEvents : {0}", ee.Message));
            }
        }

        DateTime dtCallStartTime;
        private void call_CallStateChanged(object sender, CallStateChangedArgs e)
        {
            try
            {
                LogFile.UpdateLogFile(string.Format("Call state changed : {0}", e.State.ToString()));

                if (e.State == CallState.Ringing || e.State == CallState.RingingWithEarlyMedia)
                {
                    spRingTone.PlayLooping();
                }
                else if (e.State == CallState.Answered)
                {
                    spRingTone.Stop();
                    ConnectDevicesToCall();

                    //show call timing
                    dtCallStartTime = DateTime.Now;
                    tmCallDuration = new System.Timers.Timer(1000);
                    tmCallDuration.Elapsed += tmCallDuration_Elapsed;
                    tmCallDuration.Enabled = true;

                    ArrangeAndDisplayCallerDetailPanel();
                    LogFile.UpdateLogFile(string.Format("Call Started."));

                    callInfo = new SIPInWardMessageDetail()
                    {
                        IsBNRInfo = false,
                        IsLocationInfo = false,
                        IsTripInfo = false,
                        IsTripExist = false,
                        AccessPointID = 0,
                        TransactionID = "",
                        CallIDAWS = 0,
                        OpenBarrier = 0,
                        IsHTTPs = 0,
                        LocalServerIP = ""
                    };
                    SendSIPMessage(SIPMessageType.ReceiverSIPNo, SIPAccountConfig.UserName);
                }
                else if (e.State == CallState.Completed || e.State == CallState.Cancelled || e.State == CallState.Error)
                {
                    if (e.State == CallState.Completed)
                        DisconnectDevicesFromCall();
                    // else if (e.State == CallState.Error)
                    //CreateSIPAccount();

                    spRingTone.Stop();
                    WireDownCallEvents();
                    call = null;

                    InvokeGUIThread(() =>
                    {
                        pnlVideoAndPics.Visible = pnlCallerDetail.Visible = pnlIncomingCall.Visible = false;
                        lblCallDuration.Text = lblCallingNo.Text = "";
                    });

                    if (tmCallDuration != null)
                    {
                        tmCallDuration.Elapsed -= tmCallDuration_Elapsed;
                        tmCallDuration.Enabled = false;
                    }

                    if (callInfo != null && callInfo.CallIDAWS != 0)
                        SendCallHangUpDetail();
                    callInfo = null;

                    LogFile.UpdateLogFile(string.Format("Call Ended."));

                    CreateSIPAccount();
                    ResetControlValues();
                }
                else if (call != null && call.CallState != CallState.Ringing)
                {
                    spRingTone.Stop();
                }
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error call_CallStateChanged : {0}", ee.Message));
            }
        }

        private void ResetControlValues()
        {
            try
            {
                InvokeGUIThread(() =>
                {
                    picPlateImage.BackgroundImage = picCarFrame.BackgroundImage = Resources.NoImage;

                    lblLocationName.Text = lblEntryExitName.Text = lblDeviceName.Text = "";

                    lblPlateDetail.Text = lblEntryTime.Text = lblDuration.Text =
                    lblExitTime.Text = lblWalletBalance.Text = lblTripAmount.Text = "";

                    lbl10AEDCount.Text = lbl20AEDCount.Text = lblLastCashPaymentPlate.Text =
                    lblPaymentTime.Text = lblCashPaymentAmount.Text = lblDepositedAmount.Text = lblReturnedAmount.Text = "";

                    cmbLocation.SelectedIndex = cmbCallType.SelectedIndex = cmbDevice.SelectedIndex = cmbBarrier.SelectedIndex = -1;
                    txtRemarks.Text = "";
                });
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error ResetControlsValues : {0}", ee.Message));
            }
        }

        private void ConnectDevicesToCall()
        {
            try
            {
                if (microphone != null)
                    microphone.Start();
                connector.Connect(microphone, mediaSender);

                if (speaker != null)
                    speaker.Start();
                connector.Connect(mediaReceiver, speaker);

                mediaSender.AttachToCall(call);
                mediaReceiver.AttachToCall(call);

                //for video call
                if (webCamera != null)
                    webCamera.Start();

                if (_videoViewer != null)
                    _videoViewer.Start();

                connector.Connect(webCamera, mediaSenderVid);
                connector.Connect(mediaReceiverVid, _imageProvider);

                mediaReceiverVid.AttachToCall(call);
                mediaSenderVid.AttachToCall(call);
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error ConnectDevicesToCall : {0}", ee.Message));
            }
        }

        private void DisconnectDevicesFromCall()
        {
            try
            {
                if (microphone != null)
                    microphone.Stop();
                connector.Disconnect(microphone, mediaSender);

                if (speaker != null)
                    speaker.Stop();
                connector.Disconnect(mediaReceiver, speaker);

                mediaSender.Detach();
                mediaReceiver.Detach();

                //for video call
                if (webCamera != null)
                    webCamera.Stop();
                connector.Disconnect(webCamera, mediaSenderVid);

                if (_videoViewer != null)
                    _videoViewer.Stop();
                connector.Disconnect(mediaReceiverVid, _imageProvider);

                mediaSenderVid.Detach();
                mediaReceiverVid.Detach();
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error DisconnectDevicesFromCall : {0}", ee.Message));
            }
        }

        private void WireDownCallEvents()
        {
            try
            {
                if (call != null)
                    call.CallStateChanged -= (call_CallStateChanged);
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error WireDownCallEvents : {0}", ee.Message));
            }
        }

        private void TmCounter_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                //if (tmCounter != null && call != null && call.CallState == CallState.Ringing)
                //{
                //    btnHangUp_Click(sender, new EventArgs());
                //    tmCounter.Elapsed -= TmCounter_Elapsed;
                //    tmCounter.Enabled = false;
                //    return;
                //}
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error TmCounter_Elapsed : {0}", ee.Message));
            }
        }

        private void tmCallDuration_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                TimeSpan span = DateTime.Now.Subtract(dtCallStartTime);
                InvokeGUIThread(() =>
                {
                    lblCallDuration.Text = string.Format("{0}:{1}", Math.Floor(span.TotalMinutes).ToString("00"), span.Seconds.ToString("00"));
                });

            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error tmCallDuration_Elapsed : {0}", ee.Message));
            }
        }

        private void btnHangUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (call != null)
                {
                    if (inComingCall && call.CallState == CallState.Ringing)
                    {
                        call.Reject();
                        LogFile.UpdateLogFile(string.Format("Call Rejected"));
                    }
                    else
                    {
                        call.HangUp();
                        LogFile.UpdateLogFile(string.Format("Call Hanged Up"));
                    }
                    inComingCall = false;
                    call = null;
                }
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error btnHangUp_Click : {0}", ee.Message));
            }
        }

        private void btnAnswer_Click(object sender, EventArgs e)
        {
            try
            {
                if (inComingCall)
                {
                    inComingCall = false;
                    //InvokeGUIThread(() => { pnlIncomingCall.Visible = false; });
                    //ArrangeDetailPanel();
                    call.Answer();
                    LogFile.UpdateLogFile(string.Format("Call Accepted"));
                    return;
                }
                else if (call != null)
                {
                    LogFile.UpdateLogFile(string.Format("Hang up existing call to make new call"));
                    return;
                }
                else if (SIPAcConnectivityState == RegState.Error || SIPAcConnectivityState == RegState.NotRegistered || SIPAcConnectivityState == RegState.UnregRequested)
                {
                    picStatus.BackgroundImage = Resources.StatusInActive;
                    CreateSIPAccount();
                    LogFile.UpdateLogFile(string.Format("Registration Failed!"));
                    return;
                }
                else if (SIPAcConnectivityState == RegState.RegistrationRequested)
                {
                    picStatus.BackgroundImage = Resources.StatusInActive;
                    LogFile.UpdateLogFile(string.Format("Registration Requested"));
                    return;
                }
                //else if (txtMakeCall.Text.Trim() != "")
                //{
                //    CallingNo = txtDialedNo.Text;
                //    StartCall(txtDialedNo.Text);
                //}

            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error btnAnswer_Click : {0}", ee.Message));
            }
        }

        private void StartCall(string Number)
        {
            try
            {
                call = softPhone.CreateCallObject(phoneLine, new DialParameters(Number) { CallType = CallType.Audio });
                WireUpCallEvents();
                call.Start();
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error StartCall : {0}", ee.Message));
            }
        }

        private void btnMainCallForward_Click(object sender, EventArgs e)
        {
            try
            {
                string CallFrwrdNo = "";
                InvokeGUIThread(() => { CallFrwrdNo = txtMainCallForward.Text.Trim(); });

                if (CallFrwrdNo != "")
                {
                    LogFile.UpdateLogFile(string.Format("Info Main Call Transferred to : {0}", CallFrwrdNo));
                    call.TransferStateChanged += Call_TransferStateChanged;
                    call.BlindTransfer(CallFrwrdNo);
                }
                else
                {
                    InvokeGUIThread(() => { txtMainCallForward.Focus(); });
                    MessageBox.Show("Please enter valid number to transfer the call", "Invalid Extention No", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error btnMainCallForward_Click : {0}", ee.Message));
            }
        }

        private void Call_TransferStateChanged(object sender, VoIPEventArgs<TransferState> e)
        {
            LogFile.UpdateLogFile(string.Format("Call Transfer Status: {0}", e.Item.ToString()));
            //if (e.Item == TransferState.Accepted)
            //{
            //    LogFile.UpdateLogFile(string.Format("Call accepted by another person disconnect from here"));
            //}
        }

        #endregion

        #region User Actions

        private async void btnDispute_Click(object sender, EventArgs e)
        {
            try
            {
                if (callInfo != null && callInfo.TransactionID != "")
                {
                    if (cmbCallType.SelectedIndex < 0)
                    {
                        InvokeGUIThread(() => { cmbCallType.Focus(); });
                        MessageBox.Show("Please select the type", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (txtRemarks.Text.Trim() == "")
                    {
                        InvokeGUIThread(() => { txtRemarks.Focus(); });
                        MessageBox.Show("Please enter remarks", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string remarks = "";
                    InvokeGUIThread(() => { remarks = string.Format("Call Center Remarks : {0}", txtRemarks.Text.Trim()); });

                    IRestResponse resp = await APIs_DAL.ConfirmPayment(callInfo.TransactionID.ToString(), "0", "0", "0", "",
                        Utilis.UserName, callInfo.Source, callInfo.DefaultRateMaster.ToString(), "0", callInfo.AccessPointID.ToString(),
                        callInfo.DeviceID, callInfo.OpenBarrier.ToString(), 0, remarks, 0, 1, 0, "", "", callInfo.LocalServerIP, callInfo.IsHTTPs == 1);

                    if (resp.IsSuccessful && resp.StatusCode == HttpStatusCode.OK)
                    {
                        LogFile.UpdateLogFile(string.Format("Dispute API Responce : {0}, Trans ID : {1}", resp != null ? resp.Content : "null", callInfo.TransactionID));

                        PaymentConfirmation rest = JsonConvert.DeserializeObject<PaymentConfirmation>(resp.Content);
                        if (rest != null && rest.payment.ToLower() == "success")
                        {
                            LogFile.UpdateLogFile(string.Format("Disputed Successfully. Response : {0}", resp.Content));
                            MessageBox.Show("Transaction disputed successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            LogFile.UpdateLogFile(string.Format("Server Error for dispute."));
                            MessageBox.Show("Server error for dispute", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        LogFile.UpdateLogFile(string.Format("Server is not accessible for dispute."));
                        MessageBox.Show("Server is not accessible for dispute", "Invalid Server", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                    MessageBox.Show("There is no transaction to make a dispute.", "Invalid Transaction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error btnDispute_Click : {0}", ee.Message));
            }
        }

        private void picPlateImage_Click(object sender, EventArgs e)
        {
            try
            {
                if ((sender as PictureBox).BackgroundImage != null)
                {
                    frmZoomInImage frm = new frmZoomInImage();
                    frm.Show((sender as PictureBox).BackgroundImage);
                }
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error picPlateImage_Click : {0}", ee.Message));
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (callInfo != null && callInfo.LocalServerIP != "")
                {
                    frmSearchedPlates frm = new frmSearchedPlates();
                    frm.Tag = this;
                    frm.Show(callInfo.IsHTTPs == 1, callInfo.LocalServerIP);
                }
                else
                    MessageBox.Show("Invalid location detail to search the plate.", "Invalid Location Detail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error btnSearch_Click : {0}", ee.Message));
            }
        }

        #endregion

        #region Call Waiting Forward

        private void btnAnswerWaiting_Click(object sender, EventArgs e)
        {
            try
            {
                if (callWaiting != null)
                {
                    // hang up existing call 
                    if (call != null)
                        btnHangUp_Click(sender, e);

                    call = callWaiting;
                    WireUpCallEvents();
                    inComingCall = true;

                    btnAnswer_Click(sender, e);
                    DisableCallWaiting();
                }
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error btnAnswerWaiting_Click : {0}", ee.Message));
            }
        }

        private void btnRejectWaiting_Click(object sender, EventArgs e)
        {
            try
            {
                LogFile.UpdateLogFile(string.Format("Info Waiting Call Rejected by the user"));
                callWaiting.Reject();
                DisableCallWaiting();
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error btnRejectWaiting_Click : {0}", ee.Message));
            }
        }

        private void btnForwardWaiting_Click(object sender, EventArgs e)
        {
            try
            {
                string CallFrwrdNo = "";
                InvokeGUIThread(() => { CallFrwrdNo = txtForwardNo.Text.Trim(); });

                if (CallFrwrdNo != "")
                {
                    LogFile.UpdateLogFile(string.Format("Info Waiting Call Transferred to : {0}", CallFrwrdNo));
                    callWaiting.Forward(CallFrwrdNo);
                    DisableCallWaiting();
                }
                else
                {
                    InvokeGUIThread(() => { txtForwardNo.Focus(); });
                    MessageBox.Show("Please enter valid number to transfer the call", "Invalid Extention No", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error btnForwardWaiting_Click : {0}", ee.Message));
            }
        }

        private void DisableCallWaiting()
        {
            try
            {
                InvokeGUIThread(() => { pnlCallForward.Visible = false; });
                callWaiting.CallStateChanged -= callWaiting_CallStateChanged;
                callWaiting = null;
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error btnRejectWaiting_Click : {0}", ee.Message));
            }
        }

        private void callWaiting_CallStateChanged(object sender, CallStateChangedArgs e)
        {
            try
            {
                LogFile.UpdateLogFile(string.Format("Call Waiting state changed : {0}", e.State.ToString()));
                if (e.State == CallState.Cancelled || e.State == CallState.Error)
                    DisableCallWaiting();
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error call_CallStateChanged : {0}", ee.Message));
            }
        }

        #endregion

        #region Messaging

        SIPInWardMessageDetail callInfo;

        private void btnRefreshLocation_Click(object sender, EventArgs e)
        {
            try
            {
                if (callInfo != null)
                {
                    if (callInfo.IsLocationInfo) // if already loaded than call only location detail
                        SendSIPMessage(SIPMessageType.LocationDetail);
                    else // if location detail is missing and first msg didn't deliver than send again receiver info to the device to get the location detail
                        SendSIPMessage(SIPMessageType.ReceiverSIPNo, SIPAccountConfig.UserName);
                }
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error btnRefreshLocation_Click : {0}", ee.Message));
            }
        }

        private void btnRefreshTripDetail_Click(object sender, EventArgs e)
        {
            try
            {
                SendSIPMessage(SIPMessageType.TripDetail);
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error btnRefreshTripDetail_Click : {0}", ee.Message));
            }
        }

        private void btnRefreshBNRDetail_Click(object sender, EventArgs e)
        {
            try
            {
                SendSIPMessage(SIPMessageType.BNRDetail);
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error btnRefreshBNRDetail_Click : {0}", ee.Message));
            }
        }

        private async void btnRefreshCarImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (callInfo != null && callInfo.AccessPointID != 0)
                {
                    InvokeGUIThread(() =>
                    {
                        btnRefreshCarImage.Enabled = false;
                    });
                    byte[] newBytes = null;
                    var baseString = "";
                    IRestResponse respLE = await APIs_DAL.GetLiveCameraImage(callInfo.LocalServerIP, callInfo.IsHTTPs == 1, callInfo.AccessPointID);
                    if (respLE.IsSuccessful && respLE.StatusCode == HttpStatusCode.OK)
                    {
                        baseString = respLE.Content.Replace("data:image/jpeg;base64,", string.Empty);
                        newBytes = Convert.FromBase64String(baseString);

                        MemoryStream ms = new MemoryStream(newBytes);
                        InvokeGUIThread(() =>
                        {
                            picCarFrame.BackgroundImage = Image.FromStream(ms);
                        });
                    }
                    else
                        LogFile.UpdateLogFile(string.Format("Error btnRefreshCarImage_Click : Server is not accessible"));
                    InvokeGUIThread(() =>
                    {
                        btnRefreshCarImage.Enabled = true;
                    });

                }
            }
            catch (Exception ee)
            {
                InvokeGUIThread(() =>
                {
                    btnRefreshCarImage.Enabled = true;
                });
                LogFile.UpdateLogFile(string.Format("Error btnRefreshCarImage_Click : {0}", ee.Message));
            }
        }

        private void btnScreenShot_Click(object sender, EventArgs e)
        {
            try
            {
                SendSIPMessage(SIPMessageType.ScreenShot);
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error btnScreenShot_Click : {0}", ee.Message));
            }
        }

        public void SendSIPMessage(SIPMessageType msgType, string data = "", string plateImageUrl = "")
        {
            try
            {
                if (call != null && call.DialInfo != null)
                {
                    string devNo = "";
                    cmbDevice.InvokeControl(l => devNo = l.SelectedIndex > -1 ? l.SelectedValue.ToString() : "");
                    string ReceiverNo = call.DialInfo.CallerID == SIPAccountConfig.UserName && devNo != "" ? devNo : call.DialInfo.CallerID;
                    SIPOutWardMessageDetail info = new SIPOutWardMessageDetail()
                    {
                        MsgType = msgType.ToString(),
                        Data = data,
                        PlateImageURL = plateImageUrl
                    };
                    string json = JsonConvert.SerializeObject(info);

                    phoneLine.InstantMessaging.SendMessage(new InstantMessage(ReceiverNo, json));
                    LogFile.UpdateLogFile(string.Format("SIP Message Sent to : {0}, msg: {1}", ReceiverNo, json));
                }
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error SendSIPMessage : {0}, {1}, {2}", ee.Message, msgType, data));
            }
        }

        public enum SIPMessageType
        {
            ReceiverSIPNo,
            LocationDetail,
            TripDetail,
            BNRDetail,
            ScreenShot,
            ClosePaymentWindow,
            InitTransaction
        }

        private void ReceivedSIPMessage(string jSonMsg)
        {
            try
            {
                //await Task.Run(() =>
                //{
                //    try
                //    {
                SIPInWardMessageDetail msg = JsonConvert.DeserializeObject<SIPInWardMessageDetail>(jSonMsg);
                LogFile.UpdateLogFile(string.Format("Msg Received: Content:{0}\tSender:{1}", msg.MsgType.ToLower(), SIPMessageType.ReceiverSIPNo.ToString().ToLower()));
                if (msg != null)
                {
                    if (msg.MsgType.ToLower() == SIPMessageType.LocationDetail.ToString().ToLower())
                    {
                        callInfo.LocalServerIP = msg.LocalServerIP;
                        callInfo.LocationID = msg.LocationID;
                        callInfo.LocationName = msg.LocationName;
                        callInfo.DeviceID = msg.DeviceID;
                        callInfo.DeviceName = msg.DeviceName;
                        callInfo.OpenBarrier = msg.OpenBarrier;
                        callInfo.DefaultRateMaster = msg.DefaultRateMaster;
                        callInfo.IsHTTPs = msg.IsHTTPs;
                        callInfo.AccessPointName = msg.AccessPointName;
                        callInfo.AccessPointID = msg.AccessPointID;
                        DisplayLocationInfo();

                        if (!callInfo.IsLocationInfo && callInfo.LocationID != 0 && callInfo.LocalServerIP != "") // if first time and have valid values
                        {
                            SendIncomingCallDetail();
                            callInfo.IsLocationInfo = true;
                            SendSIPMessage(SIPMessageType.TripDetail);
                            SendSIPMessage(SIPMessageType.BNRDetail);
                            btnRefreshCarImage_Click(this, new EventArgs());
                        }
                    }
                    else if (msg.MsgType.ToLower() == SIPMessageType.TripDetail.ToString().ToLower())
                    {
                        callInfo.IsTripExist = msg.IsTripExist;
                        if (callInfo.IsTripExist)
                        {
                            callInfo.TransactionID = msg.TransactionID;
                            callInfo.PlateNo = msg.PlateNo;
                            callInfo.PlateCode = msg.PlateCode;
                            callInfo.PlateCity = msg.PlateCity;
                            callInfo.EntryTime = msg.EntryTime;
                            callInfo.ExitTime = msg.ExitTime;
                            callInfo.Duration = msg.Duration;
                            callInfo.WalletBalance = msg.WalletBalance;
                            callInfo.Amount = msg.Amount;
                            callInfo.VAT = msg.VAT;
                            callInfo.BillAmount = msg.BillAmount;
                            callInfo.Source = msg.Source;
                            callInfo.PlateImageURL = msg.PlateImageURL;
                            callInfo.IsTripInfo = true;
                        }
                        DisplayTripInfo();
                    }
                    else if (msg.MsgType.ToLower() == SIPMessageType.BNRDetail.ToString().ToLower())
                    {
                        callInfo.IsBNRAvailable = msg.IsBNRAvailable;
                        callInfo.AED10Count = msg.AED10Count;
                        callInfo.AED20Count = msg.AED20Count;
                        callInfo.LastPaidCarPlate = msg.LastPaidCarPlate;
                        callInfo.PaymentTime = msg.PaymentTime;
                        callInfo.TripAmount = msg.TripAmount;
                        callInfo.Deposited = msg.Deposited;
                        callInfo.Returned = msg.Returned;
                        callInfo.IsCompleted = msg.IsCompleted;
                        callInfo.IsBNRInfo = true;
                        DisplayBNRInfo();
                    }
                    else if (msg.MsgType.ToLower() == SIPMessageType.ScreenShot.ToString().ToLower())
                    {
                        callInfo.ScreenShotBytes = msg.ScreenShotBytes;
                        DisplayScreenshot();
                    }
                }
                //    }
                //    catch (Exception ee)
                //    {
                //        LogFile.UpdateLogFile(string.Format("Error ReceivedSIPMessage Sub Task : {0}", ee.Message));
                //    }
                //});
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error ReceivedSIPMessage : {0}", ee.Message));
            }
        }

        private void DisplayScreenshot()
        {
            try
            {
                Image img = null;
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(callInfo.ScreenShotBytes)))
                {
                    img = Image.FromStream(ms);
                }
                InvokeGUIThread(() =>
                {
                    picCarFrame.BackgroundImage = img;
                });
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error DisplayScreenshot : {0}", ee.Message));
            }
        }

        private void DisplayBNRInfo()
        {
            try
            {
                InvokeGUIThread(() =>
                {
                    lbl10AEDCount.Text = string.Format("{0} Notes", callInfo.AED10Count);
                    lbl20AEDCount.Text = string.Format("{0} Notes", callInfo.AED20Count);
                    lblLastCashPaymentPlate.Text = callInfo.LastPaidCarPlate;
                    lblPaymentTime.Text = callInfo.PaymentTime;
                    lblCashPaymentAmount.Text = string.Format("{0} AED", callInfo.TripAmount);
                    lblDepositedAmount.Text = string.Format("{0} AED", callInfo.Deposited);
                    lblReturnedAmount.Text = string.Format("{0} AED", callInfo.Returned);
                });
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error DisplayBNRInfo : {0}", ee.Message));
            }
        }

        private async void DisplayTripInfo()
        {
            try
            {
                if (callInfo.IsTripExist)
                {
                    InvokeGUIThread(() =>
                    {
                        lblPlateDetail.Text = string.Format("{0} {1}", callInfo.PlateNo, callInfo.PlateCity);
                        lblEntryTime.Text = callInfo.EntryTime;
                        lblExitTime.Text = callInfo.ExitTime;
                        lblDuration.Text = string.Format("{0} Hrs", callInfo.Duration);
                        lblWalletBalance.Text = string.Format("{0} AED", callInfo.WalletBalance);
                        lblTripAmount.Text = string.Format("{0} AED", callInfo.BillAmount);
                    });

                    //show plate image url
                    if (callInfo.PlateImageURL != "")
                    {
                        await Task.Run(() =>
                        {
                            try
                            {
                                var request = WebRequest.Create(callInfo.PlateImageURL);
                                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                                request.Timeout = 1000;
                                using (var response = request.GetResponse())
                                using (var stream = response.GetResponseStream())
                                {
                                    picPlateImage.BackgroundImage = Bitmap.FromStream(stream);
                                }
                            }
                            catch (Exception ee)
                            {
                                LogFile.UpdateLogFile(string.Format("Plate Image Error: {0}", ee.Message));
                                picPlateImage.BackgroundImage = Resources.NoImage;
                            }
                        });
                    }
                    else
                        picPlateImage.BackgroundImage = Resources.NoImage;
                }
                else
                {
                    InvokeGUIThread(() =>
                    {
                        lblTripAmount.Text = lblWalletBalance.Text = lblDuration.Text = lblExitTime.Text = lblEntryTime.Text = lblPlateDetail.Text = "N/A";
                        picPlateImage.BackgroundImage = Resources.NoImage;
                    });
                }
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error DisplayTripInfo : {0}", ee.Message));
            }
        }

        private void DisplayLocationInfo()
        {
            try
            {
                InvokeGUIThread(() =>
                {
                    lblLocationName.Text = callInfo.LocationName;
                    lblDeviceName.Text = callInfo.DeviceName;
                    lblEntryExitName.Text = callInfo.AccessPointName;
                    cmbLocation.SelectedValue = callInfo.LocationID;
                    cmbBarrier.SelectedValue = callInfo.AccessPointID;
                });
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error DisplayLocationInfo : {0}", ee.Message));
            }
        }

        //private void InstantMessaging_ResponseReceived(object sender, SIPInstantMessageResult e)
        //{
        //    try
        //    {
        //        LogFile.UpdateLogFile(string.Format("Msg Response:  Delivered:{0}\tSentMessage:{1}", e.Delivered, e.SentMessage));
        //    }
        //    catch (Exception ee)
        //    {
        //        LogFile.UpdateLogFile(string.Format("Error InstantMessaging_ResponseReceived : {0}", ee.Message));
        //    }
        //}

        private void InstantMessaging_MessageReceived(object sender, InstantMessage e)
        {
            try
            {
                ReceivedSIPMessage(e.Content);
                LogFile.UpdateLogFile(string.Format("Msg Received:  Content:{0}\tSender:{1}", e.Content, e.Sender));
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error InstantMessaging_MessageReceived : {0}", ee.Message));
            }
        }

        public class SIPOutWardMessageDetail
        {
            public string Data { get; set; }
            public string MsgType { get; set; }
            public string PlateImageURL { get; set; }
        }

        public class SIPInWardMessageDetail
        {
            public string MsgType { get; set; }
            public string ScreenShotBytes { get; set; }
            public int CallIDAWS { get; set; }

            //general detail
            public string LocalServerIP { get; set; }
            public int LocationID { get; set; }
            public string LocationName { get; set; }
            public string DeviceID { get; set; }
            public string DeviceName { get; set; }
            public int OpenBarrier { get; set; }
            public int DefaultRateMaster { get; set; }
            public int IsHTTPs { get; set; }
            public string AccessPointName { get; set; }
            public int AccessPointID { get; set; }
            public bool IsLocationInfo { get; set; }

            //Trip detail
            public bool IsTripExist { get; set; }
            public string TransactionID { get; set; }
            public string PlateNo { get; set; }
            public string PlateCode { get; set; }
            public string PlateCity { get; set; }
            public string EntryTime { get; set; }
            public string ExitTime { get; set; }
            public int Duration { get; set; }
            public double WalletBalance { get; set; }
            public double Amount { get; set; }
            public double VAT { get; set; }
            public double BillAmount { get; set; }
            public string Source { get; set; }
            public string PlateImageURL { get; set; }
            public bool IsTripInfo { get; set; }

            //BNR detail
            public bool IsBNRAvailable { get; set; }
            public int AED10Count { get; set; }
            public int AED20Count { get; set; }
            public string LastPaidCarPlate { get; set; }
            public string PaymentTime { get; set; }
            public double TripAmount { get; set; }
            public int Deposited { get; set; }
            public int Returned { get; set; }
            public bool IsCompleted { get; set; }
            public bool IsBNRInfo { get; set; }
        }

        #endregion

        #region AWS Record

        private async void SendIncomingCallDetail()
        {
            try
            {
                IRestResponse resp = await AWS_APIs.GetStartCallResponce(callInfo.LocationID, callInfo.DeviceID, callInfo.DeviceName);
                if (resp.IsSuccessful && resp.StatusCode == HttpStatusCode.OK)
                {
                    CallStartResponse rest = JsonConvert.DeserializeObject<CallStartResponse>(resp.Content);
                    if (rest != null && rest.status)
                        callInfo.CallIDAWS = rest.id;
                    else
                        LogFile.UpdateLogFile(string.Format("Error AWS server Call Start msg : {0}", rest.message));
                }
                else
                    LogFile.UpdateLogFile(string.Format("Error AWS server not accessable for call start"));
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error SendIncomingCallDetail : {0}", ee.Message));
            }
        }

        private async void SendCallHangUpDetail()
        {
            try
            {
                int CallTypeID = 0;
                string Remarks = "";

                InvokeGUIThread(() =>
                {
                    Remarks = txtRemarks.Text.Trim();
                    CallTypeID = cmbCallType.SelectedIndex > -1 ? Convert.ToInt32(cmbCallType.SelectedValue) : 0;
                });

                IRestResponse resp = await AWS_APIs.GetCallHangUpResponce(callInfo.CallIDAWS, callInfo.TransactionID, CallTypeID, Remarks);
                if (resp.IsSuccessful && resp.StatusCode == HttpStatusCode.OK)
                {
                    CallStartResponse rest = JsonConvert.DeserializeObject<CallStartResponse>(resp.Content);
                    if (rest != null && rest.status)
                        return;
                    else
                        LogFile.UpdateLogFile(string.Format("Error AWS server Call end msg : {0}", rest.message));
                }
                else
                    LogFile.UpdateLogFile(string.Format("Error AWS server not accessable for call end"));
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error SendIncomingCallDetail : {0}", ee.Message));
            }
        }

        #endregion

        private void frmDashboard_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (call != null && inComingCall)
                    ArrangeIncomingCall();
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error frmDashboard_SizeChanged : {0}", ee.Message));
            }
        }

        private void btnMakeCall_Click(object sender, EventArgs e)
        {
            try
            {
                string CallNo = "";
                InvokeGUIThread(() => { CallNo = cmbDevice.SelectedIndex > -1 ? cmbDevice.SelectedValue.ToString() : ""; });
                if (CallNo != "")
                {
                    if (!inComingCall && call == null)
                    {
                        LogFile.UpdateLogFile(string.Format("Outgoing call on : {0}", CallNo));
                        StartCall(CallNo);
                    }
                    else
                    {
                        LogFile.UpdateLogFile(string.Format("Outgoing call not possible, already on call : {0}", CallNo));
                        MessageBox.Show("Hang up existing call to make new call", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    InvokeGUIThread(() => { cmbDevice.Focus(); });
                    MessageBox.Show("Please select the device", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error btnMakeCall_Click : {0}", ee.Message));
            }
        }

        private void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbLocation.SelectedIndex > -1)
                {
                    int LocID = Convert.ToInt32(cmbLocation.SelectedValue);
                    FillAccessPoints(LocID);
                }
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error cmbLocation_SelectedIndexChanged : {0}", ee.Message));
            }
        }

        private async void FillDevices(int LocID)
        {
            try
            {
                IRestResponse resp = await AWS_APIs.GetDevicesResponce(LocID);
                if (resp.IsSuccessful && resp.StatusCode == HttpStatusCode.OK)
                {
                    Devices rest = JsonConvert.DeserializeObject<Devices>(resp.Content);
                    if (rest != null && rest.status)
                    {
                        cmbDevice.DataSource = rest.data;
                        cmbDevice.DisplayMember = "device_name";
                        cmbDevice.ValueMember = "zip_number";
                        cmbDevice.SelectedIndex = -1;
                    }
                    else
                        LogFile.UpdateLogFile(string.Format("Server is not accessible"));
                }
                else
                    LogFile.UpdateLogFile(string.Format("Server is not accessible"));
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error FillDevices : {0}", ee.Message));
            }
        }

        private async void SendOpenBarrierDetails(int calltype, int accessPointID, string remarks, int LocationID)
        {
            try
            {
                IRestResponse resp = await AWS_APIs.OpenBarrier("", "", accessPointID, calltype, remarks, LocationID);
                if (resp.IsSuccessful && resp.StatusCode == HttpStatusCode.OK)
                {
                    CallStartResponse rest = JsonConvert.DeserializeObject<CallStartResponse>(resp.Content);
                    if (rest != null && rest.status)
                        return;
                    else
                        LogFile.UpdateLogFile(string.Format("Error AWS server open-gate msg : {0}", rest.message));
                }
                else
                    LogFile.UpdateLogFile(string.Format("Error AWS server not accessable for gate-open"));
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error SendOpenBarrierDetails : {0}", ee.Message));
            }
        }

        private async void btnOpenBarrier1_Click(object sender, EventArgs e)
        {
            try
            {
                int callType = 0;
                int AccessPointID = 0, LocationID = 0;
                string remarks = "";

                cmbCallType.InvokeControl(l => callType = (l.SelectedIndex >= 0 ? Convert.ToInt32(l.SelectedValue) : 0));
                cmbBarrier.InvokeControl(l => AccessPointID = (l.SelectedIndex >= 0 ? Convert.ToInt32(l.SelectedValue) : 0));
                cmbLocation.InvokeControl(l => LocationID = (l.SelectedIndex >= 0 ? Convert.ToInt32(l.SelectedValue) : 0));
                txtRemarks.InvokeControl(l => remarks = l.Text.Trim());

                if (callType == 0)
                {
                    InvokeGUIThread(() => { cmbCallType.Focus(); });
                    MessageBox.Show("Please select the type", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (AccessPointID == 0)
                {
                    InvokeGUIThread(() => { cmbBarrier.Focus(); });
                    MessageBox.Show("Please select the barrier", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (remarks == "")
                {
                    InvokeGUIThread(() => { txtRemarks.Focus(); });
                    MessageBox.Show("Please enter remarks", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                IRestResponse resp = await APIs_DAL.OpenBarrierManually(GetLocationIP(LocationID), true, AccessPointID);
                if (resp.IsSuccessful && resp.StatusCode == HttpStatusCode.OK)
                {
                    SendOpenBarrierDetails(callType, AccessPointID, remarks, LocationID);
                    LogFile.UpdateLogFile(string.Format("Barrier opened Successfully. Response : {0}", resp.Content));
                    MessageBox.Show("Barrier opened successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    LogFile.UpdateLogFile(string.Format("Server is not accessible for barrier opening."));
                    MessageBox.Show("Server is not accessible for barrier opening", "Invalid Server", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ee)
            {
                LogFile.UpdateLogFile(string.Format("Error btnOpenBarrier1_Click : {0}", ee.Message));
            }
        }
    }

    public static class ControlExtensions
    {
        public static void InvokeControl<T>(this T control, Action<T> action) where T : Control
        {
            if (control.IsHandleCreated)
            {
                if (control.InvokeRequired)
                    control.Invoke(new Action(() => action(control)));
                else
                    action(control);
            }
        }
    }
}
