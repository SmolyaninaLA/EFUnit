using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFUnit 
{
    public class UserRepository
    {

        public void AddUser(User[] users)
        {
          
            using (var db = new AppContext())
            {
                var userAdd = users.Where(p => p.Id == GetUserId(p)).ToArray();
               
                db.Users.AddRange(userAdd);
                db.SaveChanges();
                
              
            }
        }

        public void RemoveUser(User[] users)
        {
            using (var db = new AppContext())
            {
                var userRemove = users.Select(u => new User { Id = GetUserId(u), FirstName = u.FirstName, LastName = u.LastName, Email = u.Email}).Where(u => u.Id > 0).ToArray();
                if (userRemove.Length > 0)
                {
                    db.Users.RemoveRange(userRemove);
                    db.SaveChanges();
                }
             
            }
        }

        public void ChangeUser(User userOld, User userNew)
        {
            using (var db = new AppContext())
            {
                var findIdUser = GetUserId(userOld);

                if (findIdUser > 0)
                {
                    var user = db.Users.FirstOrDefault(u => u.Id == findIdUser);

                    user.FirstName = userNew.FirstName;
                    user.LastName = userNew.LastName;
                    user.Email = userNew.Email;
                   
                    db.SaveChanges();
                }
            }
        }

        public IEnumerable<User> FindUserId(int id)
        {
            using (var db = new AppContext())
            {
                var findUser = db.Users.Where(p => p.Id == id).ToList();
                return findUser;
            }
         }

        public void SelectAll()
        {
            using (var db = new AppContext())
            {
                var AllUser = db.Users.Select(p => p).ToList();
                foreach (var u in AllUser )
                {
                    Console.WriteLine(u.FirstName + " " + u.LastName);
                }

            }
        }


        public int GetUserId(User userData) 
        {
            int ret = 0;

            using (var db = new AppContext())
            {
              
                var findUser = db.Users.FirstOrDefault(p => p.FirstName == userData.FirstName &&
                                                  p.LastName == userData.LastName &&
                                                  p.Email == userData.Email);

                if (findUser != null)
                    ret = findUser.Id;

            }


            return ret;
        }
        
    }
}
