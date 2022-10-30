using CurbashaApi.Areas.Identity.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CurbashaApi.Data;

public class CurbashaApiContext : IdentityDbContext<IdentityUser>
{
    public DbSet<AspOrderItem> AspOrderItems { get; set; }

    public DbSet<AspProduct> AspProducts { get; set; }

    public DbSet<AspSelections> AspSelections { get; set; }

    public DbSet<AspUserOrder> AspUserOrder { get; set; }

    public CurbashaApiContext(DbContextOptions<CurbashaApiContext> options)
        : base(options)
    {
    }

    //protected override void OnModelCreating(ModelBuilder builder)
    //{
    //    base.OnModelCreating(builder);
    //}
}
