using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace TodREST.Controllers
{
    public class QueryController : ApiController
    {
        public QueryResult Get(string computerId, int start = 0, int rows = 10, DateTime? from = null, DateTime? to = null)
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

            QueryResult result = new QueryResult()
            {
                Guid = query.Guid,
                Pictures = pictures,
                Total = pictures.Count
            };

            return result;
        }

        public QueryResult Get(string queryGuid)
        {
            Query query = DataAccess.GetQuery(queryGuid);

            return Get(query.ComputerId, query.Start, query.Rows, query.From, query.To);
        }
    }
}