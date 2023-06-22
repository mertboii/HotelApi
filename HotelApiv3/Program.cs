using HotelApiv3.Authentication;
using HotelApiv3.Controllers;
using HotelApiv3.Data;
using HotelApiv3.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

//configure services
builder.Services.AddTransient<EntityService>();
builder.Services.AddTransient<HashService>();
builder.Services.AddTransient<DbService>();


string connectionString = builder.Services.BuildServiceProvider().GetService<DbService>().GetConnectionString();


//appDbContext configuration

builder.Services.AddDbContext<AppDbContext>
    (options => options.UseSqlServer
    (connectionString));


//add controllers
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "The API key to access the API",
        Type = SecuritySchemeType.ApiKey,
        Name = "x-api-key",
        In = ParameterLocation.Header,
        Scheme="ApiKeyScheme"
        
    });

    var scheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"

        },
        In = ParameterLocation.Header
    };
    var requirement = new OpenApiSecurityRequirement
    {
        {scheme, new List<string>() }
    };
    x.AddSecurityRequirement(requirement);
});


builder.Services.AddScoped<ApiKeyAuthFilter> ();


// Build the application.
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseMiddleware<ApiKeyAuthMiddleware>();

app.UseAuthorization();

app.MapControllers();

//seed database
//AppDbInitializer.Seed(app);



app.Run();


