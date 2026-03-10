using System;

namespace APC.Domain.Entities
{
    public class MemberAuthentication
    {
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }

        public MemberAuthentication(string username, string passwordHash)
        {
            SetUsername(username);
            SetPasswordHash(passwordHash);
        }

        private void SetUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username is required");

            Username = username.Trim();
        }

        public void UpdateUsername(string newUsername)
        {
            SetUsername(newUsername);
        }

        private void SetPasswordHash(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required", nameof(password));

            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password.Trim());
        }

        public void UpdatePasswordHash(string newPassword)
        {
            SetPasswordHash(newPassword);
        }
    }
}
