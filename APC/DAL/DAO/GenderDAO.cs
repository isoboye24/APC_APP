using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DAO
{
    public class GenderDAO : APCContexts
    {
        public List<GenderDetailDTO> Select()
        {
            try
            {
                List<GenderDetailDTO> genders = new List<GenderDetailDTO>();
                var list = db.GENDERs.OrderBy(x => x.genderName).ToList();
                foreach (var item in list)
                {
                    GenderDetailDTO dto = new GenderDetailDTO();
                    dto.GenderID = item.genderID;
                    dto.GenderName = item.genderName;
                    genders.Add(dto);
                }
                return genders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
