using System.ComponentModel.DataAnnotations;

namespace HumanRegisterWebApi.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _allowedExtensions;

        public AllowedExtensionsAttribute(params string[] allowedExtensions)  //params geras
        {
            _allowedExtensions = allowedExtensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Value can't be null");
            }

            if (value is IFormFile formFile)
            {
                var extension = Path.GetExtension(formFile.FileName);
                if (!_allowedExtensions.Contains(extension.ToUpper()))
                {
                    return new ValidationResult($"This image extension is not allowed. Allowed extensions: {string.Join(',', _allowedExtensions)}");
                }
            }

            return ValidationResult.Success;
        }

    }
}
