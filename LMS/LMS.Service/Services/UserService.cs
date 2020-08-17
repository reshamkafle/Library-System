using LMS.Core.Constant;
using LMS.Service.Data;
using LMS.Service.Helpers;
using LMS.Service.Identity;
using LMS.Service.Intefaces;
using LMS.Service.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Service.Services
{
    public class UserService : IUserService
    {     

        private readonly AppSettings _appSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppIdentityDbContext _dbContext;
        private readonly LMSContext _context;

        public UserService(
            IOptions<AppSettings> appSettings, 
            UserManager<ApplicationUser> userManager,
            AppIdentityDbContext dbContext,
            LMSContext context
            )
        {
            _appSettings = appSettings.Value;
            _userManager = userManager;
            _dbContext = dbContext;
            _context = context;
        }

        public async Task<string> GetLibraryNumber(string userId)
        {
            return (await _userManager.FindByIdAsync(userId)).LibraryCardNumber;
        }

        public async Task<TokenResponse> Authenticate(LoginDto login)
        {
            var user = await _userManager.FindByNameAsync(login.Username);
            if(user != null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                var token = await GenerateJwtTokenAsync(user);

                var isAdmin = await _userManager.IsInRoleAsync(user, AuthorizationConstants.Roles.ADMINISTRATORS);

                return new TokenResponse(user, isAdmin,token);
            }
            else
            {
                return null;
            }           
        }

        public async Task ChangePassword(ChangePasswordDto changePassword)
        {
            var user = await _userManager.FindByIdAsync(changePassword.Id);

            if(user != null)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, changePassword.Password);
                await _userManager.UpdateAsync(user);

            }
        }

        public  async Task<IEnumerable<ApplicationUser>> GetAll(string role)
        {
            return (await (from c in _dbContext.Users
                           join d in _dbContext.UserRoles on c.Id equals d.UserId
                           join e in _dbContext.Roles on d.RoleId equals e.Id
                           where e.Name == role
                           select new ApplicationUser
                           {
                               FirstName = c.FirstName,
                               LastName = c.LastName,
                               Id = c.Id,
                               UserName = c.UserName,
                               LibraryCardNumber = c.LibraryCardNumber
                           }).ToListAsync());     
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task Register(UserDto user)
        {
            var applicationUser = new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = user.Username, Email = user.Username, FirstName = user.FirstName, LastName = user.LastName, LibraryCardNumber = user.LibraryCardNumber };
            var result = await _userManager.CreateAsync(applicationUser, user.Password);

            if (result.Succeeded)
            {
                applicationUser = await _userManager.FindByNameAsync(user.Username);
                await _userManager.AddToRolesAsync(applicationUser, user.Roles);

            }
           
        }

        public async Task UpdateUser(UserDto user)
        {
            var _user = await _userManager.FindByNameAsync(user.Username);

            if (_user != null)
            {
                _user.FirstName = user.FirstName;
                _user.LastName = user.LastName;

                await _userManager.UpdateAsync(_user);

            }
        }


        private async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, user.Id.ToString()));
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task Delete(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }
    }
}
