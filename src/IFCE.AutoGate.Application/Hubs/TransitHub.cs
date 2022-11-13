using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace IFCE.AutoGate.Application.Hubs;

public class TransitHub : Hub
{
    private readonly ILogger _logger;

    public TransitHub(ILogger<TransitHub> logger)
    {
        _logger = logger;
    }

    public override Task OnConnectedAsync()
    {
        _logger.LogInformation("Cliente conectado => {ConnectionId}", Context.ConnectionId);

        return base.OnConnectedAsync();
    }
}
