using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MTodo.Infrastruture;
using PTodo.Application.Implementation;
using PTodo.Application.Interface;
using Swashbuckle.AspNetCore.Filters;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<RDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));
builder.Services.AddScoped<IService, Service>();
builder.Services.AddScoped<IAddMyTodo, AddMyTodo>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddIdentity<AppTodoItem, IdentityRole>().AddEntityFrameworkStores<RDbContext>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],

        ValidAudience = builder.Configuration["Jwt: Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true

    };
}
);
builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>{ options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
{
     Description = "Standard Authorization header using the Bearer scheme (\"bearer{token}\")",
     In = ParameterLocation.Header,
     Name = "Authorization",
     Type = SecuritySchemeType.ApiKey
});
    options.OperationFilter<SecurityRequirementsOperationFilter>();

});

var app = builder.Build();
app.UseHttpsRedirection();


//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
