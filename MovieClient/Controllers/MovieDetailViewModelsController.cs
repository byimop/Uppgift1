using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieClient.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace MovieClient.Controllers
{
    public class MovieDetailViewModelsController : Controller
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

                }
            }
            return View();
        }

        // GET: MovieDetailViewModels
        //public ActionResult Index()
        //{
        //    return View(db.MovieDetailViewModels.ToList());
        //}

        // GET: MovieDetailViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieDetailViewModel movieDetailViewModel = db.MovieDetailViewModels.Find(id);
            if (movieDetailViewModel == null)
            {
                return HttpNotFound();
            }
            return View(movieDetailViewModel);
        }

        // GET: MovieDetailViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovieDetailViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Year,Price,DirectorName,Genre")] MovieDetailViewModel movieDetailViewModel)
        {
            if (ModelState.IsValid)
            {
                db.MovieDetailViewModels.Add(movieDetailViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movieDetailViewModel);
        }

        // GET: MovieDetailViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieDetailViewModel movieDetailViewModel = db.MovieDetailViewModels.Find(id);
            if (movieDetailViewModel == null)
            {
                return HttpNotFound();
            }
            return View(movieDetailViewModel);
        }

        // POST: MovieDetailViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Year,Price,DirectorName,Genre")] MovieDetailViewModel movieDetailViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movieDetailViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movieDetailViewModel);
        }

        // GET: MovieDetailViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieDetailViewModel movieDetailViewModel = db.MovieDetailViewModels.Find(id);
            if (movieDetailViewModel == null)
            {
                return HttpNotFound();
            }
            return View(movieDetailViewModel);
        }

        // POST: MovieDetailViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MovieDetailViewModel movieDetailViewModel = db.MovieDetailViewModels.Find(id);
            db.MovieDetailViewModels.Remove(movieDetailViewModel);
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
