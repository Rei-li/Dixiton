using Dixiton.DataAccess.Entities;

namespace Dixiton.DataAccess.NHMappings
{
   
    public class UserMapping 
        : ClassMapBase<UserEntity>
    {
        public UserMapping()
        {
            Map(v => v.Login);
            Map(v => v.Password);
            Map(v => v.Email);
         
        }
    }
}