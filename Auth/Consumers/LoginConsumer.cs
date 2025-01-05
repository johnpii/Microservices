using Auth.Interfaces.Services;
using MassTransit;
using SharedLibrary.Models;
using SharedLibrary.ViewModels;

namespace Auth.Consumers
{
    public class LoginConsumer : IConsumer<LoginModel>
    {
        private readonly IUserService _userService;

        public LoginConsumer(IUserService userService)
        {
            _userService = userService;
        }
        public async Task Consume(ConsumeContext<LoginModel> context)
        {
            AuthResponse response;
            User? user = await _userService.FindByEmailAndPassWord(context.Message.Email, context.Message.Password);
            if (user is null)
            {
                response = new AuthResponse
                {
                    IsSuccessful = false,
                    Error = "Пользователь не найден или неправильные данные ! "
                };
                await context.RespondAsync<AuthResponse>(response);
            }
            response = new AuthResponse
            {
                IsSuccessful = true,
                User = user,
            };
            await context.RespondAsync<AuthResponse>(response);
        }
    }
}
