using Catalogue.Filters;
using Catalogue.Models;
using Elmah;
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
    /// <summary>
    /// Controller for Identity Roles management.
    /// Allows to get ActionResults for listing and viewing users,
   ///  adding and removing roles.
    /// </summary>
    [CustomErrorHandle]
    [Authorize(Roles = ADMIN_ROLE_NAME)]
    public class RoleController : Controller
    {
        public const string ADMIN_ROLE_NAME = "Admin";
        ApplicationDbContext _db = new ApplicationDbContext();

        /// <summary>
        /// Get roles for specified user
        /// </summary>
        /// <param name="user">User to get roles for.</param>
        /// <returns></returns>
        private static string GetRoles(ApplicationUser user)
        {
            return (user.Roles.Count() > 0) ? ADMIN_ROLE_NAME : "";
        }

        // GET: Role
        /// <summary>
        /// Index action for Role.
        /// </summary>
        /// <returns>ActionResult containing a list of all users and their roles</returns>
        public ActionResult Index()
        {
            var users = _db.Users.ToList();
            var roles = _db.Roles.ToList();
            var model = users.Select(user => new UserRoleViewModel
                {
                    UserName = user.UserName,
                    RoleName = GetRoles(user)
                }).
                ToList();

            return View(model);
        }

        // GET: Role/Details/x@x.ua
        /// <summary>
        /// Displays detailed information about specified user.
        /// </summary>
        /// <param name="userName">Name of user to get information for</param>
        /// <returns>ActionResult containing all information from user databaase table</returns>
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
        /// <summary>
        /// Display View to prompt to add Admin role for user.
        /// </summary>
        /// <param name="userName">Username to add to Admins</param>
        /// <returns>ActionResult containing UserRoleViewModel</returns>
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
        /// <summary>
        /// POST action to add admin to specified user roles.
        /// </summary>
        /// <param name="userRole">UserRoleViewModel parameter contains user name to add to admin.</param>
        /// <returns>ActionResult to return to list of all users on success and return back to 
        /// addAdmin view if an error has happened.</returns>
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
            catch (Exception ex)
            {
                //ELMAH Signaling.
                ErrorSignal.FromCurrentContext().Raise(ex);
                return View();
            }
        }

        // GET: Role/Delete/user
        /// <summary>
        /// Display View to prompt to remove Admin role for user.
        /// </summary>
        /// <param name="userName">Username to remove from Admins</param>
        /// <returns>ActionResult containing UserRoleViewModel</returns>
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
        /// <summary>
        /// POST action to remove admin role for given user.
        /// </summary>
        /// <param name="userRole">UserRoleViewModel parameter contains user name.</param>
        /// <returns>ActionResult to return to list of all users on success and return back to 
        /// delete view if an error has happened.</returns>
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
            catch (Exception ex)
            {
                //ELMAH Signaling.
                ErrorSignal.FromCurrentContext().Raise(ex); 
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
