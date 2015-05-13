using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace TodREST.Controllers
{
    public class QueryController : ApiController
    {
        public PicturesQueryResult Get(string computerId, int start = 0, int rows = 10, DateTime? from = null, DateTime? to = null)
        {
            if (!from.HasValue)
                from = DateTime.MinValue;

            if (!to.HasValue)
                to = DateTime.MaxValue;

            List<Picture> pictures = DataAccess.GetPicturesOfComputer(computerId, from.Value, to.Value, start, rows);

            Query query = new Query()
            {
                ComputerId = computerId,
                From = from.Value,
                To = to.Value,
                Guid = Guid.NewGuid().ToString(),
                Start = start,
                Rows = rows
            };

            DataAccess.Insert(query);

            PicturesQueryResult result = new PicturesQueryResult()
            {
                Guid = query.Guid,
                Pictures = pictures,
                Total = pictures.Count
            };

            return result;
        }

        public PicturesQueryResult Get(string queryGuid)
        {
            Query query = DataAccess.GetQuery(queryGuid);

            List<Picture> pictures = 
                DataAccess.GetPicturesOfComputer(query.ComputerId, query.From, query.To, query.Start, query.Rows);

            PicturesQueryResult result = new PicturesQueryResult()
            {
                Guid = query.Guid,
                Pictures = pictures,
                Total = pictures.Count
            };

            return result;

        }

        public ComputersQueryResult Post([FromBody] List<List<double>> polygon, int start = 0, int rows = 10)
        {
            if (polygon.Count < 3)
                throw new Exception("Polygon must contain at least 3 points");

            List<Computer> computers = DataAccess.GetComputers(polygon);

            Query query = new Query()
            {
                Guid = Guid.NewGuid().ToString(),
                Start = start,
                Rows = rows,
                Polygon = polygon
            };

            DataAccess.Insert(query);

            ComputersQueryResult result = new ComputersQueryResult()
            {
                Guid = query.Guid,
                Computers = computers,
                Total = computers.Count
            };

            return result;
        }
    }
}