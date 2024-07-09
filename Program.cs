using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using User_Registartion.Context;
using User_Registartion.Repository.Implementation;
using User_Registartion.Repository.Interface;
using User_Registartion.Service.Implementation;
using User_Registartion.Service.Interface;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<UserDBContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        builder.Services.AddScoped<IIdentityService, IdentityService>();
        builder.Services.AddScoped<IOrganisationService, OrganisationService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IOrganisationRepository, OrganisationRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddControllers();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, //Validate user that generate token
                    ValidateAudience = true, //Validate the recipient of the token is authorize to receive
                    ValidateLifetime = true, //Check if token has not expiry and if sign in key is valid
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    //Validate the server (ValidateIssuer = true) that generates the token.
                    //Validate the recipient of the token is authorized to receive (ValidateAudience = true)
                    //Check if the token is not expired and the signing key of the issuer is valid(ValidateLifetime = true)
                    //Validate signature of the token (ValidateIssuerSigningKey = true)
                    //Additionally, we specify the values for the issuer, audience, signing key. In this example, I have stored these values in appsettings.json file.
                    //Note: the signing key validate the issuer of the token
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = (context) =>
                    {
                        return Task.CompletedTask;
                    },
                    OnForbidden = (context) =>
                    {
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = (context) =>
                    {
                        return Task.CompletedTask;
                    }
                };
            });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Assignment", Version = "v1" });

            //configuring swagger to include security definitions
            o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Enter a valid Token",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            //Configure swagger to use JWT bearer token authentication
            o.AddSecurityRequirement(
                new OpenApiSecurityRequirement
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

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
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
    }
}