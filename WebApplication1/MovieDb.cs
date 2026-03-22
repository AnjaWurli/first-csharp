using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1;

//The preceding code defines the database context, which is the main class that coordinates Entity Framework functionality for a data model.
//This class derives from the Microsoft.EntityFrameworkCore.DbContext class.
class MovieDb : DbContext
{
    public MovieDb(DbContextOptions<MovieDb> options)
        : base(options) { }

    public DbSet<Movie> Movies => Set<Movie>();
}