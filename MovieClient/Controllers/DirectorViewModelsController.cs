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
    public class DirectorViewModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DirectorViewModels
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50658/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Directors");
                if (response.IsSuccessStatusCode)
                {
                    List<DirectorViewModel> directors = await response.Content.ReadAsAsync<List<DirectorViewModel>>();
                    return View(directors);
                }
            }
            return HttpNotFound();
        }

        // GET: DirectorViewModels/Details/5
        public async Task< ActionResult> Details(int? id)
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

                HttpResponseMessage response = await client.GetAsync("api/Directors/" + id);
                if (response.IsSuccessStatusCode)
                {
                    DirectorViewModel director = await response.Content.ReadAsAsync<DirectorViewModel>();
                    return View(director);
                }
            }

                return HttpNotFound();

        }

        // GET: DirectorViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DirectorViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] DirectorViewModel directorViewModel)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50658/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PostAsJsonAsync("api/Directors/", directorViewModel);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }

                // ViewBag.DirectorId = new SelectList(db.DirectorViewModels, "Id", "Name", movie.DirectorId);
            }
            return HttpNotFound();


        }

        // GET: DirectorViewModels/Edit/5
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

                HttpResponseMessage response = await client.GetAsync("api/Directors/" + id);
                if (response.IsSuccessStatusCode)
                {
                    DirectorViewModel director = await response.Content.ReadAsAsync<DirectorViewModel>();
                    return View(director);
                }
            }
            return HttpNotFound();
        }

        // POST: DirectorViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] DirectorViewModel directorViewModel)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50658/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // HTTP PUT
                    HttpResponseMessage response = await client.PutAsJsonAsync("api/Directors/" + directorViewModel.Id, directorViewModel);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return HttpNotFound();
        }

        // GET: DirectorViewModels/Delete/5
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

                HttpResponseMessage response = await client.GetAsync("api/Directors/" + id);
                if (response.IsSuccessStatusCode)
                {
                    DirectorViewModel director = await response.Content.ReadAsAsync<DirectorViewModel>();
                    return View(director);
                }
            }
            return HttpNotFound();
        }

        // POST: DirectorViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50658/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync("api/Directors/" + id);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return HttpNotFound();
        }
    }
}
