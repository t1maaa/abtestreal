using System;

namespace abtestreal.VM
{
    public class UserResponse : UserResponseBase
    {
        public DateTime Registered { get; set; }
        public DateTime LastSeen { get; set; }
    }
}