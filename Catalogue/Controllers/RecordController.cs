using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Catalogue.Models;

namespace Catalogue.Controllers
{
    [Authorize]
    public class RecordController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Record
        [AllowAnonymous]
        public ActionResult Index(
            string searchTerm = null,
            string title = null,
            string author = null,
            string desc = null,
            string owner = null)
        {
            if (searchTerm == null || searchTerm.Equals(""))
            {
                return View(db.Records.ToList());
            }
            else
            {
                searchTerm = searchTerm.Trim();
                var model =
                    db.Records
                      .Where(r => (((title != null) && (-1 != r.Title.IndexOf(searchTerm)))
                                    || ((author != null) && (-1 != r.Author.IndexOf(searchTerm)))
                                    || ((desc != null) && (-1 != r.Description.IndexOf(searchTerm)))
                                    || ((owner != null) && (-1 != r.OwnerName.IndexOf(searchTerm)))
                                    ))
                      .Select(r => r);
                return View(model);
            }

        }


        // GET: Record/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = db.Records.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }

        // GET: Record/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Record/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Author,Description")] Record record)
        {
            if (ModelState.IsValid)
            {
                record.OwnerName = User.Identity.Name;
                db.Records.Add(record);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(record);
        }

        // GET: Record/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = db.Records.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }

        // POST: Record/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Author,Description")] Record record)
        {
            if (ModelState.IsValid)
            {
                record.OwnerName = User.Identity.Name;
                db.Entry(record).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(record);
        }

        // GET: Record/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = db.Records.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }

        // POST: Record/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Record record = db.Records.Find(id);
            db.Records.Remove(record);
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
