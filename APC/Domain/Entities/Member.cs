using System;

namespace APC.Domain.Entities
{
    public class Member
    {
        public int MemberId { get; private set; }

        public MemberAuthentication MemberAuthentication { get; private set; }
        public PersonalInfo PersonalInfo { get; private set; }
        public ContactInfo ContactInfo { get; private set; }
        public MembershipInfo MembershipInfo { get; private set; }
        public DemographicInfo DemographicInfo { get; private set; }
        public EmergencyContact EmergencyContact { get; private set; }
        public LifeStatus LifeStatus { get; private set; }


        public Member(
            MemberAuthentication memberAuthentication, 
            PersonalInfo personalInfo, 
            ContactInfo contactInfo,
            MembershipInfo membershipInfo, 
            DemographicInfo demographicInfo, 
            EmergencyContact emergencyContact, 
            LifeStatus lifeStatus
            )
        {
            MemberAuthentication = memberAuthentication ?? throw new ArgumentNullException(nameof(memberAuthentication));
            PersonalInfo = personalInfo ?? throw new ArgumentNullException(nameof(personalInfo));
            ContactInfo = contactInfo ?? throw new ArgumentNullException(nameof(contactInfo));
            MembershipInfo = membershipInfo ?? throw new ArgumentNullException(nameof(membershipInfo));
            DemographicInfo = demographicInfo ?? throw new ArgumentNullException(nameof(demographicInfo));
            EmergencyContact = emergencyContact ?? throw new ArgumentNullException(nameof(emergencyContact));
            LifeStatus = lifeStatus ?? throw new ArgumentNullException(nameof(lifeStatus));
        }

        public static Member Rehydrate
            (
                int memberId,
                MemberAuthentication authentication,
                PersonalInfo personalInfo,
                ContactInfo contactInfo,
                MembershipInfo membershipInfo,
                DemographicInfo demographicInfo,
                EmergencyContact emergencyContact,
                LifeStatus lifeStatus
            )
            {
                var member = new Member(authentication, personalInfo, contactInfo,
                                        membershipInfo, demographicInfo, emergencyContact, lifeStatus);

                member.MemberId = memberId;

                return member;
            }

    }
}
