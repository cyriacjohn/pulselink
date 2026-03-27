using BDMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMS.Application.Interfaces
{
    public interface IBloodRequestRepository
    {
        Task<BloodRequest?> GetByIdAsync(int id);
    }
}
