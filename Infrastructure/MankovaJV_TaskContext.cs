using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TestTasks__API_;

public partial class MankovaJV_TaskContext : DbContext
{
    public MankovaJV_TaskContext()
    {
    }

    public MankovaJV_TaskContext(DbContextOptions<MankovaJV_TaskContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pizza> Pizzas { get; set; }

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<MankovaJV_TaskContext>(options => options.UseSqlServer(connectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
