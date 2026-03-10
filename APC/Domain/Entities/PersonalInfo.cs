using System;

namespace APC.Domain.Entities
{
    public class PersonalInfo
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime Birthday { get; private set; }
        public string ImagePath { get; private set; }
        public int GenderId { get; private set; }

        public PersonalInfo(string firstName, string lastName, DateTime birthday, string imagePath, int genderId)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
            SetBirthday(birthday);
            SetImagePath(imagePath);
            SetGender(genderId);
        }

        private void SetFirstName(string firstname)
        {
            if (string.IsNullOrWhiteSpace(firstname))
                throw new ArgumentException("First name is required");

            FirstName = firstname.Trim();
        }

        public void UpdateFirstName(string newFirstname)
        {
            SetFirstName(newFirstname);
        }

        private void SetLastName(string lastname)
        {
            if (string.IsNullOrWhiteSpace(lastname))
                throw new ArgumentException("Last name is required");

            LastName = lastname.Trim();
        }

        public void UpdateLastName(string newLastname)
        {
            SetLastName(newLastname);
        }

        private void SetBirthday(DateTime birthday)
        {
            if (birthday.Year < 1930 || birthday.Year > DateTime.Now.Year + 1)
                throw new ArgumentException("Invalid year. Choose from 1930 to " + DateTime.Now.Year);

            Birthday = birthday;
        }

        public void UpdateBirthday(DateTime newBirthday)
        {
            SetBirthday(newBirthday);
        }

        private void SetImagePath(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
                throw new ArgumentException("Image path is required");

            LastName = imagePath.Trim();
        }

        public void UpdateImagePath(string newImagePath)
        {
            SetImagePath(newImagePath);
        }

        private void SetGender(int genderId)
        {
            if (genderId < 0)
                throw new ArgumentException("Invalid gender !!!");

            GenderId = genderId;
        }

        public void UpdateGender(int newGenderId)
        {
            SetGender(newGenderId);
        }
    }
}
