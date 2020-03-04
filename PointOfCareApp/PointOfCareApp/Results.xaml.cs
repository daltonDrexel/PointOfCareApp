using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OxyPlot.Xamarin.Forms;
using OxyPlot.Series;
using OxyPlot;

namespace PointOfCareApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Results : ContentPage
    {
        public Results(List<DataPoint> data)
        {
            InitializeComponent();
            plotView.Model = new OxyPlot.PlotModel();
            var s1 = new LineSeries()
            {
                Color = OxyColors.SkyBlue,
                MarkerType = MarkerType.Circle,
                MarkerSize = 6,
                MarkerStroke = OxyColors.White,
                MarkerFill = OxyColors.SkyBlue,
                MarkerStrokeThickness = 1.5
            };
            foreach (var dataP in data)
            {
                s1.Points.Add(dataP);
            }
            plotView.Model.Series.Add(s1);
            //plotView.Model.Series.Add(new OxyPlot.Series.LineSeries(data));
        }
    }
}