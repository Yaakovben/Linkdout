namespace Linkdout.Models
{
    public class PostModel
    {
        public int? id {  get; set; }
        public string? Body { get; set; }

        public UserModel? User { get; set; }

        public int? Likes { get; set; }



    }
}
