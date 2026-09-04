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
                throw new ArgumentException("Username is required.", nameof(username));

            username = username.Trim();

            if (!username.StartsWith("apc"))
                throw new ArgumentException(
                    "Username must start with 'apc'.",
                    nameof(username));

            Username = username;
        }

        private void SetPasswordHash(DateTime? birthday)
        {
            if (!birthday.HasValue)
                throw new ArgumentException(
                    "Birthday is required.",
                    nameof(birthday));

            DateTime birthDate = birthday.Value;

            string password =
                $"{birthDate.Day:D2}{birthDate.Month:D2}{birthDate.Year % 100:D2}";

            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        }

        public void UpdateUsername(string newUsername)
        {
            SetUsername(newUsername);
        }

        public void UpdatePasswordHash(DateTime newBirthday)
        {
            SetPasswordHash(newBirthday);
        }
    }
}
