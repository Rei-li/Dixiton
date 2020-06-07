using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace MyHome.Controllers
{
    public class FishFeederController : Controller
    {
        // GET: FishFeeder
        public ActionResult Index()
        {
            return View();
        }

        // GET: FishFeeder/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FishFeeder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FishFeeder/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FishFeeder/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FishFeeder/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FishFeeder/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FishFeeder/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Feed()
        {
            using (var connection = new SqlConnection("Server=tcp:batdata.database.windows.net,1433;Initial Catalog=batdata;Persist Security Info=False;User ID=nso;Password=Mortr8888;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                connection.Open();
                var sql = "UPDATE FishFeeder SET IsFeedingNeeded = @isFeedingNeeded";
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@isFeedingNeeded", 1);

                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult LightSwitch()
        {
            
             using (var connection = new SqlConnection("Server=tcp:batdata.database.windows.net,1433;Initial Catalog=batdata;Persist Security Info=False;User ID=nso;Password=Mortr8888;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                connection.Open();
                var sql = "UPDATE FishFeeder SET IsLightSwitchingNeeded = @isLightSwitchingNeeded";
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@isLightSwitchingNeeded", 1);

                    cmd.ExecuteNonQuery();
                }
            }


            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Reset()
        {

            using (var connection = new SqlConnection("Server=tcp:batdata.database.windows.net,1433;Initial Catalog=batdata;Persist Security Info=False;User ID=nso;Password=Mortr8888;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                connection.Open();
                var sql = "UPDATE FishFeeder SET IsLightSwitchingNeeded = @isLightSwitchingNeeded, IsFeedingNeeded = @isFeedingNeeded, FeedingsCount = @feedingsCount";
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@isLightSwitchingNeeded", 0);
                    cmd.Parameters.AddWithValue("@isFeedingNeeded", 0);
                    cmd.Parameters.AddWithValue("@feedingsCount", 0);

                    cmd.ExecuteNonQuery();
                }
            }


            return RedirectToAction("Index", "Home");
        }
        
    }
}
