using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC3.UI.MVC.Utilities;
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
        public ActionResult Create()
        {
            ViewBag.AlbumStatusID = new SelectList(db.AlbumStatuses, "AlbumStatusID", "Album Status Name");
            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "Artist");
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Genre");
            ViewBag.RecordLabelID = new SelectList(db.RecordLabels, "RecordLabelID", "Record Label");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumID,AlbumTitle,Description,GenreID,Price,UnitsSold,PublishDate,RecordLabelID,AlbumCover,AlbumStatusID,ArtistID")] Album album,
            HttpPostedFileBase albumCover)
        {
            if (ModelState.IsValid)
            {

                #region File Upload w/ Utility
                string file = "NoImage.png";

                if (albumCover != null)
                {
                    file = albumCover.FileName;

                    string ext = file.Substring(file.LastIndexOf("."));

                    string[] goodExts = { ".jpg", ".jpeg", ".png", ".gif" };

                    if (goodExts.Contains(ext))
                    {
                        file = Guid.NewGuid() + ext;

                        #region Resize Image
                        //params for the Image Utility
                        //what we need: filepath, image file, maximum image size (full size), maximum thumb size (thumbnail)

                        //file path
                        string savePath = Server.MapPath("~/Content/imgstore/albums/");

                        //image file
                        Image convertedImage = Image.FromStream(albumCover.InputStream);

                        //max img size
                        int maxImageSize = 500;//value in pixels

                        //max thumb size
                        int maxThumbSize = 100;

                        //Call the ImageUtility to do work
                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                        #endregion
                    }
                    else
                    {
                        file = "NoImage.png";
                    }

                    //update book object with new filename
                    //this is what is saved to the DB
                    album.AlbumCover = file;

                }
                #endregion

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
        public ActionResult Edit([Bind(Include = "AlbumID,AlbumTitle,Description,GenreID,Price,UnitsSold,PublishDate,RecordLabelID,AlbumCover,AlbumStatusID,ArtistID")] Album album, HttpPostedFileBase albumCover)
        {
            if (ModelState.IsValid)
            {

                #region File Upload w/ Utility
                //check to see if a new file has been uploaded.  If not, the HiddenFor() from the View will maintain
                //the original value

                string file = "";

                if (albumCover != null)
                {
                    //retrieve the name of the file so we can check it's extension
                    file = albumCover.FileName;

                    //retrieve the extension

                    //file = myImage.png
                    //       012345678910
                    //file.LastIndexOf("."); -> 7
                    //file.Substring(7); -> .png


                    //string post = "Just starting this trend now because #jeffrules #allthetime"
                    //post.Substring(post.LastIndexOf("#"));

                    string ext = file.Substring(file.LastIndexOf("."));

                    string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };

                    if (goodExts.Contains(ext))
                    {
                        //create a new file name (using a GUID so it will be unique)
                        file = Guid.NewGuid() + ext;

                        #region Resize Image
                        //params for the ResizeImage() method
                        string savePath = Server.MapPath("~/Content/imgstore/albums/");

                        Image convertedImage = Image.FromStream(albumCover.InputStream);

                        int maxImageSize = 500;

                        int maxThumbSize = 100;

                        //Call the Image service method to resize our image
                        //ResizeImage() will save 2 resized copies of our original image -- 1 full size, and 1 thumbnail
                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                        #endregion

                        #region Delete the old image
                        if (album.AlbumCover != null && album.AlbumCover != "NoImage.png")
                        {
                            string path = Server.MapPath("~/Content/imgstore/albums/");
                            ImageUtility.Delete(path, album.AlbumCover);
                        }
                        #endregion

                        //Assign our new filename to the album.AlbumCover we are currently editing
                        album.AlbumCover = file;

                    }

                }
                #endregion

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

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);

            string path = Server.MapPath("~/Content/imgstore/albums/");
            ImageUtility.Delete(path, album.AlbumCover);

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
