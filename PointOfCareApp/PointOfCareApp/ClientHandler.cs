using Android.App;
using Android.Content;
using Android.Net;
using Android.Net.Wifi;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PointOfCareApp
{
    public class ClientHandler
    {
        public HttpClient _client { get; set; }

        WifiManager wifiManager;

        public ClientHandler()
        {                         
                _client = new HttpClient();
                wifiManager = (WifiManager)Application.Context.
                                            GetSystemService(Context.WifiService);
        }

        public async Task<bool> SendNDVStartRequest()
        {
            try
            {
                var result = await _client.PostAsync("http://69.69.69.69:80/ndv ", new StringContent("data"));


                return result.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine();
            return false;
        }

        public async Task<bool> SendNDVStopRequest()
        {
            var result = await _client.GetAsync("http://69.69.69.69:80/stopndv ");

            return result.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<bool> SendNDVCheckTempRequest()
        {
            var result = await _client.GetAsync("http://69.69.69.69:80/checkndvtemp ");

            return result.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public bool CheckIfConnectedToNode()
        {
            if (wifiManager != null)
            {
                var test = wifiManager.ConnectionInfo.SSID == "\"NodeET\"";
                return test;
            }
            else
            {
                return false;
            }
        }
    }
}

