using System;
using System.Drawing;
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

        public async Task<SPGetFileDto> Get(string fileName)
        {
            try
            {
                var filePath = Path.Combine(_options.LocalStorageProvider.FilePath, fileName);
                var extension = Path.GetExtension(filePath);
                var image = System.IO.File.OpenRead(filePath);

                var getFile = new SPGetFileDto()
                {
                    FileName = fileName,
                    FilePath = filePath,
                    FileStream = image,
                    ContentType = Mapping.GetContentType(extension)
                };

                return getFile;
            }
            catch (System.Exception)
            {
                 throw;
            }
        }
    }
    
}