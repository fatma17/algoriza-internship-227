using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models;
using Vezeeta.Core.Repository;

namespace Vezeeta.Core
{
    public interface IUnitOfWork
    {
        IApplicationUserRepository ApplicationUser { get;}
        IDoctorRepository Doctors { get; }
        IBookingRepository  Booking { get; }
        IBaseRepository<Appointment> Appointments { get; }
        ITimesRepository Times { get; }
        IBaseRepository<Coupon> Coupons { get; }
        IUserAuthenticationRepository UserAuthentication { get; }
        int Save();
    }
}
