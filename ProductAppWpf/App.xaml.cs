using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ProductAppWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? app { get; private set; }
        public App()
        {
            app = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddScoped<MainWindow>();
                //services.AddDbContext<DataContext>(x => x.UseSqlServer(@"C:\USERS\PATRI\SKOLA\DATABASTEKNIK\DATABASTEKNINGASSIGNMENT1\PRODUCTAPP\CONTEXT\SQL_ASSIGNMENT_DB.MDF"));
                //services.AddScoped<CustomerService>();
                //services.AddScoped<ProductService>();
                //services.AddScoped<OrderService>();
            }).Build();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await app!.StartAsync();
            var MainWindow = app.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
