using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sample1.Models;

namespace Sample1.Controllers
{
    public class UserDetailsController : Controller
    {
        private DevBoxEntities1 db = new DevBoxEntities1();

        // GET: UserDetails
        public ActionResult Index()
        {
            return View(db.User_Details.ToList());
        }

       /* if(User_Details.ResumePdf!=null){
            String folder = "Details/";
        User_Details.ResumeUrl=await UploadFile(folder , User_Details.ResumePdf);
        }*/


        // GET: UserDetails/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Details user_Details = db.User_Details.Find(id);
            if (user_Details == null)
            {
                return HttpNotFound();
            }
            return View(user_Details);
        }

        // GET: UserDetails/Create
        public ActionResult Create()
        {

            YourAction();
            CitiesDropDown();
            return View();
        }

        // POST: UserDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Email,State,City,MobileNumber,Resume,Photo")] User_Details user_Details)
        {
            if (ModelState.IsValid)
            {
                db.User_Details.Add(user_Details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user_Details);
        }

        // GET: UserDetails/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Details user_Details = db.User_Details.Find(id);
            if (user_Details == null)
            {
                return HttpNotFound();
            }
            return View(user_Details);
        }

        // POST: UserDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Email,State,City,MobileNumber,ResumeUrl,PhotoUrl")] User_Details user_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_Details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user_Details);
        }

        // GET: UserDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Details user_Details = db.User_Details.Find(id);
            if (user_Details == null)
            {
                return HttpNotFound();
            }
            return View(user_Details);
        }

        // POST: UserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User_Details user_Details = db.User_Details.Find(id);
            db.User_Details.Remove(user_Details);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public List<State> GetAllStates()
        {
            List<State> states = new List<State>();
            using (var connection = new SqlConnection(ConfigurationManager.
                ConnectionStrings["DevBox"].ConnectionString))
            {
                connection.Open();
                string query = "SELECT * FROM States";
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        states.Add(new State
                        {
                            Id = (int)reader["Id"],
                            StateName = (string)reader["StateName"]
                        });
                    }
                }
            }
            return states;
        }
        public ActionResult YourAction()
        {
            List<State> states = GetAllStates(); // Replace with your method to get the list of states
            ViewBag.States = new SelectList(states, "Id", "StateName"); // Assuming 'Id' and 'StateName' are properties of your State object
            return View("Temp");
        }
        public List<City>GetAllCitiesForState(State obj)
        {
         
            string query = "SELECT * FROM cities WHERE StateName = @StateName"; // or "SELECT * FROM City WHERE StateId = @StateId";
            List<City> cities = new List<City>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevBox"].ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    // Add parameters to the query based on your chosen identifier (StateName or StateId)
                    // For StateName:
                    command.Parameters.Add(new SqlParameter("@StateName", SqlDbType.NVarChar) { Value = obj.StateName });
                    // For StateId:
                    // command.Parameters.Add(new SqlParameter("@StateId", SqlDbType.Int) { Value = obj.StateId });

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cities.Add(new City
                            {
                                ID = (int)reader["Id"],
                                CityName = (string)reader["CityName"]
                            });
                        }
                    }
                }
            }
            return cities;

        }
        State temporaryState = new State
            {
                Id = 2, 
                StateName = "Telangana"
         };


            public ActionResult CitiesDropDown()
        {
            List<City> cities = GetAllCitiesForState(temporaryState); // Replace with your method to get the list of states
            ViewBag.Cities = new SelectList(cities, "Id", "CityName"); // Assuming 'Id' and 'StateName' are properties of your State object
            return View("TempCities");
        }




    }
}
