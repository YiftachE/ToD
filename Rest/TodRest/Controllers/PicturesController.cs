using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace TodREST.Controllers
{
    public class PicturesController : ApiController
    {
        public List<Picture> Get(string computerId, DateTime? from = null, DateTime? to = null)
        {
            if (from.HasValue && to.HasValue)
                return DataAccess.GetPicturesOfComputer(computerId, from.Value, to.Value);
            else
                return DataAccess.GetPicturesOfComputer(computerId);
        }

        public bool Get(string computerId, string path, string date, string text, string tags)
        {
            List<string> tagsList;
            
            if (!string.IsNullOrEmpty(tags))
            {
                tagsList = tags.Split(new char[] { ',' }).ToList();
            }
            else
            {
                tagsList = new List<string>();
            }

            Picture pic = new Picture()
            {
                ComputerId = computerId,
                Date = Convert.ToDateTime(date),
                Path = path,
                Tags = tagsList,
                Text = text,
                GUID = Guid.NewGuid().ToString()
            };

            return DataAccess.Insert(pic);
        }

        public HttpResponseMessage Get(string guid)
        {
            string path = DataAccess.GetImagePath(guid);

            Image img = Image.FromFile(path);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(ms.ToArray());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            return result;
        }
    }
}
