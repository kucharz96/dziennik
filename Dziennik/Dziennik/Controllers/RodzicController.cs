using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dziennik.DAL;
using Dziennik.Models;
using System.Dynamic;
//using System.Data;
using System.Configuration;
using System.Data.SqlClient;
//using System.Collections.Generic;
namespace Dziennik.Controllers
{
    public class RodzicController : Controller
    {
        private Context db = new Context();

        // GET: Rodzic
        public ActionResult Index()
        {
            return View(db.Rodzice.ToList());
        }

        // GET: Rodzic/Details/5
        public ActionResult Details(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Rodzic rodzic = db.Rodzice.Find(id);

           // if (rodzic == null)
           // {
           //     return HttpNotFound();
           // }
            // Rozpoczynam tutaj tworzenie (po kliknieciu DETAILS). Kontroler bedzie
            // generował wszystkie dane z udziałem tego Rodzica.
            dynamic model = new ExpandoObject();
            model.Rodzic = GetRodzic();
            model.Uczen = GetUczniowie();
            model.Ogloszenie_dla_rodzicow = GetOgloszenia_dla_rodzica();
            //Lista o Rodzicu 
            List<Rodzic> GetRodzic()
            {
                List<Rodzic> rodzic = new List<Rodzic>();
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Context"].ConnectionString;

                //string constr = ConfigurationManager.ConnectionStrings["Data Source=(LocalDb;Initial Catalog=Dziennik;Integrated Security=SSPI;"];
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM dbo.Rodzic WHERE dbo.Rodzic.ID = @id";
                    SqlCommand cmd = new SqlCommand(query);
                    //Specyfikacja przekazanego parametru do query
                    cmd.Parameters.AddWithValue("@id", id);
                    using (cmd)
                    {
                        cmd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            Console.WriteLine("hehe");
                            while (sdr.Read())
                            {
                                rodzic.Add(new Rodzic
                                {
                                    ID = Convert.ToInt32(sdr["ID"]),
                                    imie = sdr["imie"].ToString(),
                                    nazwisko = sdr["nazwisko"].ToString(),
                                    // Country = sdr["Country"].ToString()
                                    
                                });
                            }
                        }
                        con.Close();
                        return rodzic;
                    }
                }
            }
            // Lista o Dzieciach Rodzica
            List<Uczen> GetUczniowie()
            {
                List<Uczen> uczniowie = new List<Uczen>();
                //string query = "SELECT * FROM Uczen WHERE RodzicID = id";
                string constr = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = "SELECT * FROM dbo.Uczen WHERE RodzicID = @id";
                    SqlCommand cmd = new SqlCommand(query);
                    //Specyfikacja przekazanego parametru do query
                    cmd.Parameters.AddWithValue("@id", id);
                    using (cmd)
                    {
                        cmd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                uczniowie.Add(new Uczen
                                {
                                    ID = Convert.ToInt32(sdr["ID"]),
                                    imie = sdr["imie"].ToString(),
                                    nazwisko = sdr["nazwisko"].ToString(),
                                    // Country = sdr["Country"].ToString()
                                });
                            }
                        }
                        con.Close();
                        
                        return uczniowie;
                    }
                }
            }
            //Ogłoszenia Rodzica
            List<Ogloszenie_dla_rodzicow> GetOgloszenia_dla_rodzica()
            {
                List<Ogloszenie_dla_rodzicow> ogloszenia = new List<Ogloszenie_dla_rodzicow>();
                string constr = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = "SELECT * FROM dbo.Ogloszenie_dla_rodzicow WHERE RodzicID = @id";
                    SqlCommand cmd = new SqlCommand(query);
                    //Specyfikacja przekazanego parametru do query (przekazane id musimy tak 'oprawić')
                    cmd.Parameters.AddWithValue("@id", id);
                    using (cmd)
                    {
                        cmd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read()) 
                            {
                                ogloszenia.Add(new Ogloszenie_dla_rodzicow
                                {
                                    ID = Convert.ToInt32(sdr["ID"]),
                                    naglowek = sdr["naglowek"].ToString(),
                                    tresc = sdr["tresc"].ToString(),
                                    data = (DateTime)sdr["data"],
                                    // Country = sdr["Country"].ToString()
                                });
                            }
                        }
                        con.Close();

                        return ogloszenia;
                    }
                }
            }

            //Zwraca caly model do widoku
            return View(model);
        }

        // GET: Rodzic/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rodzic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,imie,nazwisko,login,haslo")] Rodzic rodzic)
        {
            if (ModelState.IsValid)
            {
                db.Rodzice.Add(rodzic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rodzic);
        }

        // GET: Rodzic/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rodzic rodzic = db.Rodzice.Find(id);
            if (rodzic == null)
            {
                return HttpNotFound();
            }
            return View(rodzic);
        }

        // POST: Rodzic/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,imie,nazwisko,login,haslo")] Rodzic rodzic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rodzic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rodzic);
        }

        // GET: Rodzic/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rodzic rodzic = db.Rodzice.Find(id);
            if (rodzic == null)
            {
                return HttpNotFound();
            }
            return View(rodzic);
        }

        // POST: Rodzic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rodzic rodzic = db.Rodzice.Find(id);
            db.Rodzice.Remove(rodzic);
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
    }
}
