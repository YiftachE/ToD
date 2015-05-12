using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodREST
{
    public class Picture
    {
        public string GUID
        {
            get;
            set;
        }

        public string ComputerId
        {
            get;
            set;
        }
        
        public DateTime Date
        {
            get;
            set;
        }

        public string Path
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        public List<string> Tags
        {
            get;
            set;
        }
    }
}