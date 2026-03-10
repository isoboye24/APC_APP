using System;

namespace APC.Domain.Entities
{
    public class MembershipInfo
    {
        public DateTime MembershipDate { get; private set; }
        public int MembershipStatusId { get; private set; }
        public int PositionId { get; private set; }
        public int PermissionId { get; private set; }

        public MembershipInfo(DateTime membershipDate, int membershipStatusId, int positionId, int permissionId)
        {
            SetMembershipDate(membershipDate);
            SetMembershipStatus(membershipStatusId);
            SetPosition(positionId);
            SetPermission(permissionId);
        }

        private void SetMembershipDate(DateTime date)
        {
            if (date.Year < 1999 || date.Year > DateTime.Now.Year + 1)
                throw new ArgumentException("Invalid year. Choose from 1930 to " + DateTime.Now.Year);

            MembershipDate = date;
        }

        public void UpdateMembershipDate(DateTime newDate)
        {
            SetMembershipDate(newDate);
        }

        private void SetMembershipStatus(int statusId)
        {
            if (statusId < 0)
                throw new ArgumentException("Invalid membership status !!!");

            MembershipStatusId = statusId;
        }

        public void UpdateMembershipStatus(int newStatusId)
        {
            SetMembershipStatus(newStatusId);
        }

        private void SetPosition(int positionId)
        {
            if (positionId < 0)
                throw new ArgumentException("Invalid position !!!");

            PositionId = positionId;
        }

        public void UpdatePosition(int newPositionId)
        {
            SetPosition(newPositionId);
        }
        
        private void SetPermission(int permissionId)
        {
            if (permissionId < 0)
                throw new ArgumentException("Invalid permission !!!");

            PositionId = permissionId;
        }

        public void UpdatePermission(int newPermissionId)
        {
            SetPermission(newPermissionId);
        }
    }
}
