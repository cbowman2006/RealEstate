using Microsoft.EntityFrameworkCore;
using RealEstate.Data.Models;

namespace RealEstate.Data
{
    public class RealEstateDbContext : DbContext
    { 
        public DbSet<User> Users { get; set; }
        public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options) : base(options){}
        public RealEstateDbContext(){}
    }
}
