using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;
[Route("api/[controller]")]
//declare that the controller's actions support a response content type of application/json:
[Produces("application/json")]
[ApiController]
public class MovieController : ControllerBase
{
    //Adding triple-slash comments to an action enhances the Swagger UI by adding the description to the section header.
    // GET: api/<MovieController>
    /// <summary>
    /// Get the movies
    /// </summary>
    /// <returns>list of movies</returns>
    [HttpGet]
    public IEnumerable<Movie> Get()
    {
        return movies;
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
    public Movie Get(int id)
    {
        return movies.FirstOrDefault(x => x.Id == id);
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
    ///        "id": 1,
    ///        "name": "Item #1",
    ///        "isComplete": true
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
        movies.Add(value);
    }

    // DELETE api/<MovieController>/5
    /// <summary>
    /// Deletes a specific movie.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        movies.RemoveAll(m => m.Id == id);
    }
    
    private static readonly List<Movie> movies = new List<Movie>() {
        new Movie{
            Id = 1,
            Title="Iron Man",
            PublicationYear = 2008,
            Rating=7.9f,
            Stars = new []{ "Robert Downey Jr.", "Gwyneth Paltrow", "Terrence Howard", "Jeff Bridges" }
        },
        new Movie{
            Id = 2,
            Title="Thor",
            PublicationYear = 2011,
            Rating=7f,
            Stars = new []{ "Chris Hemsworth", "Anthony Hopkins", "Natalie Portman", "Tom Hiddleston" }
        },
        new Movie{
            Id = 3,
            Title=" Guardians of the Galaxy",
            PublicationYear = 2014,
            Rating=8f,
            Stars = new []{ "Chris Pratt", "Vin Diesel", "Bradley Cooper", "Zoe Saldana" }
        }
    };
}