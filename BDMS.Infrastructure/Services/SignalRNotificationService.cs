using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDMS.Application.Interfaces;
using BDMS.Infrastructure.RealTime;
using Microsoft.AspNetCore.SignalR;

namespace BDMS.Infrastructure.Services
{
    public class SignalRNotificationService : INotificationService
    {
        // Use the Microsoft.AspNetCore.SignalR.Hub type explicitly to satisfy the generic constraint.
        private readonly IHubContext<Microsoft.AspNetCore.SignalR.Hub> _hub;

        public SignalRNotificationService(IHubContext<Microsoft.AspNetCore.SignalR.Hub> hub)
        { 
            _hub = hub;
        }

        public async Task NotifyDonationUpdated(int donationId, string message)
        {
            await _hub.Clients.All.SendAsync("DonationUpdated", new
            {
                DonationId = donationId,
                Message = message
            });
        }
    }
}
