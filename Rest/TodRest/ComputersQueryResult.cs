using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodREST
{
    public class ComputersQueryResult : BasicQueryResult
    {
        public List<Computer> Computers
        {
            get;
            set;
        }
    }
}