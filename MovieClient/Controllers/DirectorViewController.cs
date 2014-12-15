﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieClient.Models;

namespace MovieClient.Controllers
{
    public class DirectorViewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DirectorView
        public ActionResult Index()
        {
            return View(db.DirectorViewModels.ToList());
        }

        // GET: DirectorView/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DirectorViewModel directorViewModel = db.DirectorViewModels.Find(id);
            if (directorViewModel == null)
            {
                return HttpNotFound();
            }
            return View(directorViewModel);
        }

        // GET: DirectorView/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DirectorView/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] DirectorViewModel directorViewModel)
        {
            if (ModelState.IsValid)
            {
                db.DirectorViewModels.Add(directorViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(directorViewModel);
        }

        // GET: DirectorView/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DirectorViewModel directorViewModel = db.DirectorViewModels.Find(id);
            if (directorViewModel == null)
            {
                return HttpNotFound();
            }
            return View(directorViewModel);
        }

        // POST: DirectorView/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] DirectorViewModel directorViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(directorViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(directorViewModel);
        }

        // GET: DirectorView/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DirectorViewModel directorViewModel = db.DirectorViewModels.Find(id);
            if (directorViewModel == null)
            {
                return HttpNotFound();
            }
            return View(directorViewModel);
        }

        // POST: DirectorView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DirectorViewModel directorViewModel = db.DirectorViewModels.Find(id);
            db.DirectorViewModels.Remove(directorViewModel);
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