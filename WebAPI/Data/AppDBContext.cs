using System;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI;

public class AppDBContext : DbContext
{
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Student { get; set; }
        public DbSet<Major> Major { get; set; }
}
