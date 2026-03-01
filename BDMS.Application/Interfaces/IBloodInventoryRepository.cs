using BDMS.Domain.Entities;
using BDMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMS.Application.Interfaces
{
    public interface IBloodInventoryRepository
    {
        Task<BloodInventory?> GetByHospitalAndBloodGroup(int hospitalId, BloodGroup bloodGroup);
        Task SaveChangesAsync();
    }
}
