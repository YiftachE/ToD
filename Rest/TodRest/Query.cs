using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodREST
{
    public class Query
    {
        public string Guid
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        public string[] Tags
        {
            get;
            set;
        }

        public List<List<double>> Polygon
        {
            get;
            set;
        }

        public string ComputerId
        {
            get;
            set;
        }

        public DateTime From
        {
            get;
            set;
        }

        public DateTime To
        {
            get;
            set;
        }

        public int Start
        {
            get;
            set;
        }

        public int Rows
        {
            get;
            set;
        }
    }
}