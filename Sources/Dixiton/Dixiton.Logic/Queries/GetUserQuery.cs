using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dixiton.Logic.Queries
{
    public class GetUserQuery : QueryBase
    {
        public static readonly Guid CMD_ID_GET_USER = new Guid("00927A4F-11C8-4FA6-AA80-97F462BDFE91");

        public override Guid QueryId
        {
            get { return CMD_ID_GET_USER; }
        }

        public string UserId { get; set; }
    }
}
