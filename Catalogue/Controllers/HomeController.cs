using Catalogue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catalogue.Controllers
{
    public class HomeController : Controller
    {
        CatalogueContext _dbContext = new CatalogueContext();

        public ActionResult Index()
        {
            var model = _dbContext.Records.ToList();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}