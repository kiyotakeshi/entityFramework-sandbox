using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using entityFramework_sandbox;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using sandbox.models;

namespace sandbox
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      // CreateHostBuilder(args).Build().Run();
      var host = CreateHostBuilder(args).Build();
      using var scope = host.Services.CreateScope();
      var services = scope.ServiceProvider;

      try
      {
        var context = services.GetRequiredService<DataContext>();
        await context.Database.MigrateAsync();
        await Seed.SeedData(context);
      }
      catch (Exception e)
      {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(e, "migration error");
      }

      await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
