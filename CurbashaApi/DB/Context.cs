using CurbashaApi.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurbashaApi.DB
{
    public class Context : DbContext
    {
        protected readonly IConfiguration Configuration;

        public Context(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }
        }

        public DbSet<UserAuth> UserAuth { get; set; }

        public DbSet<UserInfo> UserInfo { get; set; }

        public DbSet<UserItem> UserItem { get; set; }

        public DbSet<UserOrder> UserOrder { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
