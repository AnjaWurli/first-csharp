using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Models;

//The preceding code defines the database context, which is the main class that coordinates Entity Framework functionality for a data model.
//This class derives from the Microsoft.EntityFrameworkCore.DbContext class.
public class MovieDbContext : DbContext
{
    //public constructor for passing context configuration from AddDbContext to the DbContext
    //https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/
    public MovieDbContext(DbContextOptions<MovieDbContext> options) 
        : base(options) { }

    //manage the connection between the model and the database
    public DbSet<Movie> Movies => Set<Movie>();
}