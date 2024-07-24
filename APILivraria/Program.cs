using APILivraria.Data;
using APILivraria.Integracao;
using APILivraria.Integracao.Interfaces;
using APILivraria.Integracao.Refit;
using APILivraria.Models;
using APILivraria.PrivateKey;
using APILivraria.Repositories;
using APILivraria.Repositories.Interfaces;
using APILivraria.Services;
using APILivraria.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Refit;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer Auth", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n Put **_ONLY_** your JWT Bearer token inthe text input below.\r\n\r\nExample: \"12345abcdef\""
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer Auth"
            }
        },
        Array.Empty<string>()
    } }
    );
});


builder.Services.AddScoped<IGeneroService, GeneroService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILivrariaService, LivrariaService>();
builder.Services.AddScoped<ILivrariaRepositorie, LivrariaRepositorie>();
builder.Services.AddScoped<IGenerosRepositories, GenerosRepositories>();
builder.Services.AddScoped<IViaCepRepository, ViaCepIntegracao>();
builder.Services.AddRefitClient<IViaCepIntegracaoRefit>().ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri("https://viacep.com.br");
});

builder.Services.AddTransient<IUserRepositorie, UserRepositorie>();

var key = Encoding.ASCII.GetBytes(Key.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    // Middleware
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

builder.Services.AddDbContext<LivrariaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ServerConnection")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opt =>
{
    opt.AllowAnyHeader()
       .AllowAnyMethod()
       .AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
