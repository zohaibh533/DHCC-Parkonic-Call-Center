using RestSharp;
using System.Configuration;
using System.Net;

namespace ParkonicCallCenter.Logic
{
    public class SysRestClient
    {
        public static RestClient GetLocalRestClient(bool IsHTTPs, string ServerIP)
        {
            if (!IsHTTPs)
            {
                return new RestClient(string.Format("http://{0}/parkonic/", ServerIP));
            }
            else
            {
                var restClient = new RestClient(string.Format("https://{0}/parkonic/", ServerIP));
                restClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                return restClient;
            }
        }

        public static RestClient GetAWSRestClient()
        {
            //
            var restClient = new RestClient(string.Format("https://{0}.parkonic.com/api/intercom/", ConfigurationManager.AppSettings["AWSServer"]));
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // restClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            return restClient;
        }
    }
}
