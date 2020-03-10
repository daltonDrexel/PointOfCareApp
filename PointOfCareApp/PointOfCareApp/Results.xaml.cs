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
using OxyPlot.Axes;

namespace PointOfCareApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Results : ContentPage
    {
        public Results(List<List<DataPoint>> data)
        {
            InitializeComponent();
            plotView.Model = new OxyPlot.PlotModel();
            plotView.Model.Axes.Add(new LinearAxis() { Position = AxisPosition.Bottom,  Minimum = 0, Title = "Time [min]" });
            plotView.Model.Axes.Add(new LinearAxis() { Position = AxisPosition.Left, Minimum = 0, Title = "Fluorescence Intensity" }); 

            var s1 = new LineSeries()
            {
                Color = OxyColors.SkyBlue,
                MarkerType = MarkerType.Circle,
                MarkerSize = 6,
                MarkerStroke = OxyColors.White,
                MarkerFill = OxyColors.SkyBlue,
                MarkerStrokeThickness = 1.5
            };
            foreach (var dataP in data[0])
            {
                s1.Points.Add(dataP);
            }
            plotView.Model.Series.Add(s1);

            var s2 = new LineSeries()
            {
                Color = OxyColors.Bisque,
                MarkerType = MarkerType.Circle,
                MarkerSize = 6,
                MarkerStroke = OxyColors.White,
                MarkerFill = OxyColors.SkyBlue,
                MarkerStrokeThickness = 1.5
            };
            foreach (var dataP in data[1])
            {
                s2.Points.Add(dataP);
            }
            plotView.Model.Series.Add(s2);

            //plotView.Model.Series.Add(new OxyPlot.Series.LineSeries(data));
        }
    }
}