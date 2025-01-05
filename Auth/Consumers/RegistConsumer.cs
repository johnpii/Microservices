using Auth.Interfaces.Services;
using Auth.Utilities;
using MassTransit;
using SharedLibrary.Models;
using SharedLibrary.ViewModels;

namespace Auth.Consumers
{
    public class RegistConsumer : IConsumer<RegistModel>
    {
        private readonly IUserService _userService;

        public RegistConsumer(IUserService userService)
        {
            _userService = userService;
        }
        public async Task Consume(ConsumeContext<RegistModel> context)
        {
            AuthResponse response;
            if (!_userService.ExistByEmail(context.Message.Email))
            {
                User user = new User();
                user.Email = context.Message.Email;
                user.Username = context.Message.Username;
                user.Password = PasswordEncryption.EncryptPassword(context.Message.Password);
                user.Role = "user";
                _userService.AddUser(user);

                response = new AuthResponse
                {
                    IsSuccessful = true
                };

                await context.RespondAsync<AuthResponse>(response);
            }
            else
            {
                response = new AuthResponse
                {
                    IsSuccessful = false,
                    Error = "Пользователь с такой почтой уже существует ! "
                };
                await context.RespondAsync<AuthResponse>(response);
            }
        }
    }
}
