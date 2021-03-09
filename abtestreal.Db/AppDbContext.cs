using System;
using System.Runtime.CompilerServices;
using abtestreal.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace abtestreal.Db
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
         public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
         {
             
         }
    }
}