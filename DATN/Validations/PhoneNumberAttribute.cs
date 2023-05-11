using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DATN.Validations
{
    public class PhoneNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            // Kiểm tra xem giá trị đầu vào có phải là một chuỗi hay không
            if (value is string phone)
            {
                if(!long.TryParse(phone, out long result))
                {
                    return false;
                }    
                else
                {
                    // Loại bỏ các khoảng trắng và dấu cách trong số điện thoại
                    phone = Regex.Replace(phone, @"[^\d]", "");
                    // Kiểm tra xem số điện thoại có đúng định dạng không
                    if (phone.Length == 10 && phone.StartsWith("0") && (phone.StartsWith("01") || phone.StartsWith("09") || phone.StartsWith("03") || phone.StartsWith("07") || phone.StartsWith("08")))
                    {
                        return true;
                    }
                    else if (phone.Length == 11 && phone.StartsWith("84") && (phone.StartsWith("841") || phone.StartsWith("849") || phone.StartsWith("843") || phone.StartsWith("847") || phone.StartsWith("848")))
                    {
                        return true;
                    }
                }    
            }

            // Trả về một thông báo lỗi nếu giá trị không phải là số điện thoại hợp lệ
            return false;
        }
    }
}
