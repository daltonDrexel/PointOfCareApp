using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using PointOfCareApp.CustomViews;
using System.Drawing;
using System.Threading;
using System.IO;
using Microcharts;
using SkiaSharp;
using OxyPlot;

namespace PointOfCareApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NDVDash : ContentPage
    {
        ImageProcessHandler imgHand = new ImageProcessHandler();
        public static SortedDictionary<DateTime, Android.Graphics.Bitmap> pics = new SortedDictionary<DateTime, Android.Graphics.Bitmap>();

        private int currentTemp = 0;

        ClientHandler clientHandler;

        private bool firstRun = true;

        

        private TimeSpan elaspedTime = new TimeSpan();
        private TimeSpan targetTime = new TimeSpan(0,30,0);
        private DateTime lastPicTaken = new DateTime();

        private DateTime reactionStart = new DateTime();        

        public NDVDash()
        {
            InitializeComponent();
            CameraPreview.PictureFinished += OnPictureFinished;
            clientHandler = new ClientHandler();
            CheckConnection();
            //For debugging file writer 
            StartBtn.IsEnabled = true;

        }

        async void StartButtonClicked(object sender, EventArgs args)
        {
            int[] intensityTest = new int[] { 423215,
                                                421240,
                                                273375,
                                                247353,
                                                242193,
                                                245180,
                                                244120,
                                                244488,
                                                244261,
                                                245852,
                                                246177,
                                                246009,
                                                248261,
                                                248550,
                                                254064,
                                                268421,
                                                298742,
                                                338754,
                                                350944,
                                                358468,
                                                360950,
                                                363501,
                                                362925,
                                                366354,
                                                361763,
                                                359153,
                                                359069,
                                                358452,
                                                355486,
                                                359751,
                                                349146};

            int[] intesnsityTest2 = new int[] { 416165,
                                            337221,
                                            260125,
                                            223279,
                                            213163,
                                            207053,
                                            207391,
                                            205382,
                                            203425,
                                            203642,
                                            204047,
                                            203077,
                                            203304,
                                            202129,
                                            202052,
                                            214281,
                                            233050,
                                            258059,
                                            271651,
                                            273743,
                                            274307,
                                            276295,
                                            275945,
                                            275709,
                                            274326,
                                            276528,
                                            269673,
                                            268995,
                                            269216,
                                            268859,
                                            266063};

            List<DataPoint> chan1 = new List<DataPoint>();

            int i = 0;
            foreach (var data in intensityTest) 
            {
                chan1.Add(new DataPoint(i,data));
                i++;
            }

           

            await Navigation.PushAsync(new Results(chan1) {Title = "NDV Test Results"});
            //Testing Chart
            //StartBtn.IsEnabled = false;
            //StopBtn.IsEnabled = true;
            //Task.Run(StartTest);
            //await SaveResultsAsync();
        }

        private void CheckConnection()
        {
            //if (clientHandler.CheckIfConnectedToNode())
            //{
                WifiConnectedTextBox.Text = "Wifi Connected Ready to Conduct Test";
                WifiConnectedTextBox.BackgroundColor = Xamarin.Forms.Color.Green;
                StartBtn.IsEnabled = true;
                TestRequestBtn.IsEnabled = true;
            //}
            //else
            //{
                //WifiConnectedTextBox.Text = "Connect to Node Network to Conduct Test!";
            //}
        }

        private async void StartTest() 
        {

            //var gotOKStatus = await clientHandler.SendNDVStartRequest();

            //while (!clientHandler.SendNDVCheckTempRequest().Result)
            //{
                Thread.Sleep(1000);
            //}
        
            Console.WriteLine();

            reactionStart = DateTime.Now;

            //Test Values
            while (elaspedTime < new TimeSpan(0, 0, 10)) 
            {
                elaspedTime = DateTime.Now - reactionStart;
                //TimeRemainingTextBox.Text = $"Time Remaining - {elaspedTime.Hours}:{elaspedTime.Minutes}:{elaspedTime.Seconds}";
                if (firstRun)
                {
                    CameraPreview.CameraClick.Execute(null);
                    lastPicTaken = DateTime.Now;
                    firstRun = false;
                }
                else if(DateTime.Now - lastPicTaken > new TimeSpan(0,0,3))
                {
                    CameraPreview.CameraClick.Execute(null);
                    lastPicTaken = DateTime.Now;
                }
            
            }

            //imgHand.GetIntensitiesFromData(pics);


            //await SaveResultsAsync(await imgHand.GetAllGreenPixelsFromImage(pics));

            //Real Values
            //while (elaspedTime < targetTime)
            //{
            //    elaspedTime = DateTime.Now - reactionStart;
            //    //TimeRemainingTextBox.Text = $"Time Remaining - {elaspedTime.Hours}:{elaspedTime.Minutes}:{elaspedTime.Seconds}";
            //    if (firstRun)
            //    {
            //        CameraPreview.CameraClick.Execute(null);
            //        lastPicTaken = DateTime.Now;
            //        firstRun = false;
            //    }
            //    else if (DateTime.Now - lastPicTaken > new TimeSpan(0, 1, 0))
            //    {
            //        CameraPreview.CameraClick.Execute(null);
            //        lastPicTaken = DateTime.Now;
            //    }

            //}


            //imgHand.GetIntensitiesFromData(pics);
            List<DataPoint> chan1 = new List<DataPoint>();

            foreach (var intensityDateTime in imgHand.results[0].Keys) 
            {
                chan1.Add(new DataPoint(intensityDateTime.Minute, imgHand.results[0][intensityDateTime]));
            }

            Console.WriteLine();
        }

        async void StopButtonClicked(object sender, EventArgs args)
        {
            bool answer = await DisplayAlert("Abort", "Would you like to abort the test?", "Yes", "No");
            if (answer)
            {
                var gotOKStatus = await clientHandler.SendNDVStopRequest();

                pics.Clear();
                StopBtn.IsEnabled = false;
                
            }
        }

        public async Task SaveResultsAsync(SortedDictionary<DateTime, List<int>> saveMe)
        {
            //puts the text file in the movies DIR good enough for now 
            var newDir = System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryMovies);
            foreach (DateTime key in saveMe.Keys)
            {
                var fileNamePath = Path.Combine(newDir, $"{key.Second.ToString()}.txt");
                using (var writer = File.CreateText(fileNamePath))
                {
                    foreach (int i in saveMe[key])
                    {
                        await writer.WriteLineAsync(i.ToString());
                    }
                }
            }
            Console.WriteLine("Done");
        }



        private void OnPictureFinished()
        {
            //DisplayAlert("Confirm", "Picture Taken", "", "Ok");
        }

        private void RecheckConnectionBtn_Clicked(object sender, EventArgs e)
        {
            CheckConnection();
        }

        private async void TestRequestBtn_Clicked(object sender, EventArgs e)
        {
            var upToTemp = await clientHandler.SendNDVStartRequest();
            Console.WriteLine();

        }
    }
}