using Linkdout.Dal;
using Linkdout.DTO;
using Linkdout.Models;
using Microsoft.EntityFrameworkCore;

namespace Linkdout.Services
{
    public class PostService
    {
        private DataLayer db;

        public PostService(DataLayer _db)
        {
            db = _db;
        }
        // פונקצייה שמחזירה את כל הפוסטים
        public async Task<List<PostModel>> GetAll()
        {
            return db.Posts.Include(p => p.User).ToList();
        }
        // פונקצייה שמחזירה את פוסט אחד
        public async Task<PostModel> GetPostById(int id)
        {
            return db.Posts.Include(p => p.User).FirstOrDefault(p => p.id == id);
        }

        // פונקצייה שיוצרת פוסט
        public async Task<bool> AddNewPost(NewPostDTO req)
        {
            try
            {
                UserModel user = db.Users.Find(req.UserId);
                req.Post.User = user;
                db.Posts.Add(req.Post);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;

            }


        }

        // פונקצייה שעורכת פוסט
        public async Task<string> EditPostBody(int PostId, string NewBody)
        {
        PostModel post = db.Posts.Find(PostId);
            string oldBody = post.Body;
            post.Body = NewBody;
            db.SaveChanges();
            return oldBody;

        }

        // פונקצייה שמוחקת פוסט
        public async Task<int> DeletPost(int PostId)
        {
            try
            {
                PostModel post = db.Posts.Find(PostId);
                db.Posts.Remove(post);
                db.SaveChanges();
                return PostId;

            }
            catch (Exception)
            {

                return -1;
            }
           
                   
            

        }


    }
}
