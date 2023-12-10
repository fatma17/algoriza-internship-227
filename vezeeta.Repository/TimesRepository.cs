using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models;
using Vezeeta.Core.Repository;

namespace vezeeta.Repository
{
    public class TimesRepository : BaseRepository<Times>, ITimesRepository
    {
        protected ApplicationDbContext _context;
        public TimesRepository(ApplicationDbContext context) : base(context)
        {

        }

        public (int? timeid, int? doctorid, int? price) FindTimeWithPrice(int id) 
        {
            var result = _context.Times.Where(time => time.Id == id).Include(T=>T.Appointment)
                                                           .ThenInclude(A=>A.Doctor)
                                                           .Select(T => new {
                                                                    timeid = T.Id,
                                                                    doctorid = T.Appointment.DoctorId,
                                                                    price =T.Appointment.Doctor.Price
                                                           }).SingleOrDefault();                                                        

            return (result!=null)? ( result.timeid, result.doctorid, result.price ):(null,null,null);
        }
    }
}
