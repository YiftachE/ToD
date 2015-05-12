using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace TodREST
{
    public class Computer : ApiController
    {
        public string ComputerId
        {
            get;
            set;
        }

        public List<double> Location
        {
            get;
            set;
        }
    }
}