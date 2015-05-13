using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodREST
{
    public class QueryResult
    {
        public List<Picture> Pictures
        {
            get;
            set;
        }

        public string Guid
        {
            get;
            set;
        }

        public int Total
        {
            get;
            set;
        }
    }
}