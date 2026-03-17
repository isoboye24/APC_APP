using System;

namespace APC
{
    public class AuthenticationDTO
    {
        public int MemberId { get; set; }
        public string Username { get; set; }
        public int AccessLevel { get; set; }
    }
}
