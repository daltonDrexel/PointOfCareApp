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
            StartBtn.IsEnabled = false;
            StopBtn.IsEnabled = true;
            Task.Run(StartTest);
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