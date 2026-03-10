using System;

namespace APC.Domain.Entities
{
    public class ContactInfo
    {
        public string Email { get; private set; }
        public string HouseAddress { get; private set; }
        public string PhoneNumber { get; private set; }
        public string PhoneNumber2 { get; private set; }
        public string PhoneNumber3 { get; private set; }

        public ContactInfo(string email, string houseAddress, string phoneNumber, string phoneNumber2, string phoneNumber3)
        {
            SetEmail(email);
            SetHouseAddress(houseAddress);
            SetPhoneNumber(phoneNumber);
            SetPhoneNumber2(phoneNumber2);
            SetPhoneNumber3(phoneNumber3);
        }

        private void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                email = "noemail@gmail.com";

            email = email.Trim();

            if (!email.Contains("@"))
                throw new ArgumentException("Email must contain '@'.", nameof(email));

            if (email.StartsWith("@") || email.EndsWith("@"))
                throw new ArgumentException("Invalid email format.", nameof(email));

            Email = email.ToLowerInvariant();
        }

        public void UpdateEmail(string newEmail)
        {
            SetEmail(newEmail);
        }

        private void SetHouseAddress(string houseAddress)
        {
            if (string.IsNullOrWhiteSpace(houseAddress))
                houseAddress = "No House Address";

            HouseAddress = houseAddress.Trim();
        }

        public void UpdateHouseAddress(string newHouseAddress)
        {
            SetHouseAddress(newHouseAddress);
        }
        
        private void SetPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number is required");

            PhoneNumber = phoneNumber.Trim();
        }

        public void UpdatePhoneNumber(string newPhoneNumber)
        {
            SetPhoneNumber(newPhoneNumber);
        }

        private void SetPhoneNumber2(string phoneNumber2)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber2))
                phoneNumber2 = null;

            PhoneNumber2 = phoneNumber2.Trim();
        }

        public void UpdatePhoneNumber2(string newPhoneNumber2)
        {
            SetPhoneNumber2(newPhoneNumber2);
        }
        
        private void SetPhoneNumber3(string phoneNumber3)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber3))
                phoneNumber3 = null;

            PhoneNumber3 = phoneNumber3.Trim();
        }

        public void UpdatePhoneNumber3(string newPhoneNumber3)
        {
            SetPhoneNumber3(newPhoneNumber3);
        }
    }
}
