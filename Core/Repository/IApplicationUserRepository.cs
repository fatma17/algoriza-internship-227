using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Dtos.Response;
using Vezeeta.Core.Models;

namespace Vezeeta.Core.Repository
{
    public interface IApplicationUserRepository : IBaseRepository<ApplicationUser>
    {
        //PatientWithBookingDto GetPateintById(int id);
       // IEnumerable<PatientDto> GetAllPatients(int page, int pagesize);
       // IEnumerable<PatientDto> GetAllPatients(int page, int pagesize, string search);
    }
}
