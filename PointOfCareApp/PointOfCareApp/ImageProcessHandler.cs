using Android.Graphics;
using System;
//using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using PointOfCareApp.CustomViews;


namespace PointOfCareApp
{
    class ImageProcessHandler
    {
        private string path = "C:\\Users\\ccd65\\Desktop\\MOHAMED CHIP";
        //private SortedDictionary<int, Bitmap> data = new SortedDictionary<int, Bitmap>();
        private Dictionary<int, Dictionary<DateTime, int>> results = new Dictionary<int, Dictionary<DateTime, int>>();

        private int channelOneX = 220;
        private int channelOneXEnd = 290;

        private int channelTwoX = 470;
        private int channelTwoXEnd = 540;

        private int channelThreeX = 720;
        private int channelThreeXEnd = 790;

        private int channelFourX = 970;
        private int channelFourXEnd = 1040;

        private int yTop = 290;
        private int yBot = 760;        


        public void MainRun()
        {
            //GetData();
            //GetIntensitiesFromData();
            //SaveResultsToCSV();
        }

        //private void SaveResultsToCSV()
        //{

        //    List<string> lines = new List<string>();

        //    foreach (int dataSet in results[1].Values)
        //    {
        //        lines.Add(dataSet.ToString());
        //    }
        //    File.WriteAllLines("C:\\Users\\ccd65\\Desktop\\channel1.txt", lines);
        //    lines.Clear();

        //    foreach (int dataSet in results[2].Values)
        //    {
        //        lines.Add(dataSet.ToString());
        //    }
        //    File.WriteAllLines("C:\\Users\\ccd65\\Desktop\\channel2.txt", lines);
        //    lines.Clear();

        //    foreach (int dataSet in results[3].Values)
        //    {
        //        lines.Add(dataSet.ToString());
        //    }
        //    File.WriteAllLines("C:\\Users\\ccd65\\Desktop\\channel3.txt", lines);
        //    lines.Clear();

        //    foreach (int dataSet in results[4].Values)
        //    {
        //        lines.Add(dataSet.ToString());
        //    }
        //    File.WriteAllLines("C:\\Users\\ccd65\\Desktop\\channel4.txt", lines);
        //    lines.Clear();
        //}


        public async Task<SortedDictionary<DateTime, List<int>>> GetAllGreenPixelsFromImage(SortedDictionary<DateTime, Bitmap> data) 
        {

            SortedDictionary<DateTime, List<int>> ret = new SortedDictionary<DateTime, List<int>>();
            try
            {
                foreach (DateTime key in data.Keys)
                {
                    List<int> greens = new List<int>();

                    for (int i = 0; i < data[key].Height; i++)
                    {
                        for (int ii = 0; ii < data[key].Width; ii++)
                        {
                            greens.Add(data[key].GetPixel(ii, i));
                        }
                    }
                    ret.Add(key, greens);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return ret;
        }



        public void GetIntensitiesFromData(SortedDictionary<DateTime, Bitmap> data)
        {

            Dictionary<DateTime, int> channelOneInten = new Dictionary<DateTime, int>();

            Dictionary<DateTime, int> channelTwoInten = new Dictionary<DateTime, int>();

            Dictionary<DateTime, int> channelThreeInten = new Dictionary<DateTime, int>();


            foreach (DateTime key in data.Keys)
            {
                int color = 0;
                int intensity = 0;
                for (int ii = yTop; ii <= yBot; ii++)
                {
                    for (int i = channelOneX; i <= channelOneXEnd; i++)
                    {
                        color = data[key].GetPixel(i, ii);
                        intensity = intensity + ((color >> 8) & 0xff);
                    }
                }

                channelOneInten.Add(key, intensity);
            }
            results.Add(1, channelOneInten);

            foreach (DateTime key in data.Keys)
            {
                int color = 0;
                int intensity = 0;

                for (int ii = yTop; ii <= yBot; ii++)
                {
                    for (int i = channelTwoX; i <= channelTwoXEnd; i++)
                    {
                        color = data[key].GetPixel(i, ii);
                        intensity = intensity + ((color >> 8) & 0xff);
                    }
                }

                channelTwoInten.Add(key, intensity);
            }
            results.Add(2, channelTwoInten);

            foreach (DateTime key in data.Keys)
            {
                int color = 0;
                int intensity = 0;

                for (int ii = yTop; ii <= yBot; ii++)
                {
                    for (int i = channelThreeX; i <= channelThreeXEnd; i++)
                    {
                        color = data[key].GetPixel(i, ii);
                        intensity = intensity + ((color >> 8) & 0xff);
                    }
                }

                channelThreeInten.Add(key, intensity);
            }
            results.Add(3, channelThreeInten);
        }



        //private void GetData()
        //{
        //    string[] pictures = Directory.GetFiles(path);

        //    foreach (string pict in pictures)
        //    {
        //        data.Add(Convert.ToInt32(Path.GetFileNameWithoutExtension(pict)), new Bitmap(pict));
        //    }
        //}

    }
}
