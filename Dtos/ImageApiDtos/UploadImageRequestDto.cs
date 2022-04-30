using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CarSee.Extensions.Attributes;
using Microsoft.AspNetCore.Http;

namespace CarSee.Dtos
{
    public class UploadImageRequestDto
    {   
        [MaxNumberOfFile(5)]
        public List<IFormFile> ImageFile { get; set; }
    }
}