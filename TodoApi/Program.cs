using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using MTodo.Infrastruture;
using PTodo.Application.Implementation;
using PTodo.Application.Interface;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<RDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));
builder.Services.AddScoped<IService, Service>();
builder.Services.AddIdentity<AppTodoItem, IdentityRole>().AddEntityFrameworkStores<RDbContext>();
builder.Services.AddScoped<IAddMyTodo, AddMyTodo>();

//  start
//builder.Services.AddAuthentication(options =>
//{ 
//options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme
//options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme
//options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme
//}) .AddJwtBearer(o =>
//{
// o.TokenValidationParameters = new TokenValidationParameters
//{
//   ValidIssuer = builder.Configuration["Jwt:Issuer],
 //  ValidAudience = builder.Configuration[Jwt:Audience],
 //  IssuerSigninKey = new SymmetricSecurityKey
 //  (Encoding.UTF8.GetBytes(builder.Configuration["Jwt: Key"])),
 //  ValidateIssuer = true,
 //  ValidateAudience true,
 //  ValidateLifeTime = false,
 //  ValidateIssuerSigningKey = true
//}
//}
//);
//builder.Services.AddAuthorization();// stop


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
//start
//app.UseHttpsRedirection();
//app.MapGet("/security/getMessage", () => "Hello World!").RequireAuthorization();
//app.MapPost("/security/createToken",
//[AllowAnonymous] (User user) =>
//{
// if(user.UserName == "joydip" && user.Password == "joydip123")
//{
//  var issuer = builder.Configuration["Jwt:Issuer"];
//  var audience = builder.Configuration["Jwt : Audience"];
//  var key = Encoding.ASCII.GetBytes
//  (builder.Configuration["Jwt:Key"]);
//  var tokenDescriptor = new SecurityTokenDescriptor
//{
//  Subject = new ClaimsIdentity(new[]
//   {
//      new Claim("Id", Guid.NewGuid().ToString()),
//      new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
//      new Claim(JwtRegisteredClaimNames.Email, user.UserName),
//      new Claim(JwtRegisteredClaimNames.Jti,
//      Guid.NewGuid().ToString())
//      }),
//      Expires = DateTime.UtcNow.AddMinutes(5),
//      Issuer = issuer,
//      Audience = audience,
//      SigningCredentials = new SigningCredentials
//      (new SymmetricSecurityKey(key)),
//      SecurityAlgorithms.HmacSha512Signature)
//      };
//      var tokenHandler = new JwtSecurityTokenHandler();
//      var token = tokenHandler.CreateToken(tokenDescriptor);
//      var jwtToken = tokenHandler.WriteToken(token);
//      var stringToken = tokenHandler.WriteToken(token);
//      return Result.Ok(stringToken);
//    }
//       return Results.Unauthorized();
//   });
// end

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//app.UseAuthentication();
//app.UseAuthorization();

app.Run();
