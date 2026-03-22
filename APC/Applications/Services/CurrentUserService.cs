using APC.Applications.Interfaces;

namespace APC.Applications.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public int MemberId { get; private set; }
        public string Username { get; private set; }
        public int AccessLevel { get; private set; }

        public void SetUser(int memberId, string username, int accessLevel)
        {
            MemberId = memberId;
            Username = username;
            AccessLevel = accessLevel;
        }
    }
}
