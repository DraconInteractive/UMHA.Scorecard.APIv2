using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ScorecardAPI.Models;
using ScorecardAPI.Models.DTO;
using ScorecardFE;

namespace ScorecardFE
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://umha.api.dracon.au") });
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5000") });

            builder.Services.AddScoped<DataTransferService>();
            await builder.Build().RunAsync();
        }

        public class DataTransferService
        {
            public User? targetUser { get; set; }
            public User[]? Users { get; set; }
            public TournamentOutputDTO[]? Tournaments { get; set; }
        }
    }
}