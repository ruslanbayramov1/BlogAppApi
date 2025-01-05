using BlogApp.BL.DTOs.Options;
using BlogApp.BL.Exceptions;
using BlogApp.BL.ExternalServices.Implements;
using BlogApp.BL.ExternalServices.Interfaces;
using BlogApp.BL.Services.Implements;
using BlogApp.BL.Services.Interfaces;
using BlogApp.Core.Enums;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.BL;

public static class ServiceRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }

    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceRegistration));

        return services;
    }

    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining(typeof(ServiceRegistration));

        return services;
    }

    public static IApplicationBuilder UseBlogExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(handler =>
        {
            handler.Run(async context =>
            {
                var feature = context.Features.Get<IExceptionHandlerFeature>();
                Exception ex = feature!.Error;
                if (ex is IBaseException ibe)
                {
                    await context.Response.WriteAsJsonAsync(new
                    {
                        StatusCode = ibe.StatusCode,
                        Message = ibe.ErrorMessage
                    });
                }
                else
                {
                    await context.Response.WriteAsJsonAsync(new
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = ex.Message
                    });
                }
            });
        });

        return app;
    }

    public static IServiceCollection AddEmailOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailOptions>(configuration.GetSection(EmailOptions.location));
        return services;
    }

    public static IServiceCollection AddCacheServices(this IServiceCollection service, IConfiguration _conf, CacheTypes type = CacheTypes.Redis)
    {
        if (type == CacheTypes.Redis)
        {
            var redisConnectionString = _conf.GetConnectionString("Redis");
            if (string.IsNullOrEmpty(redisConnectionString))
            {
                throw new InvalidOperationException("Redis connection string is not configured.");
            }

            service.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
                options.InstanceName = "Blog_";
            });

            service.AddScoped<ICacheService, RedisService>();
        }
        else
        {
            service.AddMemoryCache();
            service.AddScoped<ICacheService, LocalCacheService>();
        }

        return service;
    }
}
