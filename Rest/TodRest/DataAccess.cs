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

        public static bool Insert(Picture data)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.None);

            var result = _elasticClient.Raw.Index(INDEX_NAME, PICTURE_OBJECT_TYPE, json);

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
                    .QueryString(string.Format("GUID:{0}", guid)));

            return response.Documents.First().Path;
        }

        public static List<Picture> GetPicturesOfComputer(string computerId)
        {
            ISearchResponse<Picture> response =
                _elasticClient.Search<Picture>(s => s
                    .Type(PICTURE_OBJECT_TYPE)
                    .From(0)
                    .SortAscending("Date")
                    .QueryString(string.Format("ComputerId:{0}", computerId)));

            return response.Documents.ToList();            
        }

        public static List<Picture> GetPicturesOfComputer(string computerId, DateTime startDate, DateTime endDate)
        {
            ISearchResponse<Picture> response =
                _elasticClient.Search<Picture>(s => s
                    .Type(PICTURE_OBJECT_TYPE)
                    .From(0)
                    .Size(100)
                    .SortAscending("Date")
                    .QueryString(string.Format("ComputerId:{0}&Date:>={1}&Date:<={2}", 
                        computerId, startDate.ToString(), endDate.ToString())));

            return response.Documents.ToList();
        }
    }
}
