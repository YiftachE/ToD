using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace TodREST.Controllers
{
    public class ComputersController : ApiController
    {
        public List<Computer> Get()
        {
            return DataAccess.GetComputers();
        }

        public bool Post([FromBody] Computer postComputer)
        {
            return DataAccess.Insert(postComputer);
        }
    }
}