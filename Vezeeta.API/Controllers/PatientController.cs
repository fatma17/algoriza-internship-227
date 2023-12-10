using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using System;
using Vezeeta.Core;
using Vezeeta.Core.Models;
using Vezeeta.Serivce;
using Vezeeta.Core.Dtos.Request;
using vezeeta.Repository;
using Vezeeta.Core.Dtos.Response;

namespace Vezeeta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _mapper;

        public PatientController(IUnitOfWork UnitOfWork, IMapper mapper )
        {
            _mapper = mapper;
            _UnitOfWork = UnitOfWork;
        }


        [HttpGet("GetAllDoctor")]
        public async Task<IActionResult> GetAllDoctor([FromQuery] PaginationDto paginationDto)
        {
            if (String.IsNullOrEmpty(paginationDto.search))
            {
                return Ok(await _UnitOfWork.Doctors.GetAll<DoctorWithAppointmentDto>(paginationDto.page, paginationDto.pagesize, DoctorWithAppointmentDto.DoctorAppointmentSelector));
            }
            var result = _UnitOfWork.Doctors.GetAllSearch<DoctorWithAppointmentDto>(paginationDto.page, paginationDto.pagesize, DoctorWithAppointmentDto.DoctorAppointmentSelector, DoctorWithAppointmentDto.DoctorSearch(paginationDto.search));
            return Ok(result);
        }


        [HttpPost("AddBooking")] 
        public async Task<IActionResult> AddBooking( [FromForm]BookingDto bookingDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Add Booking failed", ModelState });
            }

            bool UseCoupon = false;
            var coupon = new Coupon();
            if (bookingDto.codecoupon!=null)
            {
                coupon = await _UnitOfWork.Coupons.FindAsync(c => c.DiscoundCode == bookingDto.codecoupon);
                if (coupon == null)
                {
                    return NotFound("DiscoundCode isn't found");
                }
                if (coupon.Deactivate == true)
                {
                    return BadRequest(new{ Message = "DiscoundCode is Deactivate" });
                }

                var NumCompletedBookings = await _UnitOfWork.Booking.CountAsync(b =>
                                                                  b.PatientId == bookingDto.patientid
                                                                  && b.status == Status.Completed);

                if (NumCompletedBookings < coupon.NumOfCompletedBookings)
                {
                    return BadRequest(new { Message = " Coupon Can't used ,The patient hasn't completed enough bookings" });
                }


                var isCouponUsed = _UnitOfWork.Booking.Any(b => 
                                                       b.PatientId == bookingDto.patientid 
                                                       && b.CouponId == coupon.Id);

                if (isCouponUsed)
                {
                    return BadRequest(new { Message = "The coupon is already used ,The coupon is used once " });
                }
                 
                UseCoupon = true;
            }

            var time = _UnitOfWork.Times.FindTimeWithPrice(bookingDto.timeid);

            if (time.timeid==null)
            {
                return NotFound(new { Message = "Time isn't found" });
            }

            var isTimeBooked = _UnitOfWork.Booking.Any(b =>
                                                            b.PatientId == bookingDto.patientid
                                                            && b.TimesId == bookingDto.timeid);
            if (isTimeBooked)
            {
                return BadRequest(new { Message = "The Time is Booked , please select another time " });
            }

            var booking = new Booking
                                    {
                                        status = Status.Pending,
                                        PriceBefore = time.price.Value,
                                        FinalPrice = time.price.Value,
                                        TimesId = bookingDto.timeid,
                                        PatientId = bookingDto.patientid,
                                        DoctorId = time.doctorid.Value,
                                    };

            if (UseCoupon)
            {
                var finalprice = CouponService.CalculationFinalPrice(time.price.Value, coupon.value, coupon.DiscoundType);
                booking.FinalPrice = finalprice;
                booking.DiscoundCode = bookingDto.codecoupon;
                booking.CouponId = coupon.Id;
            }

            _UnitOfWork.Booking.AddAsync(booking);
            _UnitOfWork.Save();

            return Ok(new { Message = "The Booking is successful" }); 
        }


        [HttpGet("GetAllBooking")] 
        public async Task<IActionResult> GetAllBooking(int id )
        {
            var result = await _UnitOfWork.Booking.GetAll(b => b.PatientId == id, BookingOfPatientDto.BookingOfPatientSelector);
            return Ok(result);
        }


        [HttpPut("CancelBooking")] 
        public async Task<IActionResult> CancelBooking(int bookingid )
        {
            var result= await _UnitOfWork.Booking.FindAsync(b => b.Id == bookingid);
            if(result == null)
            {
                return BadRequest(new { Message = "there is no booking with that id " });
            }
            if(result.status==Status.Completed)
            {
                return BadRequest(new { Message = "this booking is Completed , cant't cancel" });
            }
            if (result.status == Status.Cancelled)
            {
                return BadRequest(new { Message = "this booking is already cancel" });
            }
            result.status = Status.Cancelled;
            _UnitOfWork.Booking.Update(result);
            _UnitOfWork.Save();

            return Ok(new { Message = "Cancel Booking successful"});
        }

    }
}
