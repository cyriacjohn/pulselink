using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMS.Application.Interfaces
{
    public interface IGoogleMapsService
    {
        Task<double> GetDistanceInKm(double originLat, double originLng, double destLat, double destLng);
    }
}
