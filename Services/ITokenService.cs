using StaffSphere.Models;

namespace StaffSphere.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}