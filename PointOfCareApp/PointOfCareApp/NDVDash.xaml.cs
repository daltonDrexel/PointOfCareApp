﻿using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PointOfCareApp.CustomViews;

namespace PointOfCareApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NDVDash : ContentPage
    {

        ImageProcessHandler imgHand = new ImageProcessHandler();


        public NDVDash()
        {
            InitializeComponent();
            CameraPreview.PictureFinished += OnPictureFinished;
        }

        async void StartButtonClicked(object sender, EventArgs args)
        {
            //var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);

            //PermissionsTestText.Text = status.ToString();
            CameraPreview.CameraClick.Execute(null);
            //var photo = imgHand.TakePhoto();
            //imgHand.TakePhoto();
            Console.WriteLine();
        }
        async void StopButtonClicked(object sender, EventArgs args)
        {
            bool answer = await DisplayAlert("Abort", "Would you like to abort the test?", "Yes", "No");
        }


        private void OnPictureFinished()
        {
            DisplayAlert("Confirm", "Picture Taken", "", "Ok");
        }
    }
}