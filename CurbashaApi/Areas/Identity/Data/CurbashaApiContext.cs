using CurbashaApi.Areas.Identity.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CurbashaApi.Data;

public class CurbashaApiContext : IdentityDbContext<IdentityUser>
{
    public DbSet<AspUserOrder> AspUserOrders { get; set; }

    public DbSet<AspOrderItem> AspUserItems { get; set; }

    public CurbashaApiContext(DbContextOptions<CurbashaApiContext> options)
        : base(options)
    {
    }

    //protected override void OnModelCreating(ModelBuilder builder)
    //{
    //    base.OnModelCreating(builder);
    //}
}
