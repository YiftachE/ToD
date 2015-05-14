using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodREST
{
    public class PicturesQueryResult : BasicQueryResult
    {
        public List<Picture> Pictures
        {
            get;
            set;
        }
    }
}