using System;
using c_sharp_angular.Entities;
using Microsoft.EntityFrameworkCore;

namespace c_sharp_angular.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }
    }
}

