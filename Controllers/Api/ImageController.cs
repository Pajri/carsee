
using System.Collections.Generic;
using System.Threading.Tasks;
using CarSee.Dtos;
using CarSee.Utility.StorageProvider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarSee.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {   
        private IStorageProvider _storageProvider { get; set; }
        
        public ImageController(IStorageProvider storageProvider)
        {
            _storageProvider = storageProvider;
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> Get(string fileName)
        {
            var getImage = await _storageProvider.Get(fileName);
            return File(getImage.FileStream, getImage.ContentType);
        }

        [HttpPost]
        public async Task<string> Post(IFormFile file)
        {
            var response = await _storageProvider.Save(file, true);
            return response.FileName;
        }
    }
}