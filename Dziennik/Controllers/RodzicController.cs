﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dziennik.ActionAttrs;
using Dziennik.DAL;
using Dziennik.Models;

namespace Dziennik.Controllers
{
    public class RodzicController : Controller
    {
        private Context db = new Context();

        public ActionResult Index(string search)
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

            var rodzice = from s in db.Rodzice
                            select s;
            if (!String.IsNullOrEmpty(search))
            {
                rodzice = rodzice.Where(s => s.nazwisko.Contains(search)
                                       || s.imie.Contains(search));
            }
            rodzice = rodzice.OrderByDescending(s => s.nazwisko);
            return View(rodzice.ToList());
        }

        public ActionResult Details(int? id)
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

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

        public ActionResult Create()
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,imie,nazwisko,login,haslo")] Rodzic rodzic)
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			List<Rodzic> rodzice = db.Rodzice.Where(a => a.login == rodzic.login).ToList();
            if (rodzice.Count != 0)
            {
                ModelState.AddModelError("", "Podany login istnieje w bazie.");
            }
            if (ModelState.IsValid)
            {
                db.Rodzice.Add(rodzic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            

            return View(rodzic);
        }

        public ActionResult Edit(int? id)
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,imie,nazwisko,login,haslo")] Rodzic rodzic)
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			List<Rodzic> rodzice = db.Rodzice.Where(a => a.login == rodzic.login).ToList();
            if (rodzice.Count != 0)
            {
                ModelState.AddModelError("", "Podany login istnieje w bazie.");
            }
            if (ModelState.IsValid)
            {
                db.Entry(rodzic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(rodzic);
        }

        public ActionResult Delete(int? id)
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			Rodzic rodzic = db.Rodzice.Find(id);
            db.Rodzice.Remove(rodzic);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Ogloszenia()
        {
            if (Session["Status"] != "Rodzic")
                return RedirectToAction("Index", "Home");
            ViewBag.NauczycielID = Session["UserID"];
            int id = Int32.Parse(ViewBag.NauczycielID);
            Rodzic rodzic = db.Rodzice.Find(id);

            var dzieci = from s in db.Uczniowie
                         where s.RodzicID == id
                        select s.KlasaID;
            var ogloszenia = from s in db.Ogloszenia_dla_rodzicow
                             from b in dzieci
                             where s.KlasaID==b
                        select s;
            //var ogloszenia_dla_rodzicow = db.Ogloszenia_dla_rodzicow.Include(o => o.klasa).Include(o => o.Nauczyciel);
            //ogloszenia_dla_rodzicow = ogloszenia_dla_rodzicow.Where(o=>o.KlasaID==dzieci.);
            return View(ogloszenia.ToList());
        }

        public ActionResult Oceny(string data)
        {
            int? id = null;
            if(data != null)
                id = Int32.Parse(data);
                

            if (Session["Status"] != "Rodzic")
                return RedirectToAction("Index", "Home");
        

            int id_rodzic = Int32.Parse((string)Session["UserID"]);
            Rodzic rodzic = db.Rodzice.Find(id_rodzic);
            var dzieci = db.Uczniowie.Where(s => s.RodzicID == id_rodzic).ToList();
            ViewBag.dzieci = dzieci;

            if (id == null)
            {

                if (dzieci.Count() == 0)
                    return RedirectToAction("Index", "Home");
                else
                {
                    ViewBag.imie = dzieci[0].imie;
                    ViewBag.nazwisko = dzieci[0].nazwisko;
                    int id_dziecka = dzieci[0].ID;
                    var oceny = db.Oceny.Where(s => s.UczenID == id_dziecka).ToList();
                    return View(oceny);
                }

            }
            else
            {
                Uczen uczen = db.Uczniowie.Find(id);
                if(uczen == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                ViewBag.imie = uczen.imie;
                ViewBag.nazwisko = uczen.nazwisko;
                var oceny = db.Oceny.Where(s => s.UczenID == id).ToList();
                return View(oceny);
            }
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
