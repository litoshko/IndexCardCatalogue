using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalogue.Models
{
    /// <summary>
    /// View model which is used to display user roles.
    /// </summary>
    public class UserRoleViewModel
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}