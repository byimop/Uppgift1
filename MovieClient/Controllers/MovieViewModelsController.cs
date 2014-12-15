using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieClient.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MovieClient.Controllers
{
    public class MovieViewModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MovieDetailViewModels
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50658/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Movies");
                if (response.IsSuccessStatusCode)
                {
                    List<MovieViewModel> movies = await response.Content.ReadAsAsync<List<MovieViewModel>>();
                    return View(movies);
                }
            }
            return HttpNotFound();
        }

        // GET: MovieViewModels/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50658/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Movies/" + id);
                if (response.IsSuccessStatusCode)
                {
                    MovieDetailViewModel movie = await response.Content.ReadAsAsync<MovieDetailViewModel>();
                    return View(movie);
                }
            }
            return HttpNotFound();
        }

        // GET: MovieViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovieViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,DirectorName")] MovieViewModel movieViewModel)
        {
            if (ModelState.IsValid)
            {
                db.MovieViewModels.Add(movieViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movieViewModel);
        }

        // GET: MovieViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieViewModel movieViewModel = db.MovieViewModels.Find(id);
            if (movieViewModel == null)
            {
                return HttpNotFound();
            }
            return View(movieViewModel);
        }

        // POST: MovieViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,DirectorName")] MovieViewModel movieViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movieViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movieViewModel);
        }

        // GET: MovieViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieViewModel movieViewModel = db.MovieViewModels.Find(id);
            if (movieViewModel == null)
            {
                return HttpNotFound();
            }
            return View(movieViewModel);
        }

        // POST: MovieViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MovieViewModel movieViewModel = db.MovieViewModels.Find(id);
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
