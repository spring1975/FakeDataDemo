using System;
using System.IO;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PoultryCloningFactory.Fakes;
using PoultryPopulation.Seeder.Services;

namespace PoultryPopulation.Seeder
{
    public class Program
    {
        private static ILogger<Program> _logger;
        private static IServiceCollection _serviceCollection;

        public static void Main(string[] args)
        {
            ConfigureServices();
            ConfigureLogger();

            var app = new CommandLineApplication
            {
                Name = "PoultryPopulation Persistence Seeder",
                Description = ".NET Core console app to seed the database."
            };

            app.HelpOption("-?|-h|--help");

            CommandOption seedFakeDataOption = app.Option("-s|--seed-fake-data",
                "Run the fake data seeder process",
                CommandOptionType.NoValue);

            app.OnExecute(() =>
            {
                if (NoOptionsOrArguments(app) || seedFakeDataOption.HasValue())
                {
                    _logger.LogInformation("Fake Data Seeder option was selected");

                    using ServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();

                    serviceProvider
                        .GetService<DatabaseResetService>()
                        .ResetDb();

                    EntityCollections fakeData = serviceProvider
                        .GetService<EntityFakerService>()
                        .Generate();

                    serviceProvider
                        .GetService<DataSeeder>()
                        .Seed(fakeData);

                    _logger.LogInformation("Finished!");
                }
            });
            app.Execute(args);
        }

        private static void ConfigureServices()
        {
            _serviceCollection = new ServiceCollection()
                    .AddLogging(configure => configure.AddConsole())
                    .AddSingleton(GetLocalConfiguration())
                    .AddSingleton<DatabaseResetService, DatabaseResetService>()
                    .AddSingleton<DataSeeder, DataSeeder>()
                    .RegisterLookupEntityFakers()
                    .RegisterEntityFakers()
                ;
        }

        private static void ConfigureLogger()
        {
            using ServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
            _logger = serviceProvider
                .GetService<ILoggerFactory>()
                .CreateLogger<Program>();
        }

    

    private static bool NoOptionsOrArguments(CommandLineApplication app)
        {
            return !app.Options.Any(o => o.HasValue()) && !app.Arguments.Any(a => a.Values.Any());
        }

        private static IConfiguration GetLocalConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../netcoreapp3.1"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }

    public static class SeederServiceCollectionExtensions
    {
        public static IServiceCollection RegisterLookupEntityFakers(this IServiceCollection services)
        {
            // TODO: Look into making these numbers relative to the number of Chickens asked to generate.
            const int NumberOfOwners = 5;
            const int NumberOfBreeds = 50;
            const int NumberOfCoops = 10;
            
            services
                .AddSingleton(sp =>
                    new OwnerFaker(NumberOfOwners, sp.GetService<AddressFaker>()))
                .AddSingleton(sp =>
                    new ChickenBreedFaker(NumberOfBreeds))
                .AddSingleton(sp =>
                    new ChickenCoopFaker(NumberOfCoops, sp.GetService<OwnerFaker>()))
                ;
            return services;
        }

        public static IServiceCollection RegisterEntityFakers(this IServiceCollection services) =>

            services
                .AddSingleton<EntityFakes>()
                .AddSingleton<EntityFakerService>()
                .AddSingleton<AddressFaker>()
                .AddSingleton<ChickenFaker>()
        ;


    }
}
