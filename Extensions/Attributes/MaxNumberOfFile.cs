using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CarSee.Extensions.Attributes
{
    public class MaxNumberOfFile : ValidationAttribute
    {
        private readonly string[] _extensions;
        private int _maxFile;

        public MaxNumberOfFile(int maxFile)
        {
            _maxFile = maxFile;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as List<IFormFile>;
            if (file == null) return ValidationResult.Success;
            
            if(file.Count > _maxFile)
                return new ValidationResult(GetErrorMessage());
            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Maximum number of uploaded file is {_maxFile}.";
        }
    }
}