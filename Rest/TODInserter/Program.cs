using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TodREST;

namespace TODInserter
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Computer comp1 = new Computer()
            {
                ComputerId = "Maya",
                Location = new GeoLocation()
                {
                    Lat = (float)34.811605,
                    Lon = (float)32.094995
                }
            };*/

            List<double> point1 = new List<double>(){30, 30};
            List<double> point2 = new List<double>(){30, 40};
            List<double> point3 = new List<double>(){40, 40};
            List<double> point4 = new List<double>(){40, 30};

            List<List<double>> polygon = new List<List<double>>();

            polygon.Add(point1);
            polygon.Add(point2);
            polygon.Add(point3);
            polygon.Add(point4);

            string apiUrl = "http://localhost:53752/api/query";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(polygon);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }

        static void Main2(string[] args)
        {

            string computerId = args[0];
            string imageList = args[1];

            string[] files = Directory.GetFiles(imageList);

            for (int i = 13; i < files.Length; i++)
            {
                string text = PerformOCR(files[i]);
                List<string> tags = PerformLogo(files[i]);
                string fileName = Path.GetFileNameWithoutExtension(files[i]);

                string[] parts = fileName.Split(new char[] { '_', ' ' });

                int day = int.Parse(parts[0]);
                int month = int.Parse(parts[1]);
                int year = 2000 + int.Parse(parts[2]);
                int hour = int.Parse(parts[3]);
                int minutes = int.Parse(parts[4]);
                int seconds = int.Parse(parts[5]);

                DateTime date = new DateTime(year, month, day, hour, minutes, seconds);

                Picture pic = new Picture()
                {
                    ComputerId = computerId,
                    Date = date,
                    Path = files[i],
                    Tags = tags,
                    Text = text,
                    GUID = Guid.NewGuid().ToString()
                };
                    
                string apiUrl = "http://localhost:53752/api/pictures";

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
                httpWebRequest.ContentType = "text/json";
                httpWebRequest.Method = "POST";

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(pic);

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
            }
        }

        private static List<string> PerformLogo(string filePath)
        {
            var proc = Process.Start("matlab.exe", string.Format("logo.m {0}", filePath));
            
            proc.WaitForExit();

            return new List<string>();
        }

        private static string PerformOCR(string imagePath)
        {
            byte[] imageBytes = File.ReadAllBytes(imagePath);

            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create("https://script.google.com/macros/s/AKfycbxNBLRhGTAXMrnVbrQHk9pcRp_C4NH36Nw4u1caXPIijm2tsofc/exec");

            request.Method = "POST";

            string postData = Convert.ToBase64String(imageBytes);

            var data = Encoding.ASCII.GetBytes(postData);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            return new StreamReader(response.GetResponseStream()).ReadToEnd().Trim(new char[] { '\n' });
        }
    }
}
