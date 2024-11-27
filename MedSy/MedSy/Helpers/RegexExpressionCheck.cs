using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MedSy.Helpers
{
    public class RegexExpressionCheck
    {
        public static bool IsValidUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;
            string usernameRegex = @"^[a-zA-Z0-9_]{4,20}$";
            return Regex.IsMatch(username, usernameRegex);
        }

        // Kiểm tra password khi đăng nhập
        public static bool IsValidLoginPassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && password.Length >= 6;
        }

        // Kiểm tra password khi đăng ký
        public static bool IsValidRegisterPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            string passwordRegex = @"^(?=.*[a-z])(?=.*\d)(?!.*\s).{6,}$";
            return Regex.IsMatch(password, passwordRegex);
        }

        // Kiểm tra email
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailRegex);
        }

        // Kiểm tra số điện thoại hợp lệ
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            // Regex kiểm tra định dạng số điện thoại
            string phoneRegex = @"^[0-9]{1,4}?[-.\s]?(\(?\d{1,3}\)?[-.\s]?)?[\d\s.-]{7,15}$";
            return Regex.IsMatch(phoneNumber, phoneRegex);
        }
    }
}
