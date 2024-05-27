using Microsoft.EntityFrameworkCore;
using Web.Data.Entities;

namespace Web.Data;

public class DataContext:DbContext
{
    public DataContext(DbContextOptions<DataContext> options):base(options){}

    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    =>  base.OnModelCreating(modelBuilder);
    
}
