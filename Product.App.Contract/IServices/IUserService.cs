using Product.App.Contract.Dto;

namespace Product.App.Contract.IServices
{
    public interface IUserService
    {
        string Login(LoginDto dto);

        Task<string> Register(RegisterDto dto);
    }
}
