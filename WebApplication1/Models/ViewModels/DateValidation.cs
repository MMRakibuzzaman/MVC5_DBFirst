using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.CustomValidation
{
    public class DateLessThanTodayAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime dateValue = (DateTime)value;
                if (dateValue > DateTime.Now)
                {
                    return new ValidationResult(ErrorMessage ?? "Date cannot be in the future.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
