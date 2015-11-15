﻿using Catalogue.Filters;
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
        public ActionResult Index()
        {
            return View();
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

        [Authorize(Roles = RoleController.ADMIN_ROLE_NAME)]
        public ActionResult Administration()
        {
            ViewBag.Message = "Your administraion page.";

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}