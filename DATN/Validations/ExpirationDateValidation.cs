using System.ComponentModel.DataAnnotations;

namespace DATN.Validations
{
    public class ExpirationDateValidation : ValidationAttribute
    {
        private readonly string _productionDatePropertyName;

        public ExpirationDateValidation(string productionDatePropertyName)
        {
            _productionDatePropertyName = productionDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var expirationDate = (DateTime)value;

            var productionDateProperty = validationContext.ObjectType.GetProperty(_productionDatePropertyName);
            var productionDate = (DateTime)productionDateProperty.GetValue(validationContext.ObjectInstance, null);

            if (expirationDate > productionDate)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "Ngày hết hạn phải lớn hơn ngày sản xuất");
        }
    }
}
