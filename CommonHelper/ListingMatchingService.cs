using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Gharshare.Models;
using Microsoft.Extensions.Logging;

public class ListingMatchingService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Your background service logic here.
            Console.Write("ListingMatchingService is running");
            await Task.Delay(TimeSpan.FromHours(6), stoppingToken);
        }
    }
}
