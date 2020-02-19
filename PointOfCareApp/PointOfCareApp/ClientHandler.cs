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

                //var formattedSsid = $"\"{"NodeET"}\"";
                //var formattedPassword = $"\"{"123456789"}\"";

                //var wifiConfig = new WifiConfiguration
                //{
                //    Ssid = formattedSsid,
                //    PreSharedKey = formattedPassword
                //};
                ////wifiConfig.HiddenSSID = true;
                //wifiConfig.HiddenSSID = false;


                //var addNetwork = wifiManager.AddNetwork(wifiConfig);


                //var network = wifiManager.ConfiguredNetworks
                //                 .FirstOrDefault(n => n.Ssid == "NodeET");

                //if (network == null)
                //{
                //    Console.WriteLine($"Cannot connect to network");
                //    return;
                //}


                //wifiManager.Disconnect();
                //var enableNetwork = wifiManager.EnableNetwork(network.NetworkId, true);
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

