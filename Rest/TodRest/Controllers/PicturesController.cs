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
        public HttpResponseMessage Get(string imageGuid)
        {
            string path = DataAccess.GetImagePath(imageGuid);

            Image img = Image.FromFile(path);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(ms.ToArray());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            return result;
        }

        public bool Post([FromBody] Picture postPicture)
        {
            return DataAccess.Insert(postPicture);
        }
    }
}
