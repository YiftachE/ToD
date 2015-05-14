using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodREST
{
    public class Computer
    {
        public string ComputerId
        {
            get;
            set;
        }

        public GeoLocation Location
        {
            get;
            set;
        }
    }
}