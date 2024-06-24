using System.Text;
using System.Security.Claims;
using Satrack.Identity.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Satrack.Application.Constants;
using Microsoft.IdentityModel.Tokens;
using Satrack.Application.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using Satrack.Application.Models.Identity;
using Satrack.Application.Contracts.Identity;

namespace Satrack.Identity.Services
{
	public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;


        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new BadRequestException($"Email y/o contraseña son incorrectos");
            }

            var resultado = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (!resultado.Succeeded)
            {
                throw new BadRequestException($"Email y/o contraseña son incorrectos");
            }

            var token = await GenerateToken(user);
            return new AuthResponse
            {
                Name = user.Name,
                Lastname = user.Lastname,
                Email = user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserId = user.Id,
            };
        }

        public async Task<AuthResponse> Register(RegistrationRequest request)
        {
            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingEmail != null)
            {
                throw new BadRequestException($"El email ya fue tomado por otra cuenta");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
                Name = request.Name,
                EmailConfirmed = true,
                
            };

            var result = await _userManager.CreateAsync(user, request.Password);


            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Operator");

                var token = await GenerateToken(user);

                return new AuthResponse
                {
                    Name = user.Name,
                    Lastname = user.Lastname,
                    Email = user.Email,
                    Username = user.UserName,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    UserId = user.Id,
                };
            }

            throw new BadRequestException("{result.Errors}");

        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id)
            }.Union(userClaims).Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                    signingCredentials: signingCredentials);


            return jwtSecurityToken;
        }

    }
}

