using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductWebAPI.AppContext
{
    public class ApplicationContext : DbContext
    {
         
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Complaint> Complaintes { get; set; }
    }
}
