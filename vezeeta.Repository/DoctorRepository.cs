using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Dtos;
using Vezeeta.Core.Dtos.Request;
using Vezeeta.Core.Dtos.Response;
using Vezeeta.Core.Models;
using Vezeeta.Core.Repository;

namespace vezeeta.Repository
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        private readonly ApplicationDbContext _context;
        public DoctorRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        //public IEnumerable<DoctorDto> GetAllDoctorsAdmin(int page, int pagesize)
        //{
        //    var result = _context.Doctors.Select(d => new DoctorDto
        //    {
        //        Image = d.User.Image,
        //        Name = d.User.UserName,
        //        email = d.User.Email,
        //        phone = d.User.PhoneNumber,
        //        gender = d.User.Gender,
        //        speclization = d.Specialization.Name
        //    })
        //       .Skip((page - 1) * pagesize).Take(pagesize);

        //    return result;
        //}


        //public IEnumerable<DoctorDto> GetAllDoctorsAdmin(int page, int pagesize, string search)
        //{
        //    var result = _context.Doctors.Select(d => new DoctorDto
        //    {
        //        Image = d.User.Image,
        //        Name = d.User.UserName,
        //        email = d.User.Email,
        //        phone = d.User.PhoneNumber,
        //        gender = d.User.Gender,
        //        speclization = d.Specialization.Name
        //    })
        //       .Where(d => d.Name.Contains(search) || d.email.Contains(search) || d.phone.Contains(search) || d.speclization.Contains(search))
        //       .Skip((page - 1) * pagesize).Take(pagesize);

        //    return result;
        //}

        //public DoctorDto GetDoctorById(int id)
        //{
        //    var result = _context.Doctors.Where(u => u.Id == id)
        //        .Select(d => new DoctorDto
        //        {
        //            Image = d.User.Image,
        //            Name = d.User.UserName,
        //            email = d.User.Email,
        //            phone = d.User.PhoneNumber,
        //            speclization = d.Specialization.Name,
        //            gender = d.User.Gender

        //        }).ToList().SingleOrDefault();
        //    return result;
        //}

        //public IEnumerable<DoctorWithAppointmentDto> GetAllDoctorsPatient(int page, int pagesize, string search)
        //{
        //    var result = _context.Doctors.Select(d => new DoctorWithAppointmentDto
        //    {
        //        Image = d.User.Image,
        //        Name = d.User.UserName,
        //        email = d.User.Email,
        //        phone = d.User.PhoneNumber,
        //        gender = d.User.Gender,
        //        speclization = d.Specialization.Name,
        //        price = d.Price,
        //        appointment = d.Appointment.Select(p => new AppointmentDto
        //        {
        //            Day = p.Day,
        //            Times = p.Times.Select(t => t.Time)
        //        })
        //    })
        //        .Where(d => d.Name.Contains(search) || d.email.Contains(search) || d.phone.Contains(search) || d.speclization.Contains(search))
        //        .Skip((page - 1) * pagesize).Take(pagesize);

        //    return result;
        //}

    }
}
