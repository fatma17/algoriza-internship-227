using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Dtos;
using Vezeeta.Core;
using Vezeeta.Core.Models;
using Vezeeta.Core.Repository;
using Vezeeta.Core.Dtos.Response;
using System.Collections;

namespace vezeeta.Repository
{
    public class ApplicationUserRepository: BaseRepository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }



        //public IEnumerable<PatientDto> GetAllPatients(int page, int pagesize)
        //{
        //    var result = _context.ApplicationUser.Where(u => u.AccountType == "Patient")
        //        .Select(p => new PatientDto
        //        {
        //            Image = p.Image,
        //            Name = p.UserName,
        //            email = p.Email,
        //            phone = p.PhoneNumber,
        //            gender = p.Gender,
        //            dateOfBirth = p.DateOfBirth.ToString()
        //        })
        //        .Skip((page - 1) * pagesize).Take(pagesize);

        //    return result;
        //}

        //public IEnumerable<PatientDto> GetAllPatients(int page, int pagesize, string search)
        //{
        //    var result = _context.ApplicationUser.Where(u => u.AccountType == "Patient")
        //        .Select(p => new PatientDto
        //        {
        //            Image = p.Image,
        //            Name = p.UserName,
        //            email = p.Email,
        //            phone = p.PhoneNumber,
        //            gender = p.Gender,
        //            dateOfBirth = p.DateOfBirth.ToString()
        //        })
        //        .Where(d => d.Name.Contains(search) || d.email.Contains(search) || d.phone.Contains(search))
        //        .Skip((page - 1) * pagesize).Take(pagesize);

        //    return result;
        //}
         
        //public PatientWithBookingDto GetPateintById(int id)
        //{
        //    var result = _context.ApplicationUser.Where(u => u.Id == id && u.AccountType == "Patient")
        //        .Select(p => new PatientWithBookingDto
        //        {
        //            Image = p.Image,
        //            Name = p.UserName,
        //            email = p.Email,
        //            phone = p.PhoneNumber,
        //            Bookings = p.Booking.Select(b => new BookingOfPatientDto
        //            {
        //                Image = b.Doctor.User.Image,
        //                DoctorName = b.Doctor.User.UserName,
        //                specialize = b.Doctor.Specialization.Name,
        //                Day = b.Times.Appointment.Day,
        //                time = b.Times.Time,
        //                price = b.PriceBefore,
        //                discoundcode = b.DiscoundCode,
        //                finalprice = b.FinalPrice,
        //                status = b.status
        //            })
        //        }).SingleOrDefault();
        //    return result;
        //}

    }
}
