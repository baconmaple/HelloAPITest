using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace HelloApi.Models
{
  public static class SeedData
  {
    public static void Initialize(IServiceProvider serviceProvider)
    {
      using (var context = new HelloContext(
          serviceProvider.GetRequiredService<
              DbContextOptions<HelloContext>>()))
      {
        // Look for any movies.
        if (context.HelloItems.Any())
        {
          return;   // DB has been seeded
        }

        context.HelloItems.AddRange(
          new HelloItem
          {
            Name = "initial data 1",
            Balance = 210.53
          },
          new HelloItem
          {
            Name = "initial data 2",
            Balance = 320
          },
          new HelloItem
          {
            Name = "initial data 3",
            Balance = 1563.98
          },
          new HelloItem
          {
            Name = "initial data 4",
            Balance = 658.3
          }
        );
        context.SaveChanges();
      }
    }
  }
}