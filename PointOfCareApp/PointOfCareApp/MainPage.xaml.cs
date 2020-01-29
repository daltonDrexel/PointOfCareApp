using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PointOfCareApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //ClientHandler clientHandler = new ClientHandler();
        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
            bool answer = await DisplayAlert("Confirm", "Would you like to pick NDV?", "Yes", "No");
            if(answer)
                await Navigation.PushAsync(new NDVDash() { Title = "NDV Test Dashboard" });
        }

    }
}
