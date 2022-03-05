using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CarSee.Utility.StorageProvider
{
    public interface IStorageProvider
    {
        Task<SPSaveResponseDto> Save(IFormFile file, bool useUniqueFileName = false);
        Task<SPGetFileDto> Get(string fileName);
    }   
}