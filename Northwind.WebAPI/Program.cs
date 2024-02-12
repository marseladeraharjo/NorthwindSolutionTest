using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Northwind.Persistence;
using Northwind.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//register dbcontext to services
//builder.Services.AddDbContext<RepositoryDbContext>(opts =>
//{
//    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ShopeeConnection"]);
//});

builder.Services.AddCors();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// add custome
app.UseStaticFiles();

// set folder resources to static file
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});

app.UseAuthorization();

app.MapControllers();

app.Run();
