using System.ComponentModel.DataAnnotations;

namespace REST_API_XFIA.Data_structures
{
    public class DataUploded
    {
        [Required(ErrorMessage = "Please select a file.")]
        [AllowedExtensions(new string[] { ".csv"})]
        public IFormFile file { get; set; }

    }
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"This file extension is not allowed!";
        }
    }

    
}

