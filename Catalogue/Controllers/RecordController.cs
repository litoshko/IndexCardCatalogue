using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Catalogue.Models;
using Catalogue.Filters;

namespace Catalogue.Controllers
{
    [CustomErrorHandle]
    [Authorize]
    public class RecordController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Record
        /// <summary>
        /// Displays list of all records from index-catalog. 
        /// Performs filtering if corresponging patameters are specified
        /// </summary>
        /// <param name="searchTerm">Search string</param>
        /// <param name="title">'true' if needed search by record title</param>
        /// <param name="author">'true' if needed search by record author</param>
        /// <param name="desc">'true' if needed search by record description</param>
        /// <param name="owner">'true' if needed search by record owners username</param>
        /// <returns>List of filtered records.</returns>
        [AllowAnonymous]
        public ActionResult Index(string searchTerm = null,
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
                    db.Records.
                      Where(r => (((title != null) && (-1 != r.Title.IndexOf(searchTerm)))
                                    || ((author != null) && (-1 != r.Author.IndexOf(searchTerm)))
                                    || ((desc != null) && (-1 != r.Description.IndexOf(searchTerm)))
                                    || ((owner != null) && (-1 != r.OwnerName.IndexOf(searchTerm)))
                                    )).
                      Select(r => r);
                return View(model);
            }

        }

        // GET: Record/Create
        /// <summary>
        /// Returns Create Record View.
        /// </summary>
        /// <returns>Returns Create Record View.</returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: Record/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Saves Record model to database.
        /// </summary>
        /// <returns>Redirect to records list on success
        /// and create record view on failure (with model validation errors)</returns>
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
        /// <summary>
        /// Returns Edit Record View.
        /// </summary>
        /// <returns>Returns Edit Record View.</returns>
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
        /// <summary>
        /// Saves Record model to database.
        /// </summary>
        /// <returns>Redirect to records list on success
        /// and edit record view on failure (with model validation errors)</returns>
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
        /// <summary>
        /// Returns Delete Record View.
        /// </summary>
        /// <returns>Returns Delete Record View.</returns>
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
        /// <summary>
        /// Deltes Record model from database.
        /// </summary>
        /// <returns>Redirect to records list</returns>
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
