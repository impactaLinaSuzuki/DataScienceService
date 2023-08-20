using BitcoinProject.Interfaces;
using BitcoinProjectConsole;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

public class Program
{
    private static void Main(string[] args)
    {
        IConfigurationRoot configuration = _InitConfiguration();
        if (configuration == null) return;

        var serviceCollection = new ServiceCollection();
        serviceCollection.ConfigureBitcoinProject(configuration);
        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

        var bitcoinProject = serviceProvider.GetService<IBitcoinProjectService>();

        bitcoinProject.Execute();
    }

    private static IConfigurationRoot _InitConfiguration()
    {
        if (!File.Exists("appsettings.json"))
        {
            Console.WriteLine($"Arquivo de Configuração \"appsettings.json\" não localizado!");
            return null;
        }
        
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        return configuration;
    }
}