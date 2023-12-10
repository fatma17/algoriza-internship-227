using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Dtos;
using Vezeeta.Core.Dtos.Response;
using Vezeeta.Core.Models;

namespace Vezeeta.Core.Repository
{
    public interface IDoctorRepository : IBaseRepository<Doctor>
    {
        //DoctorDto GetDoctorById(int id);

        // IEnumerable<DoctorDto> GetAllDoctorsAdmin(int page, int pagesize);

        //IEnumerable<DoctorDto> GetAllDoctorsAdmin(int page, int pagesize, string search);

        //IEnumerable<DoctorWithAppointmentDto> GetAllDoctorsPatient(int page, int pagesize, string search);
    }
}
