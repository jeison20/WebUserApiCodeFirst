using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Polly;
using System.Reflection;
using WebApiUsers.Application;
using WebApiUsers.Domain;
using WebApiUsers.EFCore;
using WebApiUsers.EFCore.DataContext;
using WebUserApiCodeFirst.Config;

var builder = WebApplication.CreateBuilder(args);

//Se realiza la referencia para el manejo de la injeccion de dependencias
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration, "DbConection");

//Realiza la busqueda de la cadena de conexion y Registra el contexto con el Entity Framework ademas de establecer mejoras para la resilencia para mitigar posibles intermitencias con la bd
builder.Services.AddDbContext<WebApiContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DbConection"], sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
        maxRetryCount: 2,
        maxRetryDelay: TimeSpan.FromSeconds(50),
        errorNumbersToAdd: null);
    });
});

// Establece politicas de reintentos con polly
var httpRetryPolicy = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode).WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(50));

builder.Services.AddSingleton<IAsyncPolicy<HttpResponseMessage>>(httpRetryPolicy);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Users and Wather API",
        Description = "An ASP.NET Core Web API for manage of Users",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Jeison Vargas",
            Url = new Uri("https://www.linkedin.com/in/jeison-vargas-b66a78b4")
        },
        License = new OpenApiLicense
        {
            Name = "License MIT",
            Url = new Uri("https://example.com/license")
        }
    });
    //Se usa para que utilize los comentarios Xml generados en los endpoints
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

//Realiza el manejo de las excepciones centralizandolo y en caso de tener una exception no controlada llegaria aca y evitaria que se envie a cliente informacion que podria ser usada para ataques hacia la plataforma
app.UseExceptionHandler(error =>
{

    error.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

        if (contextFeature != null)
        {
            await context.Response.WriteAsync(new Error
            {
                StatusCode = context.Response.StatusCode,
                Message = contextFeature.Error.Message.Contains("Error:") ? (contextFeature.Error.Message).Replace("Error:", "") : "Internal Server Error. Please Try Again Later.",
                Path = context.Request.Path
            }.ToString());
        }
    });
});

var logger = app.Services.GetRequiredService<ILogger<Program>>();

// aplica las migraciones
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<WebApiContext>();
    try
    {
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

// Esta validacion es para que el swagger solo se visible en desarrollo

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiUser"));



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
