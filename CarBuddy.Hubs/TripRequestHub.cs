using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace CarBuddy.Hubs
{
    public interface ITripRequestClient
    {
        Task SendTripRequest(Guid driverId);
    }

    public class TripRequestHub : Hub<ITripRequestClient>
    { }
}
