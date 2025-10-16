using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DAO
{
    public class ChildDAO : APCContexts, IDAO<ChildDetailDTO, CHILD>
    {
        public bool Delete(CHILD entity)
        {
            try
            {
                CHILD child = db.CHILD.First(x => x.childID == entity.childID);
                child.isDeleted = true;
                child.deletedDate = DateTime.Today;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public bool GetBack(int ID)
        {
            try
            {
                CHILD child = db.CHILD.First(x=>x.childID==ID);
                child.isDeleted = false;
                child.deletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(CHILD entity)
        {
            try
            {
                db.CHILD.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ChildDetailDTO> Select()
        {
            try
            {
                List<ChildDetailDTO> children = new List<ChildDetailDTO>();
                var list = (from c in db.CHILD.Where(x => x.isDeleted == false)
                            join g in db.GENDER on c.genderID equals g.genderID
                            join n in db.NATIONALITY on c.nationalityID equals n.nationalityID
                            join mm in db.MEMBER on c.motherID equals mm.memberID
                            join mn in db.NATIONALITY on mm.nationalityID equals mn.nationalityID
                            join mf in db.MEMBER on c.fatherID equals mf.memberID
                            join fn in db.NATIONALITY on mf.nationalityID equals fn.nationalityID
                            select new
                            {
                                childID = c.childID,
                                name = c.name,
                                surname = c.surname,
                                birthday = c.birthday,
                                imagePath = c.imagePath,
                                genderID = c.genderID,
                                genderName = g.genderName,
                                nationalityID = c.nationalityID,
                                nationality = n.nationality1,
                                motherID = c.motherID,
                                motherName = mm.name,
                                motherSurname = mm.surname,
                                motherImagePath = mm.imagePath,
                                motherNationalityID = mm.nationalityID,
                                motherNationalityName = mn.nationality1,
                                fatherID = c.fatherID,
                                fatherName = mf.name,
                                fatherSurname = mf.surname,
                                fatherImagePath = mf.imagePath,
                                fatherNationalityID = mf.nationalityID,
                                fatherNationalityName = fn.nationality1,
                            }).OrderBy(x => x.surname).ToList();
                foreach (var item in list)
                {
                    ChildDetailDTO dto = new ChildDetailDTO();
                    dto.ChildID = item.childID;
                    dto.Name = item.name;
                    dto.Surname = item.surname;
                    dto.Birthday = (DateTime)item.birthday;
                    dto.ImagePath = item.imagePath;
                    dto.GenderID = item.genderID;
                    dto.GenderName = item.genderName;
                    dto.NationalityID = item.nationalityID;
                    dto.NationalityName = item.nationality;
                    dto.MotherID = (int)item.motherID;
                    dto.MothersName = item.motherName;
                    dto.MothersSurname = item.motherSurname;
                    dto.MotherImagePath = item.motherImagePath;
                    dto.MotherNationalityID = item.motherNationalityID;
                    dto.MotherNationalityName = item.motherNationalityName;
                    dto.FatherID = (int)item.fatherID;
                    dto.FathersName = item.fatherName;
                    dto.FathersSurname = item.fatherSurname;
                    dto.FatherImagePath = item.fatherImagePath;
                    dto.FatherNationalityID = item.fatherNationalityID;
                    dto.FatherNationalityName = item.fatherNationalityName;
                    children.Add(dto);
                }
                return children;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ChildDetailDTO> Select(bool isDeleted)
        {
            try
            {
                List<ChildDetailDTO> children = new List<ChildDetailDTO>();
                var list = (from c in db.CHILD.Where(x => x.isDeleted == isDeleted)
                            join g in db.GENDER on c.genderID equals g.genderID
                            join n in db.NATIONALITY.Where(x => x.isDeleted == false) on c.nationalityID equals n.nationalityID
                            join mm in db.MEMBER on c.motherID equals mm.memberID
                            join mn in db.NATIONALITY.Where(x => x.isDeleted == false) on mm.nationalityID equals mn.nationalityID
                            join mf in db.MEMBER on c.fatherID equals mf.memberID
                            join fn in db.NATIONALITY.Where(x => x.isDeleted == false) on mf.nationalityID equals fn.nationalityID
                            select new
                            {
                                childID = c.childID,
                                name = c.name,
                                surname = c.surname,
                                birthday = c.birthday,
                                imagePath = c.imagePath,
                                genderID = c.genderID,
                                genderName = g.genderName,
                                nationalityID = c.nationalityID,
                                nationality = n.nationality1,
                                motherID = c.motherID,
                                motherName = mm.name,
                                motherSurname = mm.surname,
                                motherImagePath = mm.imagePath,
                                motherNationalityID = mm.nationalityID,
                                motherNationalityName = mn.nationality1,
                                fatherID = c.fatherID,
                                fatherName = mf.name,
                                fatherSurname = mf.surname,
                                fatherImagePath = mf.imagePath,
                                fatherNationalityID = mf.nationalityID,
                                fatherNationalityName = fn.nationality1,
                                isNationalityDeleted = n.isDeleted
                            }).OrderBy(x => x.surname).ToList();
                foreach (var item in list)
                {
                    ChildDetailDTO dto = new ChildDetailDTO();
                    dto.ChildID = item.childID;
                    dto.Name = item.name;
                    dto.Surname = item.surname;
                    dto.Birthday = (DateTime)item.birthday;
                    dto.ImagePath = item.imagePath;
                    dto.GenderID = item.genderID;
                    dto.GenderName = item.genderName;
                    dto.NationalityID = item.nationalityID;
                    dto.NationalityName = item.nationality;
                    dto.isNationalityDeleted = item.isNationalityDeleted;
                    dto.MotherID = (int)item.motherID;
                    dto.MothersName = item.motherName;
                    dto.MothersSurname = item.motherSurname;
                    dto.MotherImagePath = item.motherImagePath;
                    dto.MotherNationalityID = item.motherNationalityID;
                    dto.MotherNationalityName = item.motherNationalityName;
                    dto.FatherID = (int)item.fatherID;
                    dto.FathersName = item.fatherName;
                    dto.FathersSurname = item.fatherSurname;
                    dto.FatherImagePath = item.fatherImagePath;
                    dto.FatherNationalityID = item.fatherNationalityID;
                    dto.FatherNationalityName = item.fatherNationalityName;
                    children.Add(dto);
                }
                return children;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SelectAllChildren()
        {
            try
            {
                int totalChildren = db.CHILD.Count(x => x.isDeleted == false);
                return totalChildren;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SelectAllChildrenCount(int ID)
        {
            try
            {
                int totalChildren = db.CHILD.Count(x => x.isDeleted == false && x.motherID == ID || x.fatherID == ID);
                return totalChildren;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ChildDetailDTO> SelectViewParentChild(int ID)
        {
            try
            {
                List<ChildDetailDTO> children = new List<ChildDetailDTO>();
                var list = (from c in db.CHILD.Where(x => x.isDeleted == false && x.motherID ==ID || x.fatherID==ID)
                            join g in db.GENDER on c.genderID equals g.genderID
                            join n in db.NATIONALITY on c.nationalityID equals n.nationalityID
                            join mm in db.MEMBER on c.motherID equals mm.memberID
                            join mn in db.NATIONALITY on mm.nationalityID equals mn.nationalityID
                            join mf in db.MEMBER on c.fatherID equals mf.memberID
                            join fn in db.NATIONALITY on mf.nationalityID equals fn.nationalityID
                            select new
                            {
                                childID = c.childID,
                                name = c.name,
                                surname = c.surname,
                                birthday = c.birthday,
                                imagePath = c.imagePath,
                                genderID = c.genderID,
                                genderName = g.genderName,
                                nationalityID = c.nationalityID,
                                nationality = n.nationality1,
                                motherID = c.motherID,
                                motherName = mm.name,
                                motherSurname = mm.surname,
                                motherImagePath = mm.imagePath,
                                motherNationalityID = mm.nationalityID,
                                motherNationalityName = mn.nationality1,
                                fatherID = c.fatherID,
                                fatherName = mf.name,
                                fatherSurname = mf.surname,
                                fatherImagePath = mf.imagePath,
                                fatherNationalityID = mf.nationalityID,
                                fatherNationalityName = fn.nationality1,
                            }).OrderBy(x => x.surname).ToList();
                foreach (var item in list)
                {
                    ChildDetailDTO dto = new ChildDetailDTO();
                    dto.ChildID = item.childID;
                    dto.Name = item.name;
                    dto.Surname = item.surname;
                    dto.Birthday = (DateTime)item.birthday;
                    dto.ImagePath = item.imagePath;
                    dto.GenderID = item.genderID;
                    dto.GenderName = item.genderName;
                    dto.NationalityID = item.nationalityID;
                    dto.NationalityName = item.nationality;
                    dto.MotherID = (int)item.motherID;
                    dto.MothersName = item.motherName;
                    dto.MothersSurname = item.motherSurname;
                    dto.MotherImagePath = item.motherImagePath;
                    dto.MotherNationalityID = item.motherNationalityID;
                    dto.MotherNationalityName = item.motherNationalityName;
                    dto.FatherID = (int)item.fatherID;
                    dto.FathersName = item.fatherName;
                    dto.FathersSurname = item.fatherSurname;
                    dto.FatherImagePath = item.fatherImagePath;
                    dto.FatherNationalityID = item.fatherNationalityID;
                    dto.FatherNationalityName = item.fatherNationalityName;
                    children.Add(dto);
                }
                return children;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public int SelectAllMaleChildren()
        {
            try
            {
                int totalChildren = db.CHILD.Count(x => x.isDeleted == false && x.genderID==1);
                return totalChildren;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SelectAllFemaleChildren()
        {
            try
            {
                int totalChildren = db.CHILD.Count(x => x.isDeleted == false && x.genderID == 2);
                return totalChildren;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(CHILD entity)
        {
            try
            {
                CHILD child = db.CHILD.First(x => x.childID == entity.childID);
                child.name = entity.name;
                child.surname = entity.surname;
                child.birthday = entity.birthday;
                child.imagePath = entity.imagePath;
                child.genderID = entity.genderID;
                child.nationalityID = entity.nationalityID;
                child.motherID = entity.motherID;
                child.fatherID = entity.fatherID;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
