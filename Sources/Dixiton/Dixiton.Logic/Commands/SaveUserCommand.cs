using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dixiton.Dtos;

namespace Dixiton.Logic.Commands
{
    public class SaveUserCommand : CommandBase
    {
        public static readonly Guid CMD_ID_SAVE_USER = new Guid("754FDCFB-3759-4131-ABAE-6CDEB31926DD");

        public override Guid CommandId
        {
            get { return CMD_ID_SAVE_USER; }
        }

        /// <summary>
        /// ProjectDto to filter result
        /// </summary>
        public UserDto UserDto { get; set; }
    }
}
