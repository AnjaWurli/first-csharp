using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.OpenApi;

namespace WebApplication1.Models;

public class Movie
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// movie title
    /// </summary>
    [Required]
    public string Title { get; set; }= string.Empty;
    
    /// <summary>
    /// year of publication
    /// </summary>
    public int PublicationYear { get; set; }
    
    [DefaultValue(3)]
    public float Rating { get; set; }
    public string[]? Stars { get; set; }

}