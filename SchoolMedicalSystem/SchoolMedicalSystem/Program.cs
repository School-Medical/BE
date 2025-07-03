using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolMedicalSystem.Application.ExceptionHandler;
using SchoolMedicalSystem.Application.Interfaces;
using SchoolMedicalSystem.Application.Mappers;
using SchoolMedicalSystem.Application.Services;
using SchoolMedicalSystem.Infrastructure.Data;
using SchoolMedicalSystem.Infrastructure.Services;
using SchoolMedicalSystem.Models;
using System;
using System.Text;

namespace SchoolMedicalSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Add mapper as the DI (It will seek all asembly have in mapper)
            builder.Services.AddAutoMapper(typeof(MedicalIncidentProfile));
            builder.Services.AddAutoMapper(typeof(HealthProfileProfile));

            // Use Autofac as the DI container
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterModule(new ServiceRegistration(builder.Configuration));
            });

            // Add Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!);

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ClockSkew = TimeSpan.Zero 
                    };
                });
            builder.Services.AddAuthorization();

            #region Swagger
            //Add Swagger document with Bearer to Authentication and Authorization
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "school-medical-manager-api", Version = "v1", Description = "Api document for School Medical Project SWD392" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string []{}
                    }
                });
            });
            #endregion

            //Add Cors to FE can call api from BE SWD392
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                    policy.WithOrigins("http://localhost:3000") // Allow the frontend's origin SWD project. Please change it!!!
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials()); // If you're using credentials (cookies, Authorization headers, etc.)
            });

            #region Cloudinary
            //Add Cloudinary service
            var cloudinarySettings = new CloudinarySettings();
            builder.Configuration.GetSection("CloudinarySettings").Bind(cloudinarySettings);
            var cloudinary = new Cloudinary(new Account(
                cloudinarySettings.CloudName,
                cloudinarySettings.ApiKey,
                cloudinarySettings.ApiSecret
            ));
            // Register Cloudinary as a singleton service

            builder.Services.AddSingleton(cloudinary);
            builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
            #endregion

            var app = builder.Build();

            // Configure middleware handle global exception
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
