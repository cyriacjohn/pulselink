using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMS.Application.Interfaces
{
    public interface INotificationService
    {
        Task NotifyDonationUpdated(int donationId, string message);
    }
}
