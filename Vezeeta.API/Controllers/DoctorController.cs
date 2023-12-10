using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using vezeeta.Repository;
using Vezeeta.Core;
using Vezeeta.Core.Dtos;
using Vezeeta.Core.Dtos.Request;
using Vezeeta.Core.Dtos.Response;
using Vezeeta.Core.Models;

namespace Vezeeta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _mapper;

        public DoctorController( IUnitOfWork UnitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _UnitOfWork = UnitOfWork;
        }


        [HttpGet("GetAllBooking")] 
        public async Task<IActionResult> GetAllBooking(int id , [FromQuery] PaginationDto paginationDto)
        {
            if (String.IsNullOrEmpty(paginationDto.search))  
            {
                return Ok(await _UnitOfWork.Booking.GetAll<BookingDoctorDto>(b => b.DoctorId == id, paginationDto.page, paginationDto.pagesize, BookingDoctorDto.BookingDoctorSelector));
            }
            var result = await _UnitOfWork.Booking.GetAllSearch<BookingDoctorDto>(b => b.DoctorId == id, paginationDto.page, paginationDto.pagesize, BookingDoctorDto.BookingDoctorSelector, BookingDoctorDto.BookingDoctorSearch(paginationDto.search));

            return Ok(result);
        }


        [HttpPut("ConfirmCheckUpBooking")] //Done if id doctor== id 
        public async Task<IActionResult> ConfirmCheckUpBooking(int Bookingid )
        {
            var result = await _UnitOfWork.Booking.FindAsync(b => b.Id == Bookingid);

            if (result == null)
            {
                return NotFound("Bookingid isn't found");
            }
            if (result.status == Status.Cancelled)
            {
                return BadRequest("Bookingid is Cancelled");
            }
            if (result.status == Status.Completed)
            {
                return BadRequest("Bookingid is already Completed ");
            }
            result.status = Status.Completed;
            _UnitOfWork.Booking.Update(result);
            _UnitOfWork.Save();
            return Ok(new { Message = "Confirm Booking successful" });
        }



        #region Appointment
        [HttpPost("AddAppointment")] 
        public async Task<IActionResult> AddAppointment(int doctorId, int price,[FromForm] List<AppointmentDto> appointmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctor = await _UnitOfWork.Doctors.FindAsync(d => d.Id == doctorId);

            if (doctor == null)
            {
                return NotFound("Doctor isn't found");
            }

            if (doctor.Price != null)
            {
                return BadRequest("The price is already added , you can't change it  ");
            }

            //check مش نفس المواعيد 

            doctor.Price = price;
            var appointments = new List<Appointment>();
            foreach (var apppintment in appointmentDto)
            {
                appointments.Add( new Appointment
                {
                    DoctorId = doctorId,
                    Day = apppintment.Day,
                    Times = apppintment.Times.Select(T => new Times
                    {
                        Time = T
                    }).ToList()
                });
                //_appointments.Doctor= doctor;
            }
            await _UnitOfWork.Appointments.AddRangeAsync(appointments);
            _UnitOfWork.Save();

            return Ok("Appointments inserted successfully");
        }


        [HttpPut("update")] 
        public async Task<IActionResult> updateAppointment(int id ,TimeSpan NewTime )
        {
            var result =  await _UnitOfWork.Times.FindAsync(b => b.Id == id, new[] { "Booking" });
            if (result == null)
            {
                return BadRequest(new { Message = "There is no Appointment with that id " });
            }
            if (result.Booking != null && result.Booking.status != Status.Cancelled)
            {
                return BadRequest(new { Message = "This Appointment has booked , can't delete " });
            }

            _UnitOfWork.Times.Update(result);
            _UnitOfWork.Save();
            return Ok(new { Message = "Update time successful" });


        }


        [HttpDelete("DeleteAppointment")]     
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var result = await _UnitOfWork.Times.FindAsync(b => b.Id == id, new[] { "Booking" });
            if (result == null)
            {
                return BadRequest(new { Message = "There is no Appointment with that id " });
            }
            if (result.Booking != null)
            {
                return BadRequest(new { Message = "This Appointment has booked , can't delete " });
            }
            _UnitOfWork.Times.Delete(result);
            _UnitOfWork.Save();

            return Ok(new { Message = "Delete Appointment successful" });
        }

        #endregion
    }


}
