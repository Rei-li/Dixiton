using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dixiton.DataAccess.Entities
{
    public class UserEntity : EntityBase
    {
            public virtual string Login { get; set; }
            public virtual string Password { get; set; }
            public virtual string Email { get; set; }
    }
}
