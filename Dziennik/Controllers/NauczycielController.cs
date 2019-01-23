﻿using Dziennik.DAL;
using Dziennik.Helpers;
using Dziennik.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Dziennik.Controllers
{
    public class NauczycielController : Controller
    {
        private Context db = new Context();

        #region CRUD
        public ActionResult Index()
        {
            if (Session["Status"] != "Admin")
                return RedirectToAction("Index", "Home");

            return View(db.Nauczyciele.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (Session["Status"] != "Admin")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nauczyciel nauczyciel = db.Nauczyciele.Find(id);
            if (nauczyciel == null)
            {
                return HttpNotFound();
            }
            return View(nauczyciel);
        }

        public ActionResult Create()
        {
            if (Session["Status"] != "Admin")
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NauczycielID,imie,nazwisko,login,haslo")] Nauczyciel nauczyciel)
        {
            if (ModelState.IsValid)
            {
                db.Nauczyciele.Add(nauczyciel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nauczyciel);
        }

        public ActionResult Edit(int? id)
        {
            if (Session["Status"] != "Admin")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nauczyciel nauczyciel = db.Nauczyciele.Find(id);
            if (nauczyciel == null)
            {
                return HttpNotFound();
            }
            return View(nauczyciel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NauczycielID,imie,nazwisko,login,haslo")] Nauczyciel nauczyciel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nauczyciel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nauczyciel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nauczyciel nauczyciel = db.Nauczyciele.Find(id);
            if (nauczyciel == null)
            {
                return HttpNotFound();
            }
            return View(nauczyciel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var list = db.Klasy.Where(s => s.WychowawcaID == id);
            foreach (var a in list)
            {
                a.WychowawcaID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();

            var list1 = db.Lekcja.Where(s => s.NauczycielID == id);
            foreach (var a in list1)
            {
                a.NauczycielID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();

            var list2 = db.Pliki.Where(s => s.NauczycielID == id);
            foreach (var a in list2)
            {
                a.NauczycielID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();

            var list3 = db.Testy.Where(s => s.NauczycielID == id);
            foreach (var a in list3)
            {
                a.NauczycielID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();

            var list4 = db.Uwagi.Where(s => s.NauczycielID == id);
            foreach (var a in list4)
            {
                a.NauczycielID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();

            var list5 = db.Ogloszenia.Where(s => s.NauczycielID == id);
            foreach (var a in list5)
            {
                a.NauczycielID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();

            var list6 = db.Ogloszenia_dla_rodzicow.Where(s => s.NauczycielID == id);
            foreach (var a in list6)
            {
                a.NauczycielID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();

            var list7 = db.Oceny.Where(s => s.NauczycielID == id);
            foreach (var a in list7)
            {
                //a.NauczycielID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();





            Nauczyciel nauczyciel = db.Nauczyciele.Find(id);
            db.Nauczyciele.Remove(nauczyciel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region Przedmioty
        public ActionResult Przedmioty()
        {
            if (Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            var userId = Convert.ToInt32(Session["UserID"]);

            var przedmioty = db.Lekcja
                .Where(l => l.NauczycielID == userId)
                .Include(l => l.Przedmiot)
                .Include("Przedmiot.Tresc_ksztalcenia")
                .Select(l => l.Przedmiot).ToList();
            foreach (var p in przedmioty)
            {
                p.Tresc_ksztalcenia.plikSciezka = FileHandler.getFileName(p.Tresc_ksztalcenia.plikSciezka);
            }

            return View(przedmioty);
        }

        public ActionResult PlikiPrzedmiotu(int? id)
        {
            if (Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przedmiot przedmiot = db.Przedmioty.Find(id);
            if (przedmiot == null)
            {
                return HttpNotFound();
            }
            przedmiot.Tresc_ksztalcenia.plikSciezka = FileHandler.getFileName(przedmiot.Tresc_ksztalcenia.plikSciezka);
            foreach (var file in przedmiot.Pliki)
            {
                file.FilePath = FileHandler.getFileName(file.FilePath);
            }
            return View(przedmiot);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlikiPrzedmiotu([Bind(Include = "ID")] Przedmiot przedmiot, HttpPostedFileBase[] fileUpload)
        {
            if (Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            if (przedmiot.ID != null)
            {
                string sciezka = null;
                Przedmiot original = db.Przedmioty
                    .Include(p => p.Pliki)
                    .Include(p => p.Tresc_ksztalcenia)
                    .SingleOrDefault(p => p.ID == przedmiot.ID);

                if (fileUpload != null)
                {
                    foreach (var file in fileUpload)
                    {
                        sciezka = FileHandler.saveFile(file);
                        var newFile = new Plik { PrzedmiotID = przedmiot.ID, FilePath = sciezka, NauczycielID = Convert.ToInt32(Session["UserID"]), DataDodania = DateTime.Now };
                        db.Pliki.Add(newFile);
                        original.Pliki.Add(newFile);
                    }
                    db.SaveChanges();
                }

                return RedirectToAction("Przedmioty");
            }
            ViewBag.Tresc_ksztalcenia = new SelectList(db.Tresci_ksztalcenia, "PrzedmiotID", "PrzedmiotID", przedmiot.ID);
            return View(przedmiot);
        }

        public ActionResult UsunPlik(int? id)
        {
            if (Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            Plik plik = db.Pliki.Find(id);
            if (plik == null)
                return RedirectToAction("Error", "Home");
            var przedmiotId = plik.PrzedmiotID;
            db.Pliki.Remove(plik);
            db.SaveChanges();

            FileHandler.deleteFile(plik.FilePath);

            return RedirectToAction("PlikiPrzedmiotu", new { id = przedmiotId });
        }
        #endregion

        public ActionResult PlanNauczyciela(int? id)
        {
            if (Session["Status"] == "Nauczyciel")
            {
                var user = Session["UserID"];
                string ide = user.ToString();
                id = Convert.ToInt32(ide);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var lekcje = from s in db.Lekcja
                         select s;
            lekcje = lekcje.Where(s => s.NauczycielID == id);

            if (lekcje == null)
            {
                return HttpNotFound();
            }

            return View(lekcje);
        }

        public ActionResult RozkladNauczycieli(string search)
        {
            //var nauczyciele = from s in db.Nauczyciele
            //                  .Include(s=> s.Lekcje)
            //                select s;
            //if (!String.IsNullOrEmpty(search))
            //{
            //    nauczyciele = nauczyciele.Where(s => (s.imie + " " + s.nazwisko).Contains(search));
            //}
            //nauczyciele = nauczyciele.OrderByDescending(s => s.nazwisko);


            //return View(nauczyciele.ToList());

            var lekcje = from s in db.Lekcja
                         select s;
            if (!String.IsNullOrEmpty(search))
            {
                lekcje = lekcje.Where(s => (s.Nauczyciel.imie + " " + s.Nauczyciel.nazwisko).Contains(search));
            }

            if (lekcje == null)
            {
                return HttpNotFound();
            }

            return View(lekcje);
        }
        int IDPRZEDMIOTUWYBRANEGO;
        int IDUCZNIA;
        public ActionResult Klasy(int? id)
        {
            if (Session["Status"] != "Nauczyciel")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var przedmiot = db.Przedmioty.Find(id);
            TempData["idprzedmiotu"] = id ?? default(int);
            var lewel = przedmiot.level.ToString();

            var klasa = from s in db.Klasy
                        where s.level.ToString().ToLower() == lewel.ToLower()
                        select s;


            if (klasa == null)
            {
                return HttpNotFound();
            }

            return View(klasa);
        }

        [HttpPost, ActionName("Klasy")]
        [ValidateAntiForgeryToken]
        public ActionResult Klasy(int id)
        {
            if (Session["Status"] != "Nauczyciel")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TempData["idprzedmiotu"] = id;
            TempData["idprzedmiotu1"] = id;
            var przedmiot = db.Przedmioty.Find(id);
            var lewel = przedmiot.level.ToString();
            var klasa = from s in db.Klasy
                        where s.level.ToString().ToLower() == lewel.ToLower()
                        select s;


            if (klasa == null)
            {
                return HttpNotFound();
            }

            return View(klasa.ToList());

        }

        public ActionResult ListaUczniow(int? id)
        {
            if (Session["Status"] != "Nauczyciel")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            //var przedmiot = db.Przedmioty.Find((int)TempData.Peek("idprzedmiotu"));
            //TempData["idprzedmiotu"] = id ?? default(int);
            //var lewel = przedmiot.level.ToString();

            /*  var klasa = from s in db.Klasy
                          where s.level.ToString().ToLower() == lewel.ToLower()
                          select s;


              if (klasa == null)
              {
                  return HttpNotFound();
              }*/
            var uczniowie = from s in db.Uczniowie
                            select s;
            uczniowie = uczniowie.Where(s => s.KlasaID == id);

            return View(uczniowie);
        }

        [HttpPost, ActionName("ListaUczniow")]
        [ValidateAntiForgeryToken]
        public ActionResult ListaUczniow(int id)
        {
            if (Session["Status"] != "Nauczyciel")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var uczniowie = from s in db.Uczniowie
                            select s;
            uczniowie = uczniowie.Where(s => s.KlasaID == id);

            return View(uczniowie.ToList());

        }
        public ActionResult Oceny(int? id)
        {
            if (Session["Status"] != "Nauczyciel")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempData["iducznia"] = id ?? default(int);
            TempData["iducznia1"] = id ?? default(int);
            IDPRZEDMIOTUWYBRANEGO = (int)TempData.Peek("idprzedmiotu");
            var uczen = db.Uczniowie.Find(id);
            //var oceny = uczen.Oceny;
            var oceny = from s in db.Oceny
                        select s;
            oceny = oceny.Where(s => s.PrzedmiotID == IDPRZEDMIOTUWYBRANEGO);
            oceny = oceny.Where(s => s.UczenID == id);

            if (oceny == null)
            {
                return HttpNotFound();
            }

            return View(oceny);
        }

        [HttpPost, ActionName("Oceny")]
        [ValidateAntiForgeryToken]
        public ActionResult Oceny(int id)
        {
            if (Session["Status"] != "Nauczyciel")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempData["iducznia"] = id;
            TempData["cos"] = id;

            IDPRZEDMIOTUWYBRANEGO = (int)TempData.Peek("idprzedmiotu");
            var oceny = from s in db.Oceny
                        select s;
            oceny = oceny.Where(s => s.PrzedmiotID == IDPRZEDMIOTUWYBRANEGO);
            oceny = oceny.Where(s => s.UczenID == id);

            return View(oceny.ToList());

        }
        public ActionResult TworzenieOceny()
        {
            if (Session["Status"] != "Admin" && Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            return View();
        }

        // POST: Ocena/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TworzenieOceny(FormCollection collection)
        {
            if (Session["Status"] != "Admin" && Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            double ocenka = Convert.ToDouble(collection["ocena"]);

            int wage = Convert.ToInt32(collection["waga"]);
            string trusc = collection["tresc"];
            ViewBag.NauczycielID = Session["UserID"];
            Ocena ocena1 = new Ocena
            {

                ocena = ocenka,
                waga = wage,
                data = DateTime.Now,
                tresc = trusc,
                PrzedmiotID = (int)TempData.Peek("idprzedmiotu"),
                NauczycielID = Int32.Parse(ViewBag.NauczycielID),
                UczenID = (int)TempData.Peek("iducznia")
            };
            db.Oceny.Add(ocena1);
            db.SaveChanges();
            return View(ocena1);
        }
        public ActionResult EdytowanieOceny(int? id)
        {
            if (Session["Status"] != "Admin" && Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempData["idoceny"] = id ?? default(int);
            Ocena ocena = db.Oceny.Find(id);
            if (ocena == null)
            {
                return HttpNotFound();
            }
            ViewBag.NauczycielID = Session["UserID"];
            ocena.NauczycielID = Int32.Parse(ViewBag.NauczycielID);


            return View(ocena);
        }

        // POST: Oceny/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EdytowanieOceny(FormCollection collection)
        {
            if (Session["Status"] != "Admin" && Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            Ocena ocena1 = db.Oceny.Find(TempData["idoceny"]);
            double ocenka = Convert.ToDouble(collection["ocena"]);

            int wage = Convert.ToInt32(collection["waga"]);
            string trusc = collection["tresc"];

            ocena1.ocena = ocenka;
            ocena1.waga = wage;
            ocena1.tresc = trusc;
            ViewBag.NauczycielID = Session["UserID"];
            ocena1.IdEdytujacego = Int32.Parse(ViewBag.NauczycielID);
            ocena1.dataEdycji = DateTime.Now;
            db.Entry(ocena1).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Przedmioty");

        }

        // GET: Ocena/Delete/5
        public ActionResult UsuwanieOceny(int? id)
        {
            if (Session["Status"] != "Admin" && Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ocena ocena = db.Oceny.Find(id);
            if (ocena == null)
            {
                return HttpNotFound();
            }
            return View(ocena);
        }

        // POST: Ocena/Delete/5
        [HttpPost, ActionName("UsuwanieOceny")]
        [ValidateAntiForgeryToken]
        public ActionResult UsuwanieOcenyPotwierdzone(int id)
        {
            if (Session["Status"] != "Admin" && Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");
            Ocena ocena = db.Oceny.Find(id);
            db.Oceny.Remove(ocena);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #region Testy
        public ActionResult Testy(int? id)
        {
            if (Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            var userId = Convert.ToInt32(Session["UserID"]);

            var testy = db.Testy
                .Where(t => t.PrzedmiotID == id)
                .Include(t => t.Pytania)
                .Include(t => t.Przedmiot)
                .Include(t => t.Klasa)
                .Include(t => t.Nauczyciel)
                .ToList();

            return View(testy);
        }

        public ActionResult TestDodaj(int? id)
        {
            if (Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa");
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "ID", "nazwa");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TestDodaj([Bind(Include = "ID,KlasaID,PrzedmiotID,czasTrwania")] Test test)
        {
            if (Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                var userId = Convert.ToInt32(Session["UserID"]);
                test.NauczycielID = userId;
                db.Testy.Add(test);
                db.SaveChanges();
                int? przedmiotId = test.PrzedmiotID;
                return RedirectToAction("Testy", new { id = przedmiotId });
            }

            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", test.KlasaID);
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "ID", "nazwa", test.PrzedmiotID);
            return View(test);
        }


        public ActionResult TestEdytcja(int? id)
        {
            if (Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            var userId = Convert.ToInt32(Session["UserID"]);

            var test = db.Testy
                .Where(t => t.ID == id)
                .Where(t => t.NauczycielID == userId)
                .Include(t => t.Pytania)
                .Include(t => t.Przedmiot)
                .Include(t => t.Klasa)
                .SingleOrDefault();
            if (test == null)
                return RedirectToAction("Index", "Home");

            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", test.KlasaID);
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "ID", "nazwa", test.PrzedmiotID);
            return View(test);
        }

        [HttpPost]
        public ActionResult TestEdytcja([Bind(Include = "ID,PrzedmiotID,KlasaID,czasTrwania")] Test test)
        {
            if (Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                var userId = Convert.ToInt32(Session["UserID"]);
                test.NauczycielID = userId;
                db.Entry(test).State = EntityState.Modified;
                db.SaveChanges();
                int? przedmiotId = test.PrzedmiotID;
                return RedirectToAction("Testy", new { id = przedmiotId });
            }
            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", test.KlasaID);
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "ID", "nazwa", test.PrzedmiotID);
            return View(test);
        }

        public ActionResult TestUsun(int? id)
        {
            if (Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = db.Testy.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TestUsun(int id)
        {
            if (Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            Test test = db.Testy.Find(id);
            int? przedmiotId = test.PrzedmiotID;
            db.Testy.Remove(test);
            db.SaveChanges();
            return RedirectToAction("Testy", new { id = przedmiotId });
        }
        #endregion
        #region Raporty

        [NonAction]
        public static List<String> GetDates(dzien? day)
        {


            List<String> list = new List<String>();
            int year = DateTime.Now.Year - 1;

            DateTime date = new DateTime(year, 9, 1);

            while (date.Month != 7 && date.Month != 8 && date <= DateTime.Now && date.Year >= DateTime.Now.Year - 1)
            {
                if (DateTime.Now.Month >= 9 && DateTime.Now.Year == date.Year)
                    break;


                if ((int)date.DayOfWeek - 1 == (int)day)
                    list.Add(date.ToString("dd-MM-yyyy"));


                date = date.AddDays(1);

            }

            list.Reverse();



            return list;
        }


        public ActionResult LekcjeNauczyciela()
        {
            int id;
            if (Session["Status"] == "Nauczyciel")
            {
                var user = Session["UserID"];
                string ide = user.ToString();
                id = Convert.ToInt32(ide);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            var lekcje = db.Lekcja.Where(s => s.NauczycielID == id);
            return View(lekcje);

        }

        public ActionResult LekcjaDoRaportu(int? id, string data)
        {

            ViewBag.id = id;
            int id_n;
            if (Session["Status"] == "Nauczyciel")
            {
                var user = Session["UserID"];
                string ide = user.ToString();
                id_n = Convert.ToInt32(ide);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lekcja = db.Lekcja.Find(id);
            if (lekcja == null)
            {
                return HttpNotFound();
            }

            ViewBag.Daty = GetDates(lekcja.dzien);
            if (data != null)
                ViewBag.d = data;
            else {
                data = GetDates(lekcja.dzien)[0];
                ViewBag.d = data;
            }
            var uczniowie = db.Uczniowie.Where(s => s.KlasaID == lekcja.KlasaID).ToList();
            int[] cache = new int[uczniowie.Count()];
            int a = 0;
            DateTime new_Data = DateTime.ParseExact(data, "dd-MM-yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);
            foreach (var uczen in uczniowie)
            {
                var nieobecnosci = db.Nieobecnosci.Where(s => s.UczenID == uczen.ID);
                var nieobecnosci2 = nieobecnosci.Where(s => s.date == new_Data);
                var nieobecnosci3 = nieobecnosci2.Where(s => s.LekcjaID == id).ToList();
                var spoznienia = db.Spoznienia.Where(s => s.UczenID == uczen.ID);
                var spoznienia2 = spoznienia.Where(s => s.date == new_Data);
                var spoznienia3 = spoznienia2.Where(s => s.LekcjaID == id).ToList();

                if (nieobecnosci3.Count() > 0)
                    cache[a] = 1;
                else
                if (spoznienia3.Count() > 0)
                    cache[a] = 2;
                else
                    cache[a] = 0;

                a++;
            }

            ViewBag.cache = cache;



            return View(lekcja);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UtworzRaport(int? id, string data)
        {

            if (Session["Status"] == "Nauczyciel")
            {
                var user = Session["UserID"];
                string ide = user.ToString();

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            if (data == null || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var lekcja = db.Lekcja.Find(id);
            if (lekcja == null)
            {
                return HttpNotFound();
            }
            var uczniowie = db.Uczniowie.Where(s => s.KlasaID == lekcja.KlasaID).ToList();
            DateTime new_Data = DateTime.ParseExact(data, "dd-MM-yyyy",
                                      System.Globalization.CultureInfo.InvariantCulture);
            string[] radio_index = new string[uczniowie.Count()];
            int a = 0;
            foreach (var uczen in uczniowie)
            {
                var nieobecnosci = db.Nieobecnosci.Where(s => s.UczenID == uczen.ID);
                var nieobecnosci2 = nieobecnosci.Where(s => s.date == new_Data);
                var spoznienia = db.Spoznienia.Where(s => s.UczenID == uczen.ID);
                var spoznienia2 = spoznienia.Where(s => s.date == new_Data);
                var nieobecnosci3 = nieobecnosci2.Where(s => s.LekcjaID == id).ToList();
                var spoznienia3 = spoznienia2.Where(s => s.LekcjaID == id).ToList();



                radio_index[a] = Request.Form["c_" + uczen.ID];
                Spoznienie spoznienie = new Spoznienie
                {
                    UczenID = uczen.ID,
                    LekcjaID = id,
                    date = DateTime.ParseExact(data, "dd-MM-yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture)
                };
                Nieobecnosc nieobecnosc = new Nieobecnosc
                {
                    UczenID = uczen.ID,
                    LekcjaID = id,
                    date = DateTime.ParseExact(data, "dd-MM-yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture),
                    Status = 0
                };
                if (radio_index[a] == "Spoznienie" && spoznienia3.Count() == 0)
                {
                    if (nieobecnosci3.Count() > 0)
                    {
                        int x = 0;
                        foreach (var j in nieobecnosci3)
                        {
                            x = j.ID;
                        }
                        db.Nieobecnosci.Remove(db.Nieobecnosci.Find(x));
                    }
                    db.Spoznienia.Add(spoznienie);
                }

                if (radio_index[a] == "Nieobecnosc" && nieobecnosci3.Count() == 0)
                {
                    if (spoznienia3.Count() > 0)
                    {
                        int x = 0;
                        foreach (var j in spoznienia3)
                        {
                            x = j.ID;
                        }
                        db.Spoznienia.Remove(db.Spoznienia.Find(x));
                    }
                    db.Nieobecnosci.Add(nieobecnosc);
                }

                if (radio_index[a] == "Obecnosc")
                {
                    if (spoznienia3.Count() > 0)
                    {
                        int x = 0;
                        foreach (var j in spoznienia3)
                        {
                            x = j.ID;
                        }
                        db.Spoznienia.Remove(db.Spoznienia.Find(x));
                    }

                    if (nieobecnosci3.Count() > 0)
                    {
                        int x = 0;
                        foreach (var j in nieobecnosci3)
                        {
                            x = j.ID;
                        }
                        db.Nieobecnosci.Remove(db.Nieobecnosci.Find(x));
                    }

                }
                a++;
            }
            db.SaveChanges();




            return RedirectToAction("LekcjaDoRaportu", "Nauczyciel", new { id = id, data = data });
        }



        #endregion


        public ActionResult Uwagi(int? id)
        {
            if (Session["Status"] != "Nauczyciel")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var uwagi = db.Uwagi.Where(s => s.UczenID == id);
            ViewBag.imie = db.Uczniowie.Find(id).imie;
            ViewBag.nazwisko = db.Uczniowie.Find(id).nazwisko;
            ViewBag.id = id;
            return View(uwagi);

        }


        public ActionResult Nowa_uwaga(int? id)
        {
            if (Session["Status"] != "Nauczyciel")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = Convert.ToInt32(Session["UserID"]);


            ViewBag.NauczycielID = userId;
            ViewBag.UczenID = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Nowa_uwaga(int? id, [Bind(Include = "ID,naglowek,tresc,date")] Uwaga uwaga)
        {
            var userId = Convert.ToInt32(Session["UserID"]);

            uwaga.NauczycielID = userId;
            uwaga.UczenID = id;
            uwaga.date = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Uwagi.Add(uwaga);
                db.SaveChanges();
                return RedirectToAction("Index");
            }



            ViewBag.NauczycielID = userId;
            ViewBag.UczenID = id;
            return View(uwaga);
        }


        public ActionResult Edytuj_uwage(int? id)
        {
            if (Session["Status"] != "Nauczyciel")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = Convert.ToInt32(Session["UserID"]);
            Uwaga uwaga = db.Uwagi.Find(id);

            ViewBag.NauczycielID = userId;
            ViewBag.UczenID = id;
            return View(uwaga);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edytuj_uwage(int? id, [Bind(Include = "ID,naglowek,tresc,date")] Uwaga uwaga)
        {

            if (ModelState.IsValid)
            {
                var userId = Convert.ToInt32(Session["UserID"]);
                uwaga.NauczycielID = userId;
                uwaga.UczenID = id;
                uwaga.date = DateTime.Now;

                db.Entry(uwaga).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uwaga);
        }

        public ActionResult Szczegoly_uwagi(int? id)
        {
            if (Session["Status"] != "Nauczyciel")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.id = id;

            Uwaga uwaga = db.Uwagi.Find(id);


            return View(uwaga);
        }

        public ActionResult Wychowankowie()
        {
            if (Session["Status"] != "Nauczyciel")
            {
                return RedirectToAction("Index", "Home");
            }
            var userId = Convert.ToInt32(Session["UserID"]);
            var klasa = db.Klasy.Where(s => s.WychowawcaID == userId).ToList();
            if (klasa.Count() == 0)
                return RedirectToAction("Index", "Home");


            var uczniowie = from s in db.Uczniowie
                            select s;
            int? klasa_id = klasa[0].KlasaID;
            uczniowie = uczniowie.Where(s => s.KlasaID == klasa_id);
            ViewBag.klasa = klasa[0].nazwa;
            ViewBag.level = klasa[0].level;
            return View(uczniowie.ToList());

        }

        public ActionResult Oceny_wszystkie(int? id)
        {
            if (Session["Status"] != "Nauczyciel")
            {
                return RedirectToAction("Index", "Home");
            }


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userId = Convert.ToInt32(Session["UserID"]);
            var klasa = db.Klasy.Where(s => s.WychowawcaID == userId).ToList();
            if (klasa.Count() == 0 || db.Uczniowie.Find(id).KlasaID != klasa[0].KlasaID)
                return RedirectToAction("Index", "Home");

            //var oceny = uczen.Oceny;
            var oceny = from s in db.Oceny
                        select s;
            oceny = oceny.Where(s => s.UczenID == id);

            ViewBag.nazwa = db.Uczniowie.Find(id).FullName;
            ViewBag.id = db.Uczniowie.Find(id).ID;

            return View(oceny);


        }
        public ActionResult Absencja_wszystkie(int? id)
        {

            if (Session["Status"] != "Nauczyciel")
            {
                return RedirectToAction("Index", "Home");

            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userId = Convert.ToInt32(Session["UserID"]);
            var klasa = db.Klasy.Where(s => s.WychowawcaID == userId).ToList();
            if (klasa.Count() == 0 || db.Uczniowie.Find(id).KlasaID != klasa[0].KlasaID)
                return RedirectToAction("Index", "Home");
            Uczen uczen = db.Uczniowie.Find(id);

            var model = new Absencja();

            model.Nieobecnosci = db.Nieobecnosci.Where(s => s.UczenID == id);
            model.Spoznienia = db.Spoznienia.Where(s => s.UczenID == id);

            return View(model);
        }


        public async System.Threading.Tasks.Task<ActionResult> Wyslij_oceny(int? id)
        {
            if (Session["Status"] != "Nauczyciel")
            {
                return RedirectToAction("Index", "Home");

            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userId = Convert.ToInt32(Session["UserID"]);
            var klasa = db.Klasy.Where(s => s.WychowawcaID == userId).ToList();
            if (klasa.Count() == 0 || db.Uczniowie.Find(id).KlasaID != klasa[0].KlasaID)
                return RedirectToAction("Index", "Home");



            Uczen uczen = db.Uczniowie.Find(id);
            Rodzic rodzic = db.Rodzice.Find(uczen.RodzicID);
            var body = "";
            var oceny = db.Oceny.Where(s => s.UczenID == id).ToList();
            var przedmiot = "";
            foreach (Ocena o in oceny)
            {
                if (o.Przedmiot.nazwa != przedmiot)
                {
                    przedmiot = o.Przedmiot.nazwa;
                    body += "<b>" + przedmiot + "</b>" + "<br><br>";
                }

                body += "<b>" + o.ocena + "</b>" + " (" + o.waga + ") - " + o.tresc + "<br>";

            }





            var message = new MailMessage();

            message.To.Add(new MailAddress(rodzic.Email));

            message.From = new MailAddress("mojagracv@gmail.com");
            message.Subject = "Zestawienie ocen " + uczen.imie + " " + uczen.nazwisko;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "mojagracv@gmail.com",  // replace with valid value
                    Password = "civilization96"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
            return RedirectToAction("Index");

        }

        public ActionResult Pytania_rodzicow() {
            if (Session["Status"] != "Nauczyciel")
            {
                return RedirectToAction("Index", "Home");

            }
            var userId = Convert.ToInt32(Session["UserID"]);

            var zapytania = db.Zapytania.Where(s => s.NauczycielID == userId);

            return View(zapytania);




        }

        public ActionResult Odpowiedz_pytanie(int? id)
        {
            if (Session["Status"] != "Nauczyciel")
            {
                return RedirectToAction("Index", "Home");

            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Zapytanie zapytanie = db.Zapytania.Find(id);

            return View(zapytanie);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Odpowiedz_pytanie(Zapytanie zapytanie)
        {

            if (ModelState.IsValid)
            {
                var userId = Convert.ToInt32(Session["UserID"]);

                zapytanie.data_odpowiedz = DateTime.Now;

                db.Entry(zapytanie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(zapytanie);
        }
    

       



        #region Pytania
        public ActionResult Pytania(int? id)
        {
            if (Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            var pytania = db.Pytania
                .Where(p => p.TestID == id)
                .ToList();
            return View(pytania);
        }

        public ActionResult PytanieDodaj(int? id)
        {
            if (Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            ViewBag.TestID = new SelectList(db.Testy, "ID", "ID");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PytanieDodaj([Bind(Include = "ID,TestID,tresc,odpowiedz1,odpowiedz2,odpowiedz3,odpowiedz4,punktacja,odp")] Pytanie pytanie)
        {
            if (Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                db.Pytania.Add(pytanie);
                db.SaveChanges();
                return RedirectToAction("Pytania", new { id = pytanie.TestID });
            }

            ViewBag.TestID = new SelectList(db.Testy, "ID", "ID", pytanie.TestID);
            return View(pytanie);
        }

        public ActionResult PytanieEdytcja(int? id)
        {
            if (Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pytanie pytanie = db.Pytania.Find(id);
            if (pytanie == null)
            {
                return HttpNotFound();
            }
            ViewBag.TestID = new SelectList(db.Testy, "ID", "ID", pytanie.TestID);
            return View(pytanie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PytanieEdytcja([Bind(Include = "ID,TestID,tresc,odpowiedz1,odpowiedz2,odpowiedz3,odpowiedz4,punktacja,odp")] Pytanie pytanie)
        {
            if (Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                db.Entry(pytanie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Pytania", new { id = pytanie.TestID });
            }
            ViewBag.TestID = new SelectList(db.Testy, "ID", "ID", pytanie.TestID);
            return View(pytanie);
        }

        public ActionResult PytanieUsun(int? id)
        {
            if (Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pytanie pytanie = db.Pytania.Find(id);
            if (pytanie == null)
            {
                return HttpNotFound();
            }
            return View(pytanie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PytanieUsun(int id)
        {
            if (Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            Pytanie pytanie = db.Pytania.Find(id);
            var idTestu = pytanie.TestID;
            db.Pytania.Remove(pytanie);
            db.SaveChanges();
            return RedirectToAction("Pytania", new { id = idTestu });
        }
        #endregion
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
