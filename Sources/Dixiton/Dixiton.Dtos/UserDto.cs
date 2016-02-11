using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dixiton.Common;

namespace Dixiton.Dtos
{
    public class UserDto : EntityDtoBase
    {
        #region [ Property names ]

        public static readonly string PROP_LOGIN = TypeHelper<UserDto>.PropertyName(x => x.Login);
        public static readonly string PROP_PASSWORD = TypeHelper<UserDto>.PropertyName(x => x.Password);
        public static readonly string PROP_CONFIRM_PASSWORD = TypeHelper<UserDto>.PropertyName(x => x.ConfirmPassword);
        public static readonly string PROP_EMAIL = TypeHelper<UserDto>.PropertyName(x => x.Email);

        #endregion

       
        public string Login { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
    }
}
