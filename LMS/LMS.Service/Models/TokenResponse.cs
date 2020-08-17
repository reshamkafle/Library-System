using LMS.Service.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Service.Models
{
    public class TokenResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public bool IsAdmin { get; set; }
        public TokenResponse(ApplicationUser user, bool isAdmin, string token)
        {
            Id = user.Id;
            IsAdmin = isAdmin;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.UserName;
            Token = token;
        }
    }
}
