using EventForum.Shared.Faker;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EventForum.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            var baseAddress = builder.HostEnvironment.BaseAddress;
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });

            builder.Services.AddSingleton<KommentarFaker>();

            builder.Services.AddSingleton<HubConnection>(sp =>
            {
                var navigationManager = sp.GetRequiredService<NavigationManager>();

                return new HubConnectionBuilder()
                    //.WithUrl($"{baseAddress}/beitragHub")
                    .WithUrl(navigationManager.ToAbsoluteUri("/beitragHub"))
                    .WithAutomaticReconnect()
                    .Build();
            });

            await builder.Build().RunAsync();
        }
    }
}
