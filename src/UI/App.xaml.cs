﻿using System;
using System.IO;
using System.Windows;
using Application;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UI.ViewModels;
using UI.Views;

namespace UI
{
    public partial class App
    {
        private IHost AppHost { get; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                {
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<HomePage>();
                    services.AddSingleton<ConvertorPage>();
                    services.AddTransient<CoinInformationPage>();
                    services.AddTransient<HistoryPage>();
                    services.AddTransient<QuestionPage>();
                    
                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<HomeViewModel>();
                    services.AddTransient<CoinInformationViewModel>();
                    
                    services.AddApplication();
                    services.AddInfrastructure();
                })
                .ConfigureAppConfiguration(builder =>
                {
                    builder
                        .SetBasePath(Directory.GetCurrentDirectory() + "/../../../")
                        .AddJsonFile("appsettings.json");
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost.StartAsync();
                
            var startupWindow = AppHost.Services.GetRequiredService<MainWindow>();
            startupWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost.StopAsync();
            base.OnExit(e);
        }
    }
}
