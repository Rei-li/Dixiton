using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dixiton.Logic.Queries
{
    public class IdentifyUserQuery : QueryBase
    {
        public static readonly Guid CMD_ID_IDENTIFY_USER = new Guid("7D8F10CB-D063-4B5A-BC2B-410C388E2BDB");

        public override Guid QueryId
        {
            get { return CMD_ID_IDENTIFY_USER; }
        }

        public string Login { get; set; }
        public string Password { get; set; }
    }
}
