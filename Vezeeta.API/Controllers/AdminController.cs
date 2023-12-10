using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Numerics;
using System.Reflection;
using vezeeta.Repository;
using Vezeeta.Core;
using Vezeeta.Core.Dtos;
using Vezeeta.Core.Dtos.Request;
using Vezeeta.Core.Dtos.Response;
using Vezeeta.Core.Models;
using Vezeeta.Core.Repository;
using Vezeeta.Serivce;
using static System.Net.Mime.MediaTypeNames;

namespace Vezeeta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _mapper;
        public static IWebHostEnvironment _environment;
        public AdminController(IUnitOfWork UnitOfWork, IMapper mapper, IWebHostEnvironment environment)
        {
            _mapper = mapper;
            _UnitOfWork = UnitOfWork;
            _environment = environment;

        }

        #region statistics

        [HttpGet("GetNumOfDoctors")]//Done
        public async Task<IActionResult> GetNumOfDoctors()
        {
            return Ok(await _UnitOfWork.Doctors.CountAsync());
        }

        [HttpGet("GetNumOfPatient")]//Done
        public async Task<IActionResult> GetNumOfPatient()
        {
            return Ok(await _UnitOfWork.ApplicationUser.CountAsync(u => u.AccountType == "Patient"));
        }

        [HttpGet("GetNumOfRequests")] //Done
        public async Task<IActionResult> GetNumOfRequests()
        {
            return Ok(_UnitOfWork.Booking.NumOfRequests());
        }

        [HttpGet("GetTop10_Doctor")] //Done
        public async Task<IActionResult> GetTop10_Doctor()
        {
            return Ok(_UnitOfWork.Booking.GetTopDoctor(10));
        }

        [HttpGet("GetTop5_Specialization")]//Done
        public async Task<IActionResult> GetTop5_Specialization()
        {
            return Ok(_UnitOfWork.Booking.GetTopSpecilization(5));
        }

        #endregion

        #region doctor

        [HttpGet("GetAllDoctors")]
        public async Task<IActionResult> GetAllDoctors([FromQuery] PaginationDto paginationDto)
        {
            if (String.IsNullOrEmpty(paginationDto.search))
            {
                return Ok(await _UnitOfWork.Doctors.GetAll<DoctorDto>(paginationDto.page, paginationDto.pagesize, DoctorDto.DoctorSelector));
            }
            var result = await _UnitOfWork.Doctors.GetAllSearch(paginationDto.page, paginationDto.pagesize, DoctorDto.DoctorSelector, DoctorDto.DoctorSearch(paginationDto.search));

            return Ok(result);
        }

        [HttpGet("GetDoctorById")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var result = _UnitOfWork.Doctors.GetById<DoctorDto>(d => d.Id == id, DoctorDto.DoctorSelector);
            if (result == null)
            {
                return BadRequest(new { Message = "there is no Doctor with that id " });
            }

            string imageName = result.Image;
            string imagePath = Path.Combine("wwwroot", "Images", imageName);

            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound("Image not found");
            }

            string imageUrl = Url.Content($"~/images/{imageName}");

            //byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
            //result.Image = Convert.ToBase64String(imageBytes);
            result.Image = imageUrl;

            return Ok(result);
        }

        [HttpPost("AddDoctor")]
        public async Task<IActionResult> AddDoctor([FromForm] DoctorRegistrationDto DoctorDto)
        {
            if (ModelState.IsValid)
            {

                var uniquefilaname = Images.Add(DoctorDto.image);

                var user = new ApplicationUser
                {
                    Image = DoctorDto.image.FileName,
                    FirstName = DoctorDto.FirstName,
                    LastName = DoctorDto.LastName,
                    Email = DoctorDto.Email,
                    PhoneNumber = DoctorDto.PhoneNumber,
                    Gender = DoctorDto.Gender,
                    DateOfBirth = DoctorDto.DateOfBirth
                };
                user.Image = uniquefilaname;
                user.AccountType = "Doctor";
                user.UserName = DoctorDto.FirstName + DoctorDto.LastName;
                Doctor doctor = new Doctor { SpecializationId = DoctorDto.SpecializationId };
                user.Doctor = doctor;

                var result = await _UnitOfWork.UserAuthentication.RegisterUserAsyuc(user, DoctorDto.Password);

                if (result.Succeeded)
                {
                    return Ok(new { Message = "Registration successful" });
                }
                return BadRequest(new { Message = "Registration failed", Errors = result.Errors });
            }
            return BadRequest(new { Message = "Registration failed", ModelState });
        }


        [HttpPut("EditDoctor")]
        public async Task<IActionResult> EditDoctor(int id, [FromForm]DoctorRegistrationDto DoctorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = ModelState });
            }
            var result = await _UnitOfWork.Doctors.FindAsync(b => b.Id == id, new[] {"User"});

            if (result == null)
            {
                return BadRequest(new { Message = "there is no doctor with that id " });
            }

            var ishavebooking = _UnitOfWork.Booking.Any(b => b.DoctorId == id);

            if (ishavebooking)
            {
                return BadRequest(new { Message = "The Doctor has request, can't delete " });
            }

            
            Images.delete(result.User.Image);
            var uniquefilaname = Images.Add(DoctorDto.image);

            result.User.Image = uniquefilaname;
            result.User.FirstName = DoctorDto.FirstName;
            result.User.LastName = DoctorDto.LastName;
            result.User.Email = DoctorDto.Email;
            result.User.PhoneNumber = DoctorDto.PhoneNumber;
            result.SpecializationId = DoctorDto.SpecializationId;
            result.User.Gender = DoctorDto.Gender; 
            result.User.DateOfBirth = DoctorDto.DateOfBirth;
            result.User.UserName = DoctorDto.FirstName + DoctorDto.LastName;

             _UnitOfWork.UserAuthentication.update(result.User, DoctorDto.Password);
            _UnitOfWork.Save();

            return Ok(new { Message = "Update Doctor successful" });
        }

        [HttpDelete("DeletDoctor")]
        public async Task<IActionResult> DeletDoctor(int id)
        {

            var result = await _UnitOfWork.Doctors.FindAsync(b => b.Id == id);

            if (result == null)
            {
                return BadRequest(new { Message = "There is no doctor with that id " });
            }
            var ishavebooking = _UnitOfWork.Booking.Any(Booking => Booking.DoctorId == id);

            if (ishavebooking)
            {
                return BadRequest(new { Message = "The Doctor has request, can't delete " });
            }
            var user = await _UnitOfWork.ApplicationUser.FindAsync(b => b.Id == result.UserId);

            _UnitOfWork.Doctors.Delete(result);
            _UnitOfWork.ApplicationUser.Delete(user);
            _UnitOfWork.Save(); 

            return Ok(new { Message = "Delete Doctor successful" });

        }
        #endregion




        #region Patient
        [HttpGet("GetAllPatients")]
        public async Task<IActionResult> GetAllPatients([FromQuery] PaginationDto paginationDto)
        {
            if(String.IsNullOrEmpty(paginationDto.search))
            {
                return Ok(_UnitOfWork.ApplicationUser.GetAll<PatientDto>(u => u.AccountType == "Patient", paginationDto.page, paginationDto.pagesize, PatientDto.PatientSelector));
            }
            var result = _UnitOfWork.ApplicationUser.GetAllSearch<PatientDto> (u => u.AccountType == "Patient",paginationDto.page, paginationDto.pagesize, PatientDto.PatientSelector, PatientDto.PatientSearch( paginationDto.search));
            return Ok(result);
        }


        [HttpGet("GetPateintById")]
        public async Task<IActionResult> GetPateintById(int id )
        {
            var result = _UnitOfWork.ApplicationUser.GetById<PatientWithBookingDto>(p => p.Id == id && p.AccountType == "Patient" , PatientWithBookingDto.PatientWithBookingSelector);
            if (result == null)
            {
                return BadRequest(new { Message = "there is No Patient with that id " });
            }
            return Ok(result);
        }
        #endregion


       

    }


}
