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

                HttpResponseMessage response = await client.GetAsync("api/Movies/");
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
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Year,Price,Genre,DirectorId")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50658/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PostAsJsonAsync("api/Movies/", movie);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }

                // ViewBag.DirectorId = new SelectList(db.DirectorViewModels, "Id", "Name", movie.DirectorId);
            }
            return HttpNotFound();
        }

        // GET: MovieViewModels/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50658/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Movies/" + id);
                if (response.IsSuccessStatusCode)
                {
                    MovieDetailViewModel movieViewModel = await response.Content.ReadAsAsync<MovieDetailViewModel>();
                    response = await client.GetAsync("api/Directors");
                    if (response.IsSuccessStatusCode)
                    {
                        List<DirectorViewModel> directorsList = await response.Content.ReadAsAsync<List<DirectorViewModel>>();
                        var dId = directorsList.Where(d => d.Name == movieViewModel.DirectorName).FirstOrDefault().Id;
                        int id2 = 0;
                        if (id.HasValue)
                        {
                            id2 = id.Value;
                        }
                        Movie movie = new Movie
                        {
                            Id = id2,
                            Title = movieViewModel.Title,
                            Year = movieViewModel.Year,
                            Price = movieViewModel.Price,
                            Genre = movieViewModel.Genre,
                            DirectorId = dId
                        };
                        return View(movie);
                    }
                }
            }
            return HttpNotFound();

        }

        // POST: MovieViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,DirectorId, Genre, Price, Year")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50658/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // HTTP PUT
                    HttpResponseMessage response = await client.PutAsJsonAsync("api/Movies/" + movie.Id, movie);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return HttpNotFound();
        }

        // GET: MovieViewModels/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50658/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Movies/" + id);
                if (response.IsSuccessStatusCode)
                {
                    MovieViewModel movies = await response.Content.ReadAsAsync<MovieViewModel>();
                    return View(movies);
                }
            }
            return HttpNotFound();
        }

        // POST: MovieViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50658/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync("api/Movies/" + id);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return HttpNotFound();
        }
    }
}
