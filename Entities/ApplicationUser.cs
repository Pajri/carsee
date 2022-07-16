using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CarSee.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Car> Cars { get; set; }
    }
}