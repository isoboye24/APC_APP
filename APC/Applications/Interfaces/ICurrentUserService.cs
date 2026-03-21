using System;

namespace APC.Applications.Interfaces
{
    public interface ICurrentUserService
    {
        int MemberId { get; }
        string Username { get; }
        int AccessLevel { get; }

        void SetUser(int memberId, string username, int accessLevel);
    }
}
