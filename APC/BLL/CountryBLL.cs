using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APC.DAL;
using APC.DAL.DAO;
using APC.DAL.DTO;

namespace APC.BLL
{
    public class CountryBLL : IBLL<CountryDTO, CountryDetailDTO>
    {
        CountryDAO dao = new CountryDAO();
        public bool Delete(CountryDetailDTO entity)
        {
            COUNTRY country = new COUNTRY();
            country.countryID = entity.CountryID;
            return dao.Delete(country);
        }

        public bool GetBack(CountryDetailDTO entity)
        {
            return dao.GetBack(entity.CountryID);
        }

        public bool Insert(CountryDetailDTO entity)
        {
            COUNTRY country = new COUNTRY();
            country.countryName = entity.CountryName;
            return dao.Insert(country);
        }

        public CountryDTO Select()
        {
            CountryDTO dto = new CountryDTO();
            dto.Countries = dao.Select();
            return dto;
        }

        public bool Update(CountryDetailDTO entity)
        {
            COUNTRY country = new COUNTRY();
            country.countryID = entity.CountryID;
            country.countryName = entity.CountryName;
            return dao.Update(country);
        }
    }
}
