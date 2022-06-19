using System;
using System.Collections.Generic;
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

        public async Task<SPSaveResponseDto> Save(String base64Image)
        {
            try
            {
                Image image;
                string filePath = _options.LocalStorageProvider.FilePath;
                string fileName = "";

                byte[] bytes = Convert.FromBase64String(base64Image);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                    fileName = $"{Guid.NewGuid()}.{image.RawFormat.ToString().ToLower()}";
                    filePath = Path.Combine(filePath, fileName);

                    using (FileStream fs = File.Create(filePath))
                    {
                        ms.WriteTo(fs);
                    }
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

        public async Task<List<SPSaveResponseDto>> Save(List<IFormFile> files, bool useUniqueFileName = false)
        {
            try
            {
                List<SPSaveResponseDto> responseList = new List<SPSaveResponseDto>();
                foreach (var file in files)
                {
                    var response = await Save(file, useUniqueFileName);
                    responseList.Add(response);
                }

                return responseList;
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