using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TechStore.DAL.Data;

public static class Registrator
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TechStoreDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("SqlServer")));
    }
}
