using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ParkonicCallCenter.Logic
{
    public static class AWS_APIs
    {
        public static Task<IRestResponse> GetAuthenticationResponce(string UserName, string Password)
        {
            return Task.Run(() =>
            {
                try
                {
                    RestRequest req = new RestRequest("login", Method.POST, DataFormat.Json);
                    req.AddHeader("Accept", "application/json");
                    req.AddJsonBody(new { username = UserName, password = Password });
                    //   req.Timeout = 30000;
                    return SysRestClient.GetAWSRestClient().Execute(req);
                }
                catch (Exception ee)
                {
                    throw ee;
                }
            });
        }

        public static Task<IRestResponse> GetStartCallResponce(int LocationID, string DeviceID, string DeviceName)
        {
            return Task.Run(() =>
            {
                try
                {
                    RestRequest req = new RestRequest("start-call", Method.POST, DataFormat.Json);
                    req.AddHeader("Accept", "application/json");
                    req.AddJsonBody(new
                    {
                        location_id = LocationID,
                        device_id = DeviceID,
                        device_name = DeviceName,
                        start_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en")),
                        token = Utilis.AWSToken
                    });
                    return SysRestClient.GetAWSRestClient().Execute(req);
                }
                catch (Exception ee)
                {
                    throw ee;
                }
            });
        }

        public static Task<IRestResponse> GetCallHangUpResponce(int callID, string TransID, int CallType, string Remarks)
        {
            return Task.Run(() =>
            {
                try
                {
                    RestRequest req = new RestRequest("end-call", Method.POST, DataFormat.Json);
                    req.AddHeader("Accept", "application/json");
                    req.AddJsonBody(new
                    {
                        token = Utilis.AWSToken,
                        call_id = callID,
                        trip_id = TransID,
                        end_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en")),
                        call_type = CallType,
                        remarks = Remarks
                    });
                    return SysRestClient.GetAWSRestClient().Execute(req);
                }
                catch (Exception ee)
                {
                    throw ee;
                }
            });
        }

        public static Task<IRestResponse> OpenBarrier(string DeviceID, string DeviceName,
            int AccessPointID, int CallType, string Remarks, int LocationID)
        {
            return Task.Run(() =>
            {
                try
                {
                    RestRequest req = new RestRequest("open-gate", Method.POST, DataFormat.Json);
                    req.AddHeader("Accept", "application/json");
                    req.AddJsonBody(new
                    {
                        token = Utilis.AWSToken,
                        location_id = LocationID,
                        device_id = DeviceID,
                        device_name = DeviceName,
                        event_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en")),
                        access_point_id = AccessPointID,
                        type = CallType,
                        remarks = Remarks
                    });
                    return SysRestClient.GetAWSRestClient().Execute(req);
                }
                catch (Exception ee)
                {
                    throw ee;
                }
            });
        }

        public static Task<IRestResponse> GetLocationsResponce()
        {
            return Task.Run(() =>
            {
                try
                {
                    RestRequest req = new RestRequest("locations", Method.POST, DataFormat.Json);
                    req.AddHeader("Accept", "application/json");
                    req.AddJsonBody(new
                    {
                        token = Utilis.AWSToken
                    });
                    return SysRestClient.GetAWSRestClient().Execute(req);
                }
                catch (Exception ee)
                {
                    throw ee;
                }
            });
        }

        public static Task<IRestResponse> GetLocationsByGroupResponse(int GroupID)
        {
            return Task.Run(() =>
            {
                try
                {
                    RestRequest req = new RestRequest("api/info/location-group", Method.POST, DataFormat.Json);
                    req.AddHeader("Accept", "application/json");
                    req.AddJsonBody(new
                    {
                        token = awsToken,
                        group_id = GroupID
                    });

                    RestClient restC = new RestClient("https://api.parkonic.com/");
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    return restC.Execute(req);
                }
                catch (Exception ee)
                {
                    throw ee;
                }
            });
        }

        static string awsToken = "fE7AP9UgZ0eAdIISZGtDP1cML";
        public static Task<IRestResponse> GetSitesAndAccessPoints()
        {
            return Task.Run(() =>
            {
                try
                {
                    RestRequest req = new RestRequest("api/info/locations", Method.POST, DataFormat.Json);
                    req.AddHeader("Accept", "application/json");
                    req.AddJsonBody(new
                    {
                        token = awsToken
                    });

                    RestClient restC = new RestClient("https://api.parkonic.com/");
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    return restC.Execute(req);
                }
                catch (Exception ee)
                {
                    throw ee;
                }
            });
        }

        public static Task<IRestResponse> GetDevicesResponce(int locID)
        {
            return Task.Run(() =>
            {
                try
                {
                    RestRequest req = new RestRequest("devices", Method.POST, DataFormat.Json);
                    req.AddHeader("Accept", "application/json");
                    req.AddJsonBody(new
                    {
                        token = Utilis.AWSToken,
                        location_id = locID
                    });
                    return SysRestClient.GetAWSRestClient().Execute(req);
                }
                catch (Exception ee)
                {
                    throw ee;
                }
            });
        }

    }
}
