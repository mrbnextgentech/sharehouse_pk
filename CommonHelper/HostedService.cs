using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

public class HostedService : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {        

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {        
        return Task.CompletedTask;
    }
}
