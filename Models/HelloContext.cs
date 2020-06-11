using Microsoft.EntityFrameworkCore;

namespace HelloApi.Models
{
  public class HelloContext : DbContext
  {
    public HelloContext(DbContextOptions<HelloContext> options)
        : base(options)
    {
    }

    public DbSet<HelloItem> HelloItems { get; set; }
    
  }
}