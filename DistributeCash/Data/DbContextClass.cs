using DistributedCash.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DistributedCash.Data;

public class DbContextClass : DbContext
{
    public DbContextClass(DbContextOptions<DbContextClass> options) : base(options)
    {
    }
    public DbSet<Product> Products { get; set; }
}
