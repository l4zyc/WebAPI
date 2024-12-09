using Microsoft.EntityFrameworkCore;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Add services to the container.
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();   

//Dependency Injection
builder.Services.AddDbContext<AppDBContext>(
    options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
           .EnableSensitiveDataLogging() 
           .EnableDetailedErrors());

builder.Services.AddEndpointsApiExplorer(); //Prepares the app for minimal APIs (used for generating OpenAPI documentation).
builder.Services.AddSwaggerGen(); //Add Swagger as a Documentation API
builder.Services.AddControllers(); //Register Support For API Controller

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyHeader(); //Any domain can make requests.
        policy.AllowAnyMethod(); //Any HTTP headers can be sent with the request.
        policy.AllowAnyOrigin(); //HTTP GET, POST, PUT, DELETE, etc., are permitted.
    });
}); //Configures a CORS policy named "AllowAll" to accept requests from any origin, with any method and header.

var app = builder.Build(); //Builds the application using the configured services. The one above

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); //Redirect to the Error page if there is an error on the Razor Page
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts(); // Enables HSTS (HTTP Strict Transport Security) to enforce HTTPS.

    app.UseSwagger(); //Activates Swagger for API documentation.
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); //Redirects HTTP requests to HTTPS.

app.UseStaticFiles();

app.UseRouting(); //Sets up routing for the app. //Track

app.UseAuthentication();
app.UseAuthorization(); //Adds authentication and authorization middleware.

app.MapRazorPages(); //maps routes for Razor Pages (e.g., /Students). //Destination Razor Pages
app.MapControllers(); //maps routes for API controllers (e.g., /api/students). //Destination Controller

app.Run();