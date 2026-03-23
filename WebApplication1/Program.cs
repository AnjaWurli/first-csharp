using System.Reflection;
using Microsoft.OpenApi;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args); //makes configured services available throughout the app
//initialize new instance of WebApplication class with preconfigured defaults

// Add services to the container.

//builder.Services.AddControllersWithViews(); //common usage for MVC
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();//only for minimal APIs

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    // using System.Reflection; is used to build an XML file name matching that of the web API project
    //The AppContext.BaseDirectory property is used to construct a path to the XML file
    // https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-8.0&tabs=visual-studio
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

});

//connection string includes the source database name, set in appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                       ?? throw new InvalidOperationException("Connection string" + "'DefaultConnection' not found.");;

//adds the database context to the dependency injection (DI) container
//adds PostgresSQL as db provider (each DbContext instance must be configured to use exactly 1 db provider)
//builder.AddNpgsqlDbContext<MovieDbContext>(connectionName: "DefaultConnection");
builder.Services.AddDbContext<MovieDbContext>(opt => opt.UseNpgsql(connectionString));


var app = builder.Build(); //configure a host with appsettings.json

// Configure the HTTP request pipeline. /Middleware: (order matters!)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");//catches exceptions thrown in the following middlewares
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();//adds header for HTTP Strict Transport Security Protocol (HSTS)
} else if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();//redirects HTTP requests to HTTPS
app.UseRouting(); //to route requests, If not called, runs at the beginning of the pipeline by default

app.UseAuthorization();//authorizes a user to access secure resources

app.MapControllers();
 
/*
app.MapStaticAssets();// Serve static files (CSS, JS, etc.)
app.MapControllerRoute( // For MVC controllers
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
*/

//creates the OpenAPI file under /swagger/v1/swagger.json
app.UseSwagger();

//define where to find the swagger.json file and what is the title of the page.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); 
    //serve the Swagger UI at the app's root:
    c.RoutePrefix = string.Empty;
});

//when the API container starts, it will automatically create the database tables inside the PostgreSQL container 
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
    db.Database.Migrate();
} 



app.Run();