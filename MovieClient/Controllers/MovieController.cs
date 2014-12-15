using System;
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
    public class MovieController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Movie
        public ActionResult Index()
        {
            return View(db.MovieViewModels.ToList());
        }

        // GET: Movie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieDetailViewModel movieViewModel = db.MovieViewModels.Find(id);
            if (movieViewModel == null)
            {
                return HttpNotFound();
            }
            return View(movieViewModel);
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Year,Price,DirectorName,Genre")] MovieDetailViewModel movieViewModel)
        {
            if (ModelState.IsValid)
            {
                db.MovieViewModels.Add(movieViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movieViewModel);
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieDetailViewModel movieViewModel = db.MovieViewModels.Find(id);
            if (movieViewModel == null)
            {
                return HttpNotFound();
            }
            return View(movieViewModel);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Year,Price,DirectorName,Genre")] MovieDetailViewModel movieViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movieViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movieViewModel);
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieDetailViewModel movieViewModel = db.MovieViewModels.Find(id);
            if (movieViewModel == null)
            {
                return HttpNotFound();
            }
            return View(movieViewModel);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MovieDetailViewModel movieViewModel = db.MovieViewModels.Find(id);
            db.MovieViewModels.Remove(movieViewModel);
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
