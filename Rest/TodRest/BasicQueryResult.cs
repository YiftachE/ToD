using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodREST
{
    public abstract class BasicQueryResult
    {
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