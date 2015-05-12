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

            if (data is Picture)
                result = _elasticClient.Raw.Index(INDEX_NAME, PICTURE_OBJECT_TYPE, json);
            else 
                result = _elasticClient.Raw.Index(INDEX_NAME, COMPUTER_OBJECT_TYPE, json);

            return result.Success;
        }

        public static List<Computer> GetAllComputers()
        {
            ISearchResponse<Computer> response =
                _elasticClient.Search<Computer>(s => s
                    .Type(COMPUTER_OBJECT_TYPE)
                    .From(0)
                    .Size(100));

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

        public static List<Picture> GetPicturesOfComputer(string computerId, DateTime startDate, DateTime endDate, int start, int rows)
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
    }
}
