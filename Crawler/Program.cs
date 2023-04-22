using Crawler.Application.Repository;
using Crawler.Application.Repository.IRepository;
using Crawler.Persistence;
using Crawler.Persistence.Context;
using Crawler.Persistence.Processos;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers().AddNewtonsoftJson(
    x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var connection = builder.Configuration["ConnectionStrings:DefaultConnection"];
//builder.Services.AddDbContext<CrawlerContext>(opt => opt.UseSqlite(connection));
builder.Services.AddDbContext<CrawlerContext>(opt => opt.UseSqlite(
    builder.Configuration.GetConnectionString("DefaultConnection")
    , b => b.MigrationsAssembly("Crawler")
    ));

builder.Services.AddScoped<IProcesso, Processo>();
builder.Services.AddScoped<IProcessosPersist, ProcessosPersist>();
builder.Services.AddScoped<IGeralPersist, GeralPersist>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("EnableCORS");
app.UseAuthorization();

app.MapControllers();

app.Run();
