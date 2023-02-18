using Microsoft.EntityFrameworkCore;
using ModelKullanimi.DbOperations;
using System.Reflection;

namespace ModelKullanimi.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddDbContext<BookStoreDbContext>(options
                => options.UseInMemoryDatabase(databaseName: "BookStoreDb"));


            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
