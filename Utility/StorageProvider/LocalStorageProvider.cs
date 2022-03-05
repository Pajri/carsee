using System;
using System.IO;
using System.Threading.Tasks;
using CarSee.Utility.ContentTypeFileExtensionMapping;
using CarSee.Utility.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace CarSee.Utility.StorageProvider
{
    public class LocalStorageProvider : IStorageProvider
    {
        private readonly StorageProviderConfig _options;

        public LocalStorageProvider(IOptions<StorageProviderConfig> options)
        {
            _options = options.Value;
        }
        
        public async Task<SPSaveResponseDto> Save(IFormFile file, bool useUniqueFileName = false)
        {
            try
            {
                string fileName = file.FileName;
                if(useUniqueFileName)
                {
                    fileName = Guid.NewGuid().ToString();
                    var fileExtension = Mapping.GetFileExtension(file.ContentType);
                    if(fileExtension != null) fileName = $"{fileName}{fileExtension}";
                }

                var filePath = Path.Combine(_options.LocalStorageProvider.FilePath, fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }

                return new SPSaveResponseDto
                {
                    FileName = fileName,
                    FilePath = filePath
                };
            }
            catch (System.Exception)
            {
                 throw;
            }
        }
    }
    
}