
using BlogApp.BL;
using BlogApp.DAL;
using BlogApp.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace BlogApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(builder.Configuration);

            builder.Services.AddDbContext<BlogDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("MSSql-Remote"));
            });

            builder.Services.AddJwtAuthentication(builder.Configuration);
            builder.Services.AddJwtOptions(builder.Configuration);
            builder.Services.AddAutoMapper();
            builder.Services.AddFluentValidation();
            builder.Services.AddRepositories();
            builder.Services.AddServices();

            var app = builder.Build();

            app.UseBlogExceptionHandler();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(opt =>
                {
                    opt.EnablePersistAuthorization();
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
