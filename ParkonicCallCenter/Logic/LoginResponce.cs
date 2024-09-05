using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkonicCallCenter.Logic
{
    public class LoginResponce
    {
        public string message { get; set; }
        public bool status { get; set; }

        public LoginDetail data { get; set; }
    }

    public class LoginDetail
    {
        public string token { get; set; }
        public string name { get; set; }
        public List<CallTypeMaster> call_types { get; set; }
    }

    public class CallTypeMaster
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class CallStartResponse
    {
        public string message { get; set; }
        public int id { get; set; }
        public bool status { get; set; }
    }

    public class PaymentConfirmation
    {
        public string payment { get; set; }
    }


    public class PlateDetails
    {
        public string transactionid { get; set; }
        public string plate { get; set; }
        public string platecode { get; set; }
        public string city { get; set; }
        public string checkinat { get; set; }
        public string checkoutat { get; set; }
        public object paid_at { get; set; }
        public string usertype { get; set; }
        public string bill_amount { get; set; }
        public string url { get; set; }
        public string name { get; set; }
    }

    public class SearchDetails
    {
        public List<PlateDetails> data { get; set; }
    }


    public class Location
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class GrpLocation
    {
        public int id { get; set; }
        public string name { get; set; }
        public string ip { get; set; }
    }

    public class Locations
    {
        public List<Location> data { get; set; }
        public bool status { get; set; }
    }

    public class LocationGroups
    {
        public List<GrpLocation> data { get; set; }
        public bool status { get; set; }
    }

    public class Device
    {
        public string location { get; set; }
        public string device_name { get; set; }
        public string zip_number { get; set; }
    }

    public class Devices
    {
        public List<Device> data { get; set; }
        public bool status { get; set; }
    }


    public class LocationAWS
    {
        public int id { get; set; }
        public string name { get; set; }
        public int active { get; set; }
        public List<AccessPoint> gates { get; set; }
    }

    public class AccessPoint
    {
        public int id { get; set; }
        public string name { get; set; }
        public int is_exit { get; set; }
    }

    public class SitesAndAccessPointsResponse
    {
        public List<LocationAWS> data { get; set; }
        public bool status { get; set; }
    }

}
