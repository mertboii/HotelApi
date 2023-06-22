using HotelApiv3.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelApiv3.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {
            
        }

        public DbSet<Customer>? Customers { get; set; }

    }
}
