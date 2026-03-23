using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApplication1.Models;

namespace WebApplication1.Controllers;
[Route("api/[controller]")]
//declare that the controller's actions support a response content type of application/json:
[Produces("application/json")]
[ApiController]
public class MovieController : ControllerBase
{
    //the ApplicationDbContext instance is passed to the controller through constructor injection for each request
    // to perform a unit-of-work before being disposed when the request ends.
    private readonly MovieDbContext _context;

    public MovieController(MovieDbContext context)
    {
        _context = context;
    }
    
    //Adding triple-slash comments to an action enhances the Swagger UI by adding the description to the section header.
    // GET: api/<MovieController>
    /// <summary>
    /// Get the movies
    /// </summary>
    /// <returns>list of movies</returns>
    [HttpGet]
    public async Task<IEnumerable<Movie>> Get()
    {
            return await _context.Movies
                .ToListAsync();
        
    }

    // GET api/<MovieController>/5
    /// <summary>
    /// Returns a movie given its ID
    /// </summary>
    /// <param name="id">ID of the movie to be found</param>
    /// <returns>The related movie if found. Null otherwise</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Movie> Get(int id)
    {
        return await _context.Movies
            .Where(m => m.Id == id)
            .SingleAsync();
    }

    // POST api/<MovieController>
    /// <summary>
    /// add a movie to the list
    /// </summary>
    ///  <param name="value">Movie to be added</param>
    /// /// <remarks>
    /// Sample request:
    ///
    ///     POST /movie
    ///     {
    ///        "PublicationYear" : 2008,
    ///        "Rating":7.9,
    ///        "Stars" : [ "Robert Downey Jr.", "Gwyneth Paltrow", "Terrence Howard", "Jeff Bridges" ]
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="400">If the item is null</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public void Post([FromBody] Movie value)
    {
          _context.Movies
            .Add(value);
         _context.SaveChangesAsync();
    }

    // DELETE api/<MovieController>/5
    /// <summary>
    /// Deletes a specific movie.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<int> Delete(int id)
    {
          return await _context.Movies
            .Where(m => m.Id == id)
            .ExecuteDeleteAsync();
    }
}

/*
 {
        "Title":"Iron Man",
        "PublicationYear" : 2008,
        "Rating":7.9,
        "Stars" : [ "Robert Downey Jr.", "Gwyneth Paltrow", "Terrence Howard", "Jeff Bridges" ]
 }
  {
       "Title":"Thor",
       "PublicationYear" : 2011,
       "Rating":7,
       "Stars" : [ "Chris Hemsworth", "Anthony Hopkins", "Natalie Portman", "Tom Hiddleston" ]
   }
   {
       "Title":"Guardians of the Galaxy",
       "PublicationYear" : 2014,
       "Rating":8,
       "Stars" : [ "Chris Pratt", "Vin Diesel", "Bradley Cooper", "Zoe Saldana" ]
   }
 */