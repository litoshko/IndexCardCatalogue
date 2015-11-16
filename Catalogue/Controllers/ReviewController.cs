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
    /// <summary>
    /// Controller for performing CRUDL actions for Reviws model.
    /// </summary>
    [CustomErrorHandle]
    [Authorize]
    public class ReviewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Review/Index/2
        /// <summary>
        /// Method to get all information about specified record.
        /// </summary>
        /// <param name="recordId">Id for Record to return.</param>
        /// <returns>Record for specified id.</returns>
        [AllowAnonymous]
        public ActionResult Index([Bind(Prefix = "id")]int? recordId)
        {
            if (recordId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = db.Records.Find(recordId);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }

        // GET: Review/Details/5
        /// <summary>
        /// Allows getting detailed information about a given review.
        /// </summary>
        /// <param name="id">Id of review.</param>
        /// <returns>ActionResult with review model.</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Review/Create
        /// <summary>
        /// Returns Create review View.
        /// </summary>
        /// <param name="recordId">Id of record to add review for.</param>
        /// <returns>Returns Create review View.</returns>
        [AllowAnonymous]
        public ActionResult Create(int recordId)
        {
            return View();
        }

        // POST: Review/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Saves reviw model to database.
        /// </summary>
        /// <param name="review">Model from view to add to database.</param>
        /// <returns>Redirect to record details on success
        /// and create review view on failure (with model validation errors)</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Create(Review review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = review.RecordId });
            }

            return View(review);
        }

        // GET: Review/Edit/5
        /// <summary>
        /// Edit review.
        /// </summary>
        /// <param name="id">Specifies review to edit.</param>
        /// <returns>Edit review View.</returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Review/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Saves edited review.
        /// </summary>
        /// <param name="review">Review model to save.</param>
        /// <returns>Redirect to record details on success
        /// and create record view on failure (with model validation errors)</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Rating,Comment,RecordId")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = review.RecordId });
            }
            return View(review);
        }

        // GET: Review/Delete/5
        /// <summary>
        /// Delete review.
        /// </summary>
        /// <param name="id">Specifies review to delete.</param>
        /// <returns>Delete review View.</returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Review/Delete/5
        /// <summary>
        /// Deletes chosen review by id.
        /// </summary>
        /// <param name="review">Id of Review to be deleted.</param>
        /// <returns>Redirect to record details.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            int? recordId = review.RecordId;
            db.Reviews.Remove(review);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = recordId });
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
