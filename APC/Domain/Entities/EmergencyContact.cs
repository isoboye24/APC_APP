using System;

namespace APC.Domain.Entities
{
    public class EmergencyContact
    {
        public string NextOfKin { get; private set; }
        public int RelationshipToNextOfKinId { get; private set; }

        public EmergencyContact(string nextOfKin, int relationshipToNextOfKinId)
        {
            SetNextOfKin(nextOfKin);
            SetRelationshipToNextOfKin(relationshipToNextOfKinId);
        }

        private void SetNextOfKin(string nextOfKin)
        {
            if (string.IsNullOrWhiteSpace(nextOfKin))
                throw new ArgumentException("Next of kin is required");

            NextOfKin = nextOfKin.Trim();
        }

        public void UpdateNextOfKin(string newNextOfKin)
        {
            SetNextOfKin(newNextOfKin);
        }

        private void SetRelationshipToNextOfKin(int relationshipToNextOfKinId)
        {
            if (relationshipToNextOfKinId < 0)
                throw new ArgumentException("Relationship to next of kin is required !!!");

            RelationshipToNextOfKinId = relationshipToNextOfKinId;
        }

        public void UpdateRelationshipToNextOfKin(int newRelationshipToNextOfKinId)
        {
            SetRelationshipToNextOfKin(newRelationshipToNextOfKinId);
        }
    }
}
