using Microsoft.EntityFrameworkCore;
using RedisCacheDemo.Model;

namespace RedisCacheDemo.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Ciudades> tbCiudades { get; set; }
    }
}
