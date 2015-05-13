using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace TodREST.Controllers
{
    public class ComputerController
    {
        public List<Computer> Get()
        {
            return DataAccess.GetAllComputers();
        }

        public bool Post([FromBody] Computer postComputer)
        {
            return DataAccess.Insert(postComputer);
        }
    }
}