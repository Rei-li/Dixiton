using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dixiton.Models
{
    public class LoginModel : EntityModelBase
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}