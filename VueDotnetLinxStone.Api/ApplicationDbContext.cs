using Microsoft.EntityFrameworkCore;
using VueDotnetLinxStone.Api.Entities;

namespace VueDotnetLinxStone.Api
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
    }
}
