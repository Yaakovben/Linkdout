using Linkdout.Dal;
using Linkdout.Models;

namespace Linkdout.Services
{
    public class UserService
    {
        private DataLayer db;

        public UserService(DataLayer _db) 
        { 
            db = _db; 
        }

        //ID פונקציית מציאת יוזר לפי 
        public async Task <UserModel> GetUserById(int id)
        {
            return db.Users.Find(id);
        }

        //פונקציית הרשמה
        public async Task<int> Register(UserModel user)
        {
            string uhpw = user.Password;
            // hash the password
            string hashedqw = BCrypt.Net.BCrypt.HashPassword(uhpw);
            //save the hashed pw in the user
            user.Password = hashedqw;
            db.Users.Add(user);
            db.SaveChanges();
            UserModel? created = db.Users.FirstOrDefault(u => u.UserName == user.UserName);
            return created.Id;
        }


        public async Task<UserModel> GetUserByUserNameAndPassword(string un, string uhpw)
        {
            UserModel user = db.Users.FirstOrDefault(u => u.UserName == un);
            bool isVerified = BCrypt.Net.BCrypt.Verify(uhpw, user.Password);

            return isVerified ? user : null;
        }



    }
}
