using ComplaintService.cs.Models;
using Microsoft.EntityFrameworkCore;

namespace ComplaintService.cs.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Complaint> Complaints { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
    }
}
