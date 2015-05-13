using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodREST.Properties;

namespace TodREST
{
    public static class DataAccess
    {
        private static ElasticClient _elasticClient = null;
        private const string INDEX_NAME = "tod";
        private const string PICTURE_OBJECT_TYPE = "picture";
        private const string COMPUTER_OBJECT_TYPE = "computer";
        private const string QUERY_OBJECT_TYPE = "query";

        static DataAccess()
        {
            var uri = new Uri(Settings.Default.ElasticEndPoint);

            _elasticClient = new ElasticClient(new ConnectionSettings(uri));
        }

        public static bool Delete(string id)
        {
            var result = _elasticClient.Raw.Delete(INDEX_NAME, PICTURE_OBJECT_TYPE, id);

            return result.Success;
        }

        public static bool Insert(object data)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.None);

            ElasticsearchResponse<DynamicDictionary> result;

            string type = string.Empty;

            if (data is Picture)
                type = PICTURE_OBJECT_TYPE;
            else if (data is Computer)
                type = COMPUTER_OBJECT_TYPE;
            else
                type = QUERY_OBJECT_TYPE;

            result = _elasticClient.Raw.Index(INDEX_NAME, type, json);

            return result.Success;
        }

        public static List<Computer> GetComputers()
        {
            ISearchResponse<Computer> response =
                _elasticClient.Search<Computer>(s => s
                    .Type(COMPUTER_OBJECT_TYPE)
                    .From(0)
                    .Size(100)
                    .Query(new QueryContainer(
                    new MatchQuery()
                    {
                        Field = "_type",
                        Query = "computer"
                    })));

            return response.Documents.ToList();    
        }

        public static string GetImagePath(string guid)
        {
            ISearchResponse<Picture> response =
                _elasticClient.Search<Picture>(s => s
                    .Type(PICTURE_OBJECT_TYPE)
                    .Query(new QueryContainer(
                        new MatchQuery()
                        {
                            Field = "GUID",
                            Query = guid
                        })));                    
            
            if (response.Documents.Count() != 0)
            {
                return response.Documents.First().Path;    
            }
            else
            {
                throw new KeyNotFoundException(string.Format("GUID '{0}' wasn't found", guid));
            }            
        }

        public static List<Picture> GetPicturesOfComputer(string computerId, DateTime startDate, DateTime endDate, int start = 0, int rows = 10)
        {
            QueryContainer query = new FilteredQuery()
            {
                Filter = new FilterContainer(
                    new RangeFilter()
                    {
                        Field = "Date",
                        GreaterThanOrEqualTo = startDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                        LowerThanOrEqualTo = endDate.ToString("yyyy-MM-ddTHH:mm:ss")
                    }),
                Query = new QueryContainer(
                    new MatchQuery()
                    {
                        Field = "ComputerId",
                        Query = computerId
                    })
            };

            ISearchResponse<Picture> response =
                _elasticClient.Search<Picture>(s => s
                    .Type(PICTURE_OBJECT_TYPE)
                    .From(start)
                    .Size(rows)
                    .SortAscending("Date")
                    .Query(query));

           return response.Documents.ToList();
        }

        public static Query GetQuery(string queryGuid)
        {
            ISearchResponse<Query> response =
                _elasticClient.Search<Query>(s => s
                    .Type(QUERY_OBJECT_TYPE)
                    .Query(new QueryContainer(
                        new MatchQuery()
                        {
                            Field = "Guid",
                            Query = queryGuid
                        })));

            if (response.Documents.Count() != 0)
            {
                return response.Documents.First();
            }
            else
            {
                throw new KeyNotFoundException(string.Format("GUID '{0}' wasn't found", queryGuid));
            }            
        }

        public static List<Computer> GetComputers(List<List<double>> polygon, int start = 0, int rows = 10)
        {
            QueryContainer query = new QueryContainer(new GeoShapePolygonQuery()
            {
                Field = "Location",
                Shape = new PolygonGeoShape(new List<List<List<double>>>() { polygon})
            });
    
            ISearchResponse<Computer> response =
                _elasticClient.Search<Computer>(s => s
                    .Type(COMPUTER_OBJECT_TYPE)
                    .From(start)
                    .Size(rows)
                    .Query(query));

            return response.Documents.ToList();
        }
    }
}
