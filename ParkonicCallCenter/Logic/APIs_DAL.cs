using RestSharp;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace ParkonicCallCenter.Logic
{
    public static class APIs_DAL
    {
        public static Task<IRestResponse> GetLiveCameraImage(string serverIP, bool IsHTTPs, int AccessPointID)
        {
            return Task.Run(() =>
            {
                try
                {
                    RestRequest req = new RestRequest("getimagefromcamera", Method.POST, DataFormat.Json);
                    req.AddHeader("Accept", "application/json");
                    req.AddJsonBody(new { access_point_id = AccessPointID });
                    req.Timeout = 1000;
                    return SysRestClient.GetLocalRestClient(IsHTTPs, serverIP).Execute(req);
                }
                catch (Exception ee)
                {
                    throw ee;
                }
            });
        }

        public static Task<IRestResponse> OpenBarrierManually(string serverIP, bool IsHTTPs, int AccessPointID)
        {
            return Task.Run(() =>
            {
                try
                {
                    RestRequest req = new RestRequest("open_barrier_manually", Method.POST, DataFormat.Json);
                    req.AddHeader("Accept", "application/json");
                    req.AddJsonBody(new { access_point_id = AccessPointID });
                  //  req.Timeout = 1000;
                    return SysRestClient.GetLocalRestClient(IsHTTPs, serverIP).Execute(req);
                }
                catch (Exception ee)
                {
                    throw ee;
                }
            });
        }

        public static Task<IRestResponse> GetSearchPlateResponse(string EngPlate, string ArPlate, string serverIP, bool IsHTTPs)
        {
            return Task.Run(() =>
            {
                try
                {
                    RestRequest req = new RestRequest("pof/pof_search", Method.POST, DataFormat.Json);
                    req.AddHeader("Accept", "application/json");
                    req.AddJsonBody(new
                    {
                        keyword = EngPlate,
                        keyword_ar = ArPlate,
                        start = "",
                        end = ""
                    });
                    return SysRestClient.GetLocalRestClient(IsHTTPs, serverIP).Execute(req);
                }
                catch (Exception ee)
                {
                    throw ee;
                }
            });
        }

        public static Task<IRestResponse> CreateEntryTicket(string PlateCode, string PlateNo, string PlateCity,
           DateTime EntryTime, string serverIP, bool IsHTTPs)
        {
            return Task.Run(() =>
            {
                try
                {
                    RestRequest req = new RestRequest("station/v_entry_pof", Method.POST, DataFormat.Json);
                    req.AddHeader("Accept", "application/json");
                    req.AddJsonBody(new
                    {
                        category = PlateCode,
                        plate = PlateNo,
                        city = PlateCity,
                        checkinat = EntryTime.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en")) // 2023-10-24 11:40:00
                    });
                    return SysRestClient.GetLocalRestClient(IsHTTPs, serverIP).Execute(req);
                }
                catch (Exception ee)
                {
                    throw ee;
                }
            });
        }

        public static Task<IRestResponse> ConfirmPayment(string TransID, string _amount, string _vat,
           string _BillAmount, string _RateID, string _UserName, string _Source, string _RateMasterID,
           string _Violation, string _AccessPointID, string _deviceID, string _OpenBarrier,
           int _IsCreditCard, string _Remarks, int _IsManual, int _IsDisputed, int _isReopen, string _miscNo,
           string _paymentDetail, string serverIP, bool IsHTTPs)
        {
            return Task.Run(() =>
            {
                try
                {
                    RestRequest req = new RestRequest("paymentconfirm", Method.POST, DataFormat.Json);
                    req.AddHeader("Accept", "application/json");
                    req.AddJsonBody(new
                    {
                        transactionid = TransID,
                        amount = _amount,
                        vat = _vat,
                        bill_amount = _BillAmount,
                        rateid = _RateID,
                        username = _UserName,
                        source = _Source,
                        rate_master_id = _RateMasterID,
                        violation = _Violation,
                        access_point_id = _AccessPointID,
                        device_id = _deviceID,
                        open_barrier = _OpenBarrier,
                        is_credit_card = _IsCreditCard,
                        remarks = _Remarks,
                        is_manual = _IsManual,
                        is_disputed = _IsDisputed,
                        is_reopen = _isReopen,
                        miscno = _miscNo,
                        card_detail = _paymentDetail // payment_detail before i was using, on 26-jul-2022 changed to card_detail
                        //,debug = 1
                    });
                    return SysRestClient.GetLocalRestClient(IsHTTPs, serverIP).Execute(req);
                }
                catch (Exception ee)
                {
                    throw ee;
                }
            });
        }

    }
}
