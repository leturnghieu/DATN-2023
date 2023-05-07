using DATN.Models;
using System.ComponentModel.DataAnnotations;

namespace DATN.Validations
{
    public class DateLessThanNow : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DateTime inputDate = (DateTime)value;
            if (inputDate < DateTime.Now)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "Ngày không hợp lệ");
        }
    }
}
