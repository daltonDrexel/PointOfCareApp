﻿using Android.Content;
using Android.Graphics;
using PointOfCareApp.Camera2;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using CameraPreview = PointOfCareApp.CustomViews.CameraPreview;

[assembly: ExportRenderer(typeof(CameraPreview), typeof(CameraViewServiceRenderer))]
namespace PointOfCareApp.Camera2
{
    public class CameraViewServiceRenderer : ViewRenderer<CameraPreview, CameraDroid>
    {
        private CameraDroid _camera;
        private CameraPreview _currentElement;
        private readonly Context _context;

        public CameraViewServiceRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CameraPreview> e)
        {
            base.OnElementChanged(e);

            _camera = new CameraDroid(Context);

            SetNativeControl(_camera);

            if (e.NewElement != null && _camera != null)
            {
                e.NewElement.CameraClick = new Command(() => TakePicture());
                _currentElement = e.NewElement;
                _camera.SetCameraOption(_currentElement.Camera);
                _camera.Photo += OnPhoto;
            }
        }

        public void TakePicture()
        {
            _camera.LockFocus();
        }

        private void OnPhoto(object sender, byte[] imgSource)
        {
            try
            {
                BitmapFactory.Options options = new BitmapFactory.Options();
                options.InMutable = true;
                Bitmap bmp = BitmapFactory.DecodeByteArray(imgSource, 0, imgSource.Length, options);
                CoronaDash.pics.Add(DateTime.Now, bmp);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }
            Device.BeginInvokeOnMainThread(() =>
            {
                _currentElement?.PictureTaken();
            });
        }

        protected override void Dispose(bool disposing)
        {
            _camera.Photo -= OnPhoto;

            base.Dispose(disposing);
        }
    }
}
