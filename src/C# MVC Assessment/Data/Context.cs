using Microsoft.EntityFrameworkCore;
using C__MVC_Assessment.Models;
using System.Collections.Generic;

namespace C__MVC_Assessment.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }


    }
}
