using System;

namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime Dob)
        {
            var today = DateTime.Today;
            var age = today.Year - Dob.Year;
            if (Dob.Date > today.AddYears(-age)) age--;
            return age;
        }
        public static int CalculateDaysLastLogin(this DateTime LastLogin)
        {
            var today = DateTime.Today;
            var lastLogin = today - LastLogin;
            return lastLogin.Days;
        }
    }
}
