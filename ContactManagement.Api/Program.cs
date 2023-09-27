using Ardalis.ListStartupServices;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ContactManagement.Api;
using ContactManagement.BlazorShared.Models.ContactModels.Create;
using ContactManagement.Core;
using ContactManagement.Infrastructure;
using ContactManagement.Infrastructure.Data;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Serilog;

//TODO: Clean this up

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = _ => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

string? connectionString = builder.Configuration.GetConnectionString("SqliteConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicy", b =>
    {
        b.AllowAnyOrigin();
        //b.AllowAnyHeader();
        b.WithHeaders("Origin", "X-Requested-With", "Content-Type", "Accept");
        b.AllowAnyMethod();
    });
});

builder.Services.AddFastEndpoints(o =>
{
    o.IncludeAbstractValidators = true;
    o.Assemblies = new[] {typeof(CreateContactRequest).Assembly};
});

//builder.Services.AddFastEndpointsApiExplorer();
builder.Services.SwaggerDocument(o =>
{
    o.ShortSchemaNames = true;
});

// add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
builder.Services.Configure<ServiceConfig>(config =>
{
    config.Services = new List<ServiceDescriptor>(builder.Services);

    // optional - default path to view services is /listallservices - recommended to choose your own path
    config.Path = "/listservices";
});

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    //containerBuilder.RegisterModule(new DefaultCoreModule());
    containerBuilder.RegisterModule(new AutofacInfrastructureModule());
});

//builder.Services.AddAuthentication(IISDefaults.AuthenticationScheme);

// Add services to the container.
//builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseShowAllServicesMiddleware(); // see https://github.com/ardalis/AspNetCoreStartupServices
    //app.UseSwagger();
    //app.UseSwaggerUI();
}
else
{
    app.UseDefaultExceptionHandler(); // from FastEndpoints
    app.UseHsts();
}

app.UseCors("CorsPolicy");
app.UseFastEndpoints();
app.UseSwaggerGen(); // FastEndpoints middleware
app.UseHttpsRedirection();

// Seed Database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        //                    context.Database.Migrate();
        context.Database.EnsureCreated();
        SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
    }
}

app.Run();
