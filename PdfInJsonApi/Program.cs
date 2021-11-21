using System.IO.Compression;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using PdfInJsonApi.Data;

/// <summary>
/// The program.
/// </summary>
public class Program
{
    /// <summary>
    /// The main.
    /// </summary>
    public static void Main(string[] args)
        => CreateHostBuilder(args).Build().Run();

    /// <summary>
    /// The create host builder.
    /// </summary>
    /// <remarks>
    /// EF Core uses this method at design time to access the DbContext
    /// </remarks>
    public static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
                webBuilder => webBuilder.UseStartup<Startup>());
}

/// <summary>
/// The startup class
/// </summary>
public class Startup
{
    /// <summary>
    /// The configuration of the application.
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// Default constructor
    /// </summary>
    public Startup(IConfiguration configuration) => Configuration = configuration;

    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    public void ConfigureServices(IServiceCollection services) 
    {
        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(setup => {
            setup.IncludeXmlComments($@"{AppContext.BaseDirectory}\PdfInJsonApi.xml", true);
        });

        // Add oracle database connection
        services.AddDbContext<PdfContext>(options =>
            options.UseOracle(Configuration.GetConnectionString("PdfContext"), o => o.UseOracleSQLCompatibility("11")));

        // Add automapper
        services.AddAutoMapper(typeof(Startup));

        // Add response compression
        services.Configure<GzipCompressionProviderOptions>(options => 
        {
            options.Level = CompressionLevel.Fastest;
        });

        services.AddResponseCompression();
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PdfContext context)
    {
        // Apply database migrations
        context.Database.Migrate();

        // Add response compression middleware
        app.UseResponseCompression();

        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
