using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TODInserter
{
    class Program
    {
        static void Main(string[] args)
        {

            string computerId = args[0];
            string imageList = args[1];

            string[] files = Directory.GetFiles(imageList);

            for (int i = 13; i < files.Length; i++)
            {
                string text = PerformOCR(files[i]);
                string tags = PerformLogo(files[i]);
                string fileName = Path.GetFileNameWithoutExtension(files[i]);

                string[] parts = fileName.Split(new char[]{'_', ' '});

                int day = int.Parse(parts[0]);
                int month = int.Parse(parts[1]);
                int year = 2000 + int.Parse(parts[2]);
                int hour = int.Parse(parts[3]);
                int minutes = int.Parse(parts[4]);
                int seconds = int.Parse(parts[5]);

                DateTime date = new DateTime(year, month, day, hour, minutes, seconds);

                string requestUrl =
                    string.Format("http://localhost:53752/api/pictures?computerId={0}&text={1}&path={2}&date={3}&tags={4}",
                    computerId, text, files[i], date.ToString(), tags); 

                WebRequest request = WebRequest.Create(requestUrl);

                WebResponse response = request.GetResponse();
            }
        }

        private static string PerformLogo(string p)
        {
            return string.Empty;
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

            return new StreamReader(response.GetResponseStream()).ReadToEnd().Trim(new char[]{'\n'});
        }
    }
}
