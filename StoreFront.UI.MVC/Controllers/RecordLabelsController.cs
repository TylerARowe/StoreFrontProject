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
    public class RecordLabelsController : Controller
    {
        private StoreFrontDB db = new StoreFrontDB();

        // GET: RecordLabels
        public ActionResult Index()
        {
            return View(db.RecordLabels.ToList());
        }

        // GET: RecordLabels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecordLabel recordLabel = db.RecordLabels.Find(id);
            if (recordLabel == null)
            {
                return HttpNotFound();
            }
            return View(recordLabel);
        }

        // GET: RecordLabels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecordLabels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecordLabelID,RecordLabelName,City,State,IsActive")] RecordLabel recordLabel)
        {
            if (ModelState.IsValid)
            {
                db.RecordLabels.Add(recordLabel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recordLabel);
        }

        // GET: RecordLabels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecordLabel recordLabel = db.RecordLabels.Find(id);
            if (recordLabel == null)
            {
                return HttpNotFound();
            }
            return View(recordLabel);
        }

        // POST: RecordLabels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecordLabelID,RecordLabelName,City,State,IsActive")] RecordLabel recordLabel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recordLabel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recordLabel);
        }

        // GET: RecordLabels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecordLabel recordLabel = db.RecordLabels.Find(id);
            if (recordLabel == null)
            {
                return HttpNotFound();
            }
            return View(recordLabel);
        }

        // POST: RecordLabels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RecordLabel recordLabel = db.RecordLabels.Find(id);
            db.RecordLabels.Remove(recordLabel);
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
