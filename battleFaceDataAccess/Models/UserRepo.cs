using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleFaceDataAccess.Models
{
    public class UserRepo
    {
        public static User SaveUser(ApplicationDBContext db, User user, string password)
        {
            if(!db.Users.Any(u => u.UserName == user.UserName))
            {
                db.Add(user);
                db.Add(new User_Key { Password = password, UserId = user.Id });
                db.SaveChanges();

                return user;
            }

            return null;
        }
    }
}
