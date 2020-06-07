using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Data.SqlClient;
using MyHome.Models;

namespace MyHome.Controllers.Api
{
    public class SonarController : ApiController
    {
        // GET: api/Sonar
        public IEnumerable<SonarData> Get()
        {
            var dataList = new List<SonarData>();
            using (var connection = new SqlConnection("Server=tcp:batdata.database.windows.net,1433;Initial Catalog=batdata;Persist Security Info=False;User ID=nso;Password=Mortr8888;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                string sql = "Select * from SonarData";
                SqlCommand oCmd = new SqlCommand(sql, connection);

                connection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        var data = new SonarData();
                        int distance;
                        int angle;

                        int.TryParse(oReader["Distance"].ToString(), out distance);
                        int.TryParse(oReader["Angle"].ToString(), out angle);
                        data.Distance = distance;
                        data.Angle = angle;
                        dataList.Add(data);
                    }

                    connection.Close();
                }
            }

            return dataList;
        }

        // GET: api/Sonar/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Sonar
        public void Post([FromBody]string value)
        {
            SonarData data = JsonConvert.DeserializeObject<SonarData>(value);

            using (var connection = new SqlConnection("Server=tcp:batdata.database.windows.net,1433;Initial Catalog=batdata;Persist Security Info=False;User ID=nso;Password=Mortr8888;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                connection.Open();
                var sql = "INSERT INTO SonarData(Distance, Angle) VALUES(@distance, @angle)";
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@distance", data.Distance);
                    cmd.Parameters.AddWithValue("@angle", data.Angle);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // PUT: api/Sonar/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Sonar/5
        public void Delete(int id)
        {
        }
    }
}
