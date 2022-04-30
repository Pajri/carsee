using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CarSee.Extensions.Attributes
{
    public class MaxNumberOfFileName : ValidationAttribute
    {
        private readonly string[] _extensions;
        private int _maxFile;

        public MaxNumberOfFileName(int maxFile)
        {
            _maxFile = maxFile;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var joinedFileName = value as string;
            if (joinedFileName == null) return ValidationResult.Success;

            var fileNameArr = joinedFileName.Split(';');
            if(fileNameArr.Length > _maxFile)
                return new ValidationResult(GetErrorMessage());
        
            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Maximum number of uploaded file is {_maxFile}.";
        }
    }
}