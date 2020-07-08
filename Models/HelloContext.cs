using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HelloApi.Models
{
    public class HelloContext : DbContext
    {
        public DbSet<HelloItem> HelloItems { get; set; }

        public HelloContext(DbContextOptions<HelloContext> options)
            : base(options)
        {
        }

        public void Migrate()
        {
            Database.Migrate();
        }

        public void InitData(List<HelloItem> data)
        {
            if (HelloItems.Any())
            {
                return;
            }
            HelloItems.AddRange(data);
            SaveChanges();
        }
    }
}