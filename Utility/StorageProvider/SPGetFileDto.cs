using System.IO;

namespace CarSee.Utility.StorageProvider
{
    public class SPGetFileDto : FileDto
    {
        public string ContentType { get; set; }
        public FileStream FileStream { get; set; }
    }
    
    
}