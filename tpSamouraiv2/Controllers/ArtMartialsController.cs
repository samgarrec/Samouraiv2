﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BO;
using tpSamouraiv2.Data;

namespace tpSamouraiv2.Controllers
{
    public class ArtMartialsController : Controller
    {
        private tpSamouraiv2Context db = new tpSamouraiv2Context();

        // GET: ArtMartials
        public ActionResult Index()
        {
            return View(db.ArtMartial.ToList());
        }

        // GET: ArtMartials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtMartial artMartial = db.ArtMartial.Find(id);
            if (artMartial == null)
            {
                return HttpNotFound();
            }
            return View(artMartial);
        }

        // GET: ArtMartials/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtMartials/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nom")] ArtMartial artMartial)
        {
            if (ModelState.IsValid)
            {
                db.ArtMartial.Add(artMartial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artMartial);
        }

        // GET: ArtMartials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtMartial artMartial = db.ArtMartial.Find(id);
            if (artMartial == null)
            {
                return HttpNotFound();
            }
            return View(artMartial);
        }

        // POST: ArtMartials/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom")] ArtMartial artMartial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artMartial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artMartial);
        }

        // GET: ArtMartials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtMartial artMartial = db.ArtMartial.Find(id);
            if (artMartial == null)
            {
                return HttpNotFound();
            }
            return View(artMartial);
        }

        // POST: ArtMartials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArtMartial artMartial = db.ArtMartial.Find(id);
            db.ArtMartial.Remove(artMartial);
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
