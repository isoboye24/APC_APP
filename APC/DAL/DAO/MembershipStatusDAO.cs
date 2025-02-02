using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DAO
{
    public class MembershipStatusDAO:APCContexts
    {
        public List<MembershipStatusDetailDTO> Select()
        {
            try
            {
                List<MembershipStatusDetailDTO> membershipStatuses = new List<MembershipStatusDetailDTO>();
                var list = db.MEMBERSHIP_STATUS.ToList();
                foreach (var item in list)
                {
                    MembershipStatusDetailDTO dto = new MembershipStatusDetailDTO();
                    dto.MembershipStatusID = item.membershipStatusID;
                    dto.MembershipStatus = item.membershipStatus;
                    membershipStatuses.Add(dto);
                }
                return membershipStatuses;
            }
            catch (Exception)
            {

                throw;
            }            
        }
    }
}
