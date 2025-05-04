using CSCI3110_Term_Project.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using CSCI3110_Term_Project.Models;

namespace CSCI3110_Term_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                ctx.Database.Migrate();  // ensure latest schema
                if (!ctx.Categories.Any())
                {
                    ctx.Categories.AddRange(
                        new Category { Name = "Rent", Type = "Expense" },
                        new Category { Name = "Food", Type = "Expense" },
                        new Category { Name = "Salary", Type = "Income" },
                        new Category { Name = "Entertainment", Type = "Expense" }
                    );
                    ctx.SaveChanges();
                }
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
