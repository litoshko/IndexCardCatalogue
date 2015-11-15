using Catalogue.Filters;
using Catalogue.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Catalogue.Controllers
{
    [CustomErrorHandle]
    [Authorize(Roles = ADMIN_ROLE_NAME)]
    public class RoleController : Controller
    {
        public const string ADMIN_ROLE_NAME = "Admin";
        ApplicationDbContext _db = new ApplicationDbContext();

        // Get roles for specified user
        private static string GetRoles(ApplicationUser user)
        {
            return (user.Roles.Count() > 0) ? ADMIN_ROLE_NAME : "";
        }

        // GET: Role
        public ActionResult Index()
        {
            var users = _db.Users.ToList();
            var roles = _db.Roles.ToList();
            var model = users.Select(user => new UserRoleViewModel
                {
                    UserName = user.UserName,
                    RoleName = GetRoles(user)
                })
                .ToList();

            return View(model);
        }

        // GET: Role/Details/5
        public ActionResult Details(string userName)
        {
            if (userName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _db.Users.Where(u => u.UserName.Equals(userName)).First();
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Role/AddAdmin/user
        public ActionResult AddAdmin(string userName)
        {
            if (userName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _db.Users.Where(u => u.UserName.Equals(userName)).First();
            if (user == null)
            {
                return HttpNotFound();
            }
            if (GetRoles(user).Equals(ADMIN_ROLE_NAME))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(new UserRoleViewModel
                            {
                                UserName = userName,
                                RoleName = GetRoles(user)
                            });
        }

        // POST: Role/AddAdmin/user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAdmin(UserRoleViewModel userRole)
        {
            try
            {
                var user = _db.Users.Where(u => u.UserName.Equals(userRole.UserName)).First();
                var store = new UserStore<ApplicationUser>(_db);
                var manager = new UserManager<ApplicationUser>(store);

                manager.AddToRole(user.Id, ADMIN_ROLE_NAME);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Role/Delete/user
        public ActionResult Delete(string userName)
        {
            if (userName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _db.Users.Where(u => u.UserName.Equals(userName)).First();
            if (user == null)
            {
                return HttpNotFound();
            }
            if (GetRoles(user).Equals(""))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(new UserRoleViewModel
                        {
                            UserName = userName,
                            RoleName = GetRoles(user)
                        });
        }

        // POST: Role/Delete/user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UserRoleViewModel userRole)
        {
            try
            {
                var user = _db.Users.Where(u => u.UserName.Equals(userRole.UserName)).First();
                var store = new UserStore<ApplicationUser>(_db);
                var manager = new UserManager<ApplicationUser>(store);

                manager.RemoveFromRole(user.Id, ADMIN_ROLE_NAME);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
