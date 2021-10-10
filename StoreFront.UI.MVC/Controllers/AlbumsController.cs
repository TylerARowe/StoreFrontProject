using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Storefront.DATA.EF;

namespace StoreFront.UI.MVC.Controllers
{
    public class AlbumsController : Controller
    {
        private StoreFrontDB db = new StoreFrontDB();

        // GET: Albums
        public ActionResult Index()
        {
            var albums = db.Albums.Include(a => a.AlbumStatus).Include(a => a.Artist).Include(a => a.Genre).Include(a => a.RecordLabel);
            return View(albums.ToList());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: Albums/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.AlbumStatusID = new SelectList(db.AlbumStatuses, "AlbumStatusID", "AlbumStatusName");
            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "FirstName");
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName");
            ViewBag.RecordLabelID = new SelectList(db.RecordLabels, "RecordLabelID", "RecordLabelName");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "AlbumID,AlbumTitle,Description,GenreID,Price,UnitsSold,PublishDate,RecordLabelID,AlbumCover,AlbumStatusID,ArtistID")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AlbumStatusID = new SelectList(db.AlbumStatuses, "AlbumStatusID", "AlbumStatusName", album.AlbumStatusID);
            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "FirstName", album.ArtistID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", album.GenreID);
            ViewBag.RecordLabelID = new SelectList(db.RecordLabels, "RecordLabelID", "RecordLabelName", album.RecordLabelID);
            return View(album);
        }

        // GET: Albums/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlbumStatusID = new SelectList(db.AlbumStatuses, "AlbumStatusID", "AlbumStatusName", album.AlbumStatusID);
            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "FirstName", album.ArtistID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", album.GenreID);
            ViewBag.RecordLabelID = new SelectList(db.RecordLabels, "RecordLabelID", "RecordLabelName", album.RecordLabelID);
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "AlbumID,AlbumTitle,Description,GenreID,Price,UnitsSold,PublishDate,RecordLabelID,AlbumCover,AlbumStatusID,ArtistID")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AlbumStatusID = new SelectList(db.AlbumStatuses, "AlbumStatusID", "AlbumStatusName", album.AlbumStatusID);
            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "FirstName", album.ArtistID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", album.GenreID);
            ViewBag.RecordLabelID = new SelectList(db.RecordLabels, "RecordLabelID", "RecordLabelName", album.RecordLabelID);
            return View(album);
        }

        // GET: Albums/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
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
