using Linkdout.Models;
using Microsoft.EntityFrameworkCore;

namespace Linkdout.Dal
{
    public class DataLayer: DbContext
    {
        public DbSet<UserModel> Users { get; set; } 

        public DbSet<PostModel> Posts { get; set; }

        public DataLayer(DbContextOptions<DataLayer> options) : base(options) { Database.EnsureCreated(); }

    }
}
