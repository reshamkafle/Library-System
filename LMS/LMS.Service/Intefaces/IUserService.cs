using LMS.Service.Identity;
using LMS.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Service.Intefaces
{
    public interface IUserService
    {
        Task<TokenResponse> Authenticate(LoginDto login);
        Task<ApplicationUser> GetById(string id);
        Task<IEnumerable<ApplicationUser>> GetAll(string role);
        Task Register(UserDto user);
        Task ChangePassword(ChangePasswordDto changePassword);
        Task UpdateUser(UserDto user);
        Task<string> GetLibraryNumber(string userId);
        Task Delete(string userId);
    }
}
