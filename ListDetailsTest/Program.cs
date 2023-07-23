using ListDetailsTest.Model;
using Microsoft.EntityFrameworkCore;

namespace ListDetailsTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages();


            builder.Services.AddDbContext<ApplicationContext>(options => {
                options.UseInMemoryDatabase(databaseName:"ListDetailsDatabase");
            });

            var app = builder.Build();

            app.UseStaticFiles();
            app.MapRazorPages();

            app.Run();
        }
    }
}