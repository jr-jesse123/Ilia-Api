using ILIA.SimpleStore.Persistence;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
        c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Simple Store API",
                Version = "v1",
                Description = "Api to execute operations related to Clients and Customers",
                
                Contact = new OpenApiContact
                {
                    Name = "Jessé Junior",
                    Email = "jesse@quatrodconsultoria.com.br",
                    Url = new Uri("https://www.linkedin.com/in/junior-jesse/"),
                }
            });
        }
    );

//swaggerGenOptions.IncludeXmlComments(xmlDocFile)

builder.Services.AddDefaultPersisntece();
builder.Services.AddAutoMapper( Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
