using Microsoft.EntityFrameworkCore;
using RealEstate.Data.Models;

namespace RealEstate.Data
{
    public class RealEstateDbContext : DbContext
    { 
        public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options) : base(options){}
        public RealEstateDbContext(){}
    }
}
