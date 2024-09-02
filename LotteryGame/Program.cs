using LotteryGame.Interfaces;
using LotteryGame.Models;
using LotteryGame.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace LotteryGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Build configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Bind configuration to LotterySettings class
            var lotterySettings = new LotterySettings();
            configuration.GetSection("LotterySettings").Bind(lotterySettings);

            // Set up DI container
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, lotterySettings);

            // Build the service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Resolve the LotteryManager service and run the lottery game
            var lotteryManager = serviceProvider.GetRequiredService<ILotteryManager>();
            lotteryManager.RunLottery();
        }

        private static void ConfigureServices(IServiceCollection services, LotterySettings lotterySettings)
        {
            // Register LotterySettings as a singleton
            services.AddSingleton(lotterySettings);

            // Register services
            services.AddSingleton<IPrizeDistributionService, PrizeDistributionService>();
            services.AddSingleton<IUserInputService, UserInputService>();
            services.AddSingleton<IPlayerService, PlayerService>();
            services.AddSingleton<IPlayerGenerationService, PlayerGenerationService>();
            services.AddSingleton<ICPUTicketService, CPUTicketService>();
            services.AddSingleton<ILotteryResultDisplayService, LotteryResultDisplayService>();
            services.AddSingleton<ILotteryManager, LotteryManager>();
        }
    }
}

