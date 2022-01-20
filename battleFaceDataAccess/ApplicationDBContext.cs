using battleFaceDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleFaceDataAccess
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<User_Key> User_Keys { get; set; }
        public DbSet<Quote> Quotes { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<User>()
        //        .ToTable("dbo.Users");
        //    builder.Entity<User_Key>()
        //        .ToTable("dbo.User_Keys");
        //}
    }
}
