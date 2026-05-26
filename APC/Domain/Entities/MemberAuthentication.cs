using APC.DAL;
using System;
using System.Windows.Forms;

namespace APC.Domain.Entities
{
    public class MemberAuthentication
    {
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }

        public MemberAuthentication(string username, DateTime birthday)
        {
            SetUsername(username);
            SetPasswordHash(birthday);
        }

        private void SetUsername(string username)
        {           
            if (string.IsNullOrWhiteSpace(username))
            {
                Username = "apc20001";
            }

            string getDigits = username.Substring(3);
            int convertDigits = Convert.ToInt32(getDigits) + 1;
            Username = "apc" + convertDigits;

            Username = username.Trim();
        }

        public void UpdateUsername(string newUsername)
        {
            SetUsername(newUsername);
        }

        private void SetPasswordHash(DateTime? birthday)
        {
            if (!birthday.HasValue)
                throw new ArgumentException("Birthday is required", nameof(birthday));

            DateTime birthDate = birthday.Value;

            int day = birthDate.Day;
            int month = birthDate.Month;

            string year = (birthDate.Year % 100).ToString("D2");

            string password = $"{day:D2}{month:D2}{year}";

            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        }

        public void UpdatePasswordHash(DateTime newBirthday)
        {
            SetPasswordHash(newBirthday);
        }
    }
}
