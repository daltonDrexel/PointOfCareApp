using System;
using System.Drawing;
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
        private Dictionary<int, Dictionary<int, int>> results = new Dictionary<int, Dictionary<int, int>>();

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

        public void GetIntensitiesFromData(SortedDictionary<int, Bitmap> data)
        {

            Dictionary<int, int> channelOneInten = new Dictionary<int, int>();

            Dictionary<int, int> channelTwoInten = new Dictionary<int, int>();

            Dictionary<int, int> channelThreeInten = new Dictionary<int, int>();

            Dictionary<int, int> channelFourInten = new Dictionary<int, int>();

            foreach (int key in data.Keys)
            {

                int intensity = 0;
                for (int ii = yTop; ii <= yBot; ii++)
                {
                    for (int i = channelOneX; i <= channelOneXEnd; i++)
                    {
                        intensity = intensity + data[key].GetPixel(i, ii).G;
                    }
                }

                channelOneInten.Add(key, intensity);
            }
            results.Add(1, channelOneInten);

            foreach (int key in data.Keys)
            {

                int intensity = 0;

                for (int ii = yTop; ii <= yBot; ii++)
                {
                    for (int i = channelTwoX; i <= channelTwoXEnd; i++)
                    {
                        intensity = intensity + data[key].GetPixel(i, ii).G;
                    }
                }

                channelTwoInten.Add(key, intensity);
            }
            results.Add(2, channelTwoInten);

            foreach (int key in data.Keys)
            {

                int intensity = 0;

                for (int ii = yTop; ii <= yBot; ii++)
                {
                    for (int i = channelThreeX; i <= channelThreeXEnd; i++)
                    {
                        intensity = intensity + data[key].GetPixel(i, ii).G;
                    }
                }

                channelThreeInten.Add(key, intensity);
            }
            results.Add(3, channelThreeInten);

            foreach (int key in data.Keys)
            {

                int intensity = 0;

                for (int ii = yTop; ii <= yBot; ii++)
                {
                    for (int i = channelFourX; i <= channelFourXEnd; i++)
                    {
                        intensity = intensity + data[key].GetPixel(i, ii).G;
                    }
                }

                channelFourInten.Add(key, intensity);
            }
            results.Add(4, channelFourInten);
            Console.WriteLine();
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
