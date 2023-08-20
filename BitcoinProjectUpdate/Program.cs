using BitcoinProject.Configuration;
using BitcoinProject.Controller;
using BitcoinProject.Interfaces;
using BitcoinProject.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.ServiceProcess;

namespace BitcoinProjectUpdate
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new BitcoinUpdateDatabase()
            };
            ServiceBase.Run(ServicesToRun);
        }

        //private static IConfigurationRoot _InitConfiguration()
        //{
        //    if (!File.Exists("appsettings.json"))
        //    {
        //        Console.WriteLine($"Arquivo de Configuração \"appsettings.json\" não localizado!");
        //        return null;
        //    }

        //    var configuration = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //        .Build();

        //    return configuration;
        //}
    }
}
