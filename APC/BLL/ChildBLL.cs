using APC.DAL;
using APC.DAL.DAO;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.BLL
{
    public class ChildBLL : IBLL<ChildDTO, ChildDetailDTO>
    {
        ChildDAO dao = new ChildDAO();
        MotherDAO motherDAO = new MotherDAO();
        FatherDAO fatherDAO = new FatherDAO();
        NationalityDAO nationalityDAO = new NationalityDAO();
        GenderDAO genderDAO = new GenderDAO();
        MemberDetailDTO detail = new MemberDetailDTO();
        public bool Delete(ChildDetailDTO entity)
        {
            CHILD child = new CHILD();
            child.childID = entity.ChildID;
            return dao.Delete(child);
        }

        public bool GetBack(ChildDetailDTO entity)
        {
            return dao.GetBack(entity.ChildID);
        }

        public bool Insert(ChildDetailDTO entity)
        {
            CHILD child = new CHILD();
            child.name = entity.Name;
            child.surname = entity.Surname;
            child.birthday = entity.Birthday;
            child.imagePath = entity.ImagePath;
            child.genderID = entity.GenderID;
            child.nationalityID = entity.NationalityID;
            child.motherID = entity.MotherID;
            child.fatherID = entity.FatherID;
            return dao.Insert(child);
        }

        public ChildDTO Select()
        {
            ChildDTO dto = new ChildDTO();
            dto.Fathers = fatherDAO.Select();
            dto.Mothers = motherDAO.Select();
            dto.Nationalities = nationalityDAO.Select();
            dto.Genders = genderDAO.Select();
            dto.Children = dao.Select();
            return dto;
        }
        public int SelectAllChildren()
        {
            return dao.SelectAllChildren();
        }
        public int SelectAllChildrenCount(int ID)
        {
            return dao.SelectAllChildrenCount(ID);
        }
        public ChildDTO SelectViewParentChild(int ID)
        {
            ChildDTO dto = new ChildDTO();
            dto.Children = dao.SelectViewParentChild(ID);
            return dto;
        }
        public int SelectAllMaleChildren()
        {
            return dao.SelectAllMaleChildren();
        }
        public int SelectAllFemaleChildren()
        {
            return dao.SelectAllFemaleChildren();
        }

        public bool Update(ChildDetailDTO entity)
        {
            CHILD child = new CHILD();
            child.childID = entity.ChildID;
            child.name = entity.Name;
            child.surname = entity.Surname;
            child.birthday = entity.Birthday;
            child.imagePath = entity.ImagePath;
            child.genderID = entity.GenderID;
            child.nationalityID = entity.NationalityID;
            child.motherID = entity.MotherID;
            child.fatherID = entity.FatherID;
            return dao.Update(child);
        }        
    }
}
