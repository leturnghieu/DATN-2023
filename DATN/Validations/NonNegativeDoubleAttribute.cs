using System.ComponentModel.DataAnnotations;

namespace DATN.Validations
{
    public class NonNegativeDoubleAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Kiểm tra xem giá trị đầu vào có phải là một số thực không âm hay không
            if (value != null && double.TryParse(value.ToString(), out double result) && result >= 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                // Trả về một thông báo lỗi nếu giá trị không phải là số thực không âm
                return new ValidationResult("Giá trị phải là số thực không âm.");
            }
        }
    }
}
