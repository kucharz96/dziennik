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

namespace Dziennik.Controllers
{
    public class LogController : Controller
    {
        private Context db = new Context();

        // GET: Log
        public ActionResult Index()
        {
            var viewModel = new Profil
            {
                Uczniowie = db.Uczniowie.Include(u => u.Klasa).Include(u => u.Rodzic).ToList(),
                Rodzice = db.Rodzice.ToList(),
                Administratorzy = db.Administratorzy.ToList(),
                Nauczyciele = db.Nauczyciele.Include(u => u.Klasa).ToList(),
            };
            return View(viewModel);
        }

        // GET: Log/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uczen uczen = db.Uczniowie.Find(id);
            if (uczen == null)
            {
                return HttpNotFound();
            }
            return View(uczen);
        }

        // GET: Log/Create
        public ActionResult Create()
        {
            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa");
            ViewBag.RodzicID = new SelectList(db.Rodzice, "ID", "imie");
            return View();
        }

        // POST: Log/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,imie,nazwisko,login,haslo,KlasaID,RodzicID")] Uczen uczen)
        {
            if (ModelState.IsValid)
            {
                db.Uczniowie.Add(uczen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", uczen.KlasaID);
            ViewBag.RodzicID = new SelectList(db.Rodzice, "ID", "imie", uczen.RodzicID);
            return View(uczen);
        }

        // GET: Log/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uczen uczen = db.Uczniowie.Find(id);
            if (uczen == null)
            {
                return HttpNotFound();
            }
            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", uczen.KlasaID);
            ViewBag.RodzicID = new SelectList(db.Rodzice, "ID", "imie", uczen.RodzicID);
            return View(uczen);
        }

        // POST: Log/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,imie,nazwisko,login,haslo,KlasaID,RodzicID")] Uczen uczen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uczen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", uczen.KlasaID);
            ViewBag.RodzicID = new SelectList(db.Rodzice, "ID", "imie", uczen.RodzicID);
            return View(uczen);
        }

        // GET: Log/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uczen uczen = db.Uczniowie.Find(id);
            if (uczen == null)
            {
                return HttpNotFound();
            }
            return View(uczen);
        }

        // POST: Log/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Uczen uczen = db.Uczniowie.Find(id);
            db.Uczniowie.Remove(uczen);
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
