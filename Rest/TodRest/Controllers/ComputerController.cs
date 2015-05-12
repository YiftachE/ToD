using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodREST.Controllers
{
    public class ComputerController
    {
        public List<Computer> Get()
        {
            return DataAccess.GetAllComputers();
        }

        public bool Post(string computerId, double x, double y)
        {
            Computer computer = new Computer();
            
            computer.ComputerId = computerId;
            computer.X = x;
            computer.Y = y;

            return DataAccess.Insert(computer);
        }
    }
}