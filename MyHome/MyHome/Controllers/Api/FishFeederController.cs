using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using MyHome.Models;
using Newtonsoft.Json;

namespace MyHome.Controllers.Api
{
    public class FishFeederController : ApiController
    {
        // GET: api/FishFeeder
        public FishFeederModel Get()
        {
            var data = new FishFeederModel();
            using (var connection = new SqlConnection("Server=tcp:batdata.database.windows.net,1433;Initial Catalog=batdata;Persist Security Info=False;User ID=nso;Password=Mortr8888;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                string sql = "Select TOP 1 * from FishFeeder";
                SqlCommand oCmd = new SqlCommand(sql, connection);

                connection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        int.TryParse(oReader["FeedingsCount"].ToString(), out int feedingsCount);
                        bool.TryParse(oReader["IsFeedingNeeded"].ToString(), out bool isFeedingNeeded);
                        bool.TryParse(oReader["IsLightSwitchingNeeded"].ToString(), out bool isLightSwitchingNeeded);
                        data.FeedingsCount = feedingsCount;
                        data.IsFeedingNeeded = isFeedingNeeded;
                        data.IsLightSwitchingNeeded = isLightSwitchingNeeded;
                    }

                    connection.Close();
                }
            }

            return data;
        }

        // GET: api/FishFeeder/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/FishFeeder
        public void Post([FromBody]string value)
        {
            FishFeederModel data = JsonConvert.DeserializeObject<FishFeederModel>(value);

            using (var connection = new SqlConnection("Server=tcp:batdata.database.windows.net,1433;Initial Catalog=batdata;Persist Security Info=False;User ID=nso;Password=Mortr8888;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                connection.Open();
                var sql = "UPDATE FishFeeder SET IsLightSwitchingNeeded = @isLightSwitchingNeeded, IsFeedingNeeded = @isFeedingNeeded, FeedingsCount = @feedingsCount";
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@isLightSwitchingNeeded", data.FeedingsCount);
                    cmd.Parameters.AddWithValue("@isFeedingNeeded", data.IsFeedingNeeded);
                    cmd.Parameters.AddWithValue("@feedingsCount", data.IsLightSwitchingNeeded);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // PUT: api/FishFeeder/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/FishFeeder/5
        public void Delete(int id)
        {
        }
    }
}
