using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PointOfCareApp.CustomViews;
using Android.Graphics;
using System.Threading;

namespace PointOfCareApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NDVDash : ContentPage
    {

        ImageProcessHandler imgHand = new ImageProcessHandler();
        public static SortedDictionary<int, Bitmap> pics = new SortedDictionary<int, Bitmap>();

        private int currentTemp = 0;

        private bool firstRun = true;

        private TimeSpan elaspedTime = new TimeSpan();
        private TimeSpan targetTime = new TimeSpan(0,30,0);
        private DateTime lastPicTaken = new DateTime();

        private DateTime reactionStart = new DateTime();

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public NDVDash()
        {
            InitializeComponent();
            CameraPreview.PictureFinished += OnPictureFinished;
        }

        async void StartButtonClicked(object sender, EventArgs args)
        {
            Task.Run(StartTest);
        }


        private async void StartTest() 
        {
            //Code to use client to send httpRequest to start the test

            //Code that waits for the microcontroller to respond back indicating target temp is reached

            DateTime testStart = DateTime.Now;

            //emulate warm up
            
            while (DateTime.Now - testStart < new TimeSpan(0, 0, 5)) 
            {
                Console.WriteLine("inwhile");
                
                IsBusy = true;
            }
            IsBusy = false;
            reactionStart = DateTime.Now;

            //Test Values
            while (elaspedTime < new TimeSpan(0, 0, 30)) 
            {
                elaspedTime = DateTime.Now - reactionStart;
                //TimeRemainingTextBox.Text = $"Time Remaining - {elaspedTime.Hours}:{elaspedTime.Minutes}:{elaspedTime.Seconds}";
                if (firstRun)
                {
                    CameraPreview.CameraClick.Execute(null);
                    lastPicTaken = DateTime.Now;
                    firstRun = false;
                }
                else if(DateTime.Now - lastPicTaken > new TimeSpan(0,0,10))
                {
                    CameraPreview.CameraClick.Execute(null);
                    lastPicTaken = DateTime.Now;
                }
            
            }


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
        }


        private void OnPictureFinished()
        {
            //DisplayAlert("Confirm", "Picture Taken", "", "Ok");
        }
    }
}