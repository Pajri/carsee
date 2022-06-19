using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarSee.Dtos
{
    public class CarApiDto : CarDto
    {
        public string[] ImageBase64 { get; set; }
    }
}