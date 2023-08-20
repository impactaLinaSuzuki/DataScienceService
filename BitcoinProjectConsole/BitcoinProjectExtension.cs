using BitcoinProject.Configuration;
using BitcoinProject.Controller;
using BitcoinProject.Interfaces;
using BitcoinProject.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BitcoinProjectConsole
{
    public static class BitcoinProjectExtension
    {
        public static void ConfigureBitcoinProject(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.Configure<BitcoinOptions>(configuration.GetSection("BitcoinManagerApi"));
            services.Configure<DatabaseOptions>(configuration.GetSection("Database"));
            services.AddOptions();

            services.AddSingleton<BitcoinOptions>();
            services.AddSingleton<IBitcoinProjectService, BitcoinProjectService>();
            services.AddSingleton<IConnectDatabaseService, ConnectDatabaseService>();
            services.AddSingleton<IQueryBitcoinDataService, QueryBitcoinDataService>();
            services.AddSingleton<IBitcoinOptions>(sp => sp.GetRequiredService<IOptions<BitcoinOptions>>().Value);
            services.AddSingleton<IDatabaseOptions>(sp => sp.GetRequiredService<IOptions<DatabaseOptions>>().Value);

        }
    }
}
