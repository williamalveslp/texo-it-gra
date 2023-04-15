using GRA.API.IoC;
using GRA.API.Middlewares;
using GRA.Application.AppInterfaces.MoviesFeature;
using GRA.Application.AutoMapper.MoviesFeature;
using GRA.Infra.DataStore.EntityFrameworkCore.Startup;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen();

    // Register layers.
    builder.Services.RegisterGeneralLayers();

    // EntityFramework Core.
    builder.Services.AddEntityFrameworkCoreExtension();

    // AutoMapper.
    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.AddAutoMapper(typeof(MovieMapping));

    //-------------------------------------------------------------------------------------------------------

    var app = builder.Build();

    // CORS.
    app.UseCors(x => x.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod());

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Exception Handling.
    app.UseGlobalExceptionHandler();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    // Import Movies.
    await ImportMoviesFromCsvToMemory(builder, app);

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
finally
{
    Console.WriteLine("Server Shutting down...");
}

static async Task ImportMoviesFromCsvToMemory(WebApplicationBuilder builder, WebApplication app)
{
    var serviceScope = app.Services.CreateScope();
    IMovieAppService? movieAppService = serviceScope.ServiceProvider.GetService<IMovieAppService>();

    if (movieAppService == null)
    {
        Console.Write($"Service \"{typeof(IMovieAppService)}\" not found.");
        return;
    }

    string csvFilePathFromConfiguration = builder.Configuration["CsvFilePath"];
    var response = await movieAppService.ImportFromCsv(csvFilePathFromConfiguration);

    if (response == null || !response.Success)
        Console.WriteLine(response?.MessageError ?? "Error ocorreu ao importar os dados.");
}