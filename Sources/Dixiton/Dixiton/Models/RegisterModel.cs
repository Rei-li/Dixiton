using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dixiton.Models
{
    public class RegisterModel: EntityModelBase
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
       
    }
}