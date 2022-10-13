using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CurbashaApi.Data;

public class CurbashaApiContext : IdentityDbContext<IdentityUser>
{
    public CurbashaApiContext(DbContextOptions<CurbashaApiContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        
    }
}
