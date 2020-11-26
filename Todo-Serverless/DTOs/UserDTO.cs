using System;
using System.Collections.Generic;
using System.Text;

namespace Todo_Serverless.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }

    public class UserReadDTO : UserDTO
    {
        public string Id { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateModified { get; set; }
    }
    public class LoginDTO
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
