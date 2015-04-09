using Lisa.Breakpoint.Models;
using System.Data.Entity;

namespace Lisa.Breakpoint.WebApi.Data
{
    public class Context : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
    }
}