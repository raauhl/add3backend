using System;
using add3backend.Models;
using Microsoft.EntityFrameworkCore;

namespace add3backend
{
	public class MyDbContext : DbContext
	{
        public DbSet<Customer> customers { get; set; }

        public DbSet<Mint> mints { get; set; }

        public DbSet<Login> logins { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
    }
}

