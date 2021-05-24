using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dottle.Web.ViewModels
{
    public class UserEditModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
