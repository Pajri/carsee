using System.Collections.Generic;
using CarSee.Dtos;
using Microsoft.AspNetCore.Http;

namespace CarSee.ViewModels
{
    public class CarApiListingViewModel
    {

        public List<CarDto> CarList { get; set; }
        public string SearchParam { get; set; }
        
        public int PageCount { get; set; }
        public int CurrentPageIndex { get; set; }
    }

}