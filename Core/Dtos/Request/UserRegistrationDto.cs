﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Core.Dtos.Request
{
    public class UserRegistrationDto : RegistrationDto
    {
        public IFormFile? Image { get; set; }
    }
}
