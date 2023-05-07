using System.ComponentModel.DataAnnotations;

namespace DATN.Validations
{
    public class NonNegativeIntegerAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Kiểm tra xem giá trị đầu vào có phải là một số nguyên không âm hay không
            if (value != null && int.TryParse(value.ToString(), out int result) && result >= 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                // Trả về một thông báo lỗi nếu giá trị không phải là số nguyên không âm
                return new ValidationResult("Giá trị phải là số nguyên không âm.");
            }
        }
    }
}
