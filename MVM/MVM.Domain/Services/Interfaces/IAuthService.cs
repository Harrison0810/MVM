using MVM.Domain.Models;

namespace MVM.Domain.Services
{
    public interface IAuthService
    {
        MessageModel<AuthModel> Login(AuthModel authModel);
    }
}
