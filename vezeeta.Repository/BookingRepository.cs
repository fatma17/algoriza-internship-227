using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Dtos.Response;
using Vezeeta.Core.Models;
using Vezeeta.Core.Repository;

namespace vezeeta.Repository
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _context;
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<NumRequestDto> NumOfRequests()
        {
            var result = _context.Bookings.GroupBy(g => g.status).Select(g => new NumRequestDto{
                                                                        _status = g.Key, 
                                                                        count = g.Count() });
            return result;
        }
        public IEnumerable<TopDoctorDto> GetTopDoctor(int Take) 
        {
            var topDoctors = _context.Bookings.GroupBy(b => b.DoctorId)
                .Select(g => new
                {
                    DoctorId = g.Key,
                    CountRequest = g.Count()
                })
                .OrderByDescending(d => d.CountRequest)
                .Take(Take)
                .Join(_context.Doctors,bookings => bookings.DoctorId,doctors => doctors.Id,
                     (bookings, doctors) => new TopDoctorDto
                     {
                        DoctorImage = doctors.User.Image,
                        DoctorName = doctors.User.UserName,
                        DoctorSpecializa = doctors.Specialization.Name,
                        NumRequest = bookings.CountRequest
                     })
                .ToList();
            return topDoctors; 
        }
        public IEnumerable<TopSpecilizationDto> GetTopSpecilization(int Take)
        {
            var topDoctors = _context.Bookings.GroupBy(b => b.DoctorId)
                .Select(g => new
                {
                    DoctorId = g.Key,
                    CountRequest = g.Count()
                })
                .OrderByDescending(d => d.CountRequest)
                .Take(Take)
                .Join(_context.Doctors,bookings => bookings.DoctorId,doctors => doctors.Id,
                    (bookings, doctors) => new TopSpecilizationDto
                    {
                        Name = doctors.Specialization.Name,
                        count = bookings.CountRequest
                    })
                .ToList();
            return topDoctors;
        }


        //public IEnumerable<BookingOfPatientDto> GetPatientBooking(int patientid)
        //{
        //    var BookingOfPatient = _context.Bookings.Where(b=>b.PatientId == patientid)
        //        .Select(b => new BookingOfPatientDto
        //        {
        //            Image =b.Doctor.User.Image,
        //            DoctorName = b.Doctor.User.UserName,
        //            specialize = b.Doctor.Specialization.Name,
        //            Day = b.Times.Appointment.Day,
        //            time = b.Times.Time,
        //            price = b.PriceBefore,
        //            discoundcode = b.DiscoundCode,
        //            finalprice = b.FinalPrice,
        //            status = b.status

        //        }).ToList();
        //    return BookingOfPatient;
        //}
        //public IEnumerable<BookingDoctorDto> GetAllBookingDoctor(int id, int page, int pagesize, string search)
        //{
        //    var result = _context.Bookings.Where(u => u.DoctorId == id)
        //        .Select(p => new BookingDoctorDto
        //        {
        //            Image = p.Patient.Image,
        //            Name = p.Patient.UserName,
        //            email = p.Patient.Email,
        //            phone = p.Patient.PhoneNumber,
        //            dateOfBirth = p.Patient.DateOfBirth.ToString(),
        //            Time = p.Times.Time.ToString(),
        //            Day = p.Times.Appointment.Day
        //        }).Where(d => d.Time.Contains(search) || d.Day.ToString().Contains(search))
        //        .Skip((page - 1) * pagesize).Take(pagesize);

        //    return result;
        //}


    }
}
