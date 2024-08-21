using Linkdout.Dal;
using Linkdout.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
    //*
string cs = "server=DESKTOP-66BDVEB\\SQLEXPRESS;" +
                                " initial catalog = linkdout ; user id=sa ;" +
                                " password=1234 ; TrustServerCertiFicate = Yes ";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//*
builder.Services.AddDbContext<DataLayer>(options => options.UseSqlServer(cs));
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<JwtService>();
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // מוודא שהטוקן בתוקף
             ValidateLifetime = true,
             
             ValidateIssuer = false,
            ValidateAudience = false,
            // תקין Key מוודא ש
            ValidateIssuerSigningKey = true,
            //התקין כדי שנוכל לבדוק  Key מספק את ה
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"]!))

        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
