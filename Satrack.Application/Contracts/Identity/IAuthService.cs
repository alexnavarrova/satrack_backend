using Satrack.Application.Models.Identity;

namespace Satrack.Application.Contracts.Identity
{
	public interface IAuthService
	{
        Task<AuthResponse> Login(AuthRequest request);
        Task<AuthResponse> Register(RegistrationRequest request);
    }
}

