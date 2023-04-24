using Crawler.Application.Repository;
using Crawler.Application.Repository.IRepository;
using Crawler.Persistence;
using Crawler.Persistence.Context;
using Crawler.Persistence.Processos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers().AddNewtonsoftJson(
    x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    ).AddJsonOptions(opts =>
         opts.JsonSerializerOptions
             .ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ApiCrawler",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Vittor Melo",
            Email = "vittordemelo@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/vittor-melo-3258b313a/")
        }
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.\r\n\r\nEnter 'Bearer'[space] eo seu tojen.\r\n\r\n Example: \'Bearer 12345abcdef\'"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

//var connection = builder.Configuration["ConnectionStrings:DefaultConnection"];
//builder.Services.AddDbContext<CrawlerContext>(opt => opt.UseSqlite(connection));
builder.Services.AddDbContext<CrawlerContext>(opt => opt.UseSqlite(
    builder.Configuration.GetConnectionString("DefaultConnection")
    , b => b.MigrationsAssembly("Crawler")
    ));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<CrawlerContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IProcesso, Processo>();
builder.Services.AddScoped<IProcessosPersist, ProcessosPersist>();
builder.Services.AddScoped<IGeralPersist, GeralPersist>();

builder.Services.AddAuthentication
                (JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidAudience = builder.Configuration["TokenConfiguration:Audience"],
                        ValidIssuer = builder.Configuration["TokenConfiguration:Issuer"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
