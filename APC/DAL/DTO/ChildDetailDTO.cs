using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class ChildDetailDTO
    {
        public int ChildID { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string ImagePath { get; set; }
        public int GenderID { get; set; }
        public string GenderName { get; set; }
        public int NationalityID { get; set; }
        public string NationalityName { get; set; }
        public int MotherID { get; set; }
        public string MothersName { get; set; }
        public string MothersSurname { get; set; }
        public int MotherNationalityID { get; set; }
        public string MotherNationalityName { get; set; }
        public string MotherImagePath { get; set; }
        public int FatherID { get; set; }
        public string FathersName { get; set; }
        public string FathersSurname { get; set; }
        public int FatherNationalityID { get; set; }
        public string FatherNationalityName { get; set; }
        public string FatherImagePath { get; set; }
        public bool isNationalityDeleted { get; set; }

    }
}
