using MediatR;
using Microsoft.AspNetCore.Identity;
using TeamHost.Application.DTOs.User;
using TeamHost.Domain.Entities.UserEntities;

namespace TeamHost.Application.Features.Users.Commands.PostUserLogin;

/// <summary>
/// Команда на Login юзера
/// </summary>
public class UserLoginCommand : IRequest<bool>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос с информацией о авторизуемом пользователе</param>
    public UserLoginCommand(UserLoginDto request)
    {
        Request = request;
    }

    /// <summary>
    /// Запрос с информацией о авторизуемом пользователе
    /// </summary>
    public UserLoginDto Request { get; set; }
}

/// <summary>
/// Обработчик команды на Login юзера
/// </summary>
internal class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, bool>
{
    private readonly SignInManager<User> _signInManager;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="signInManager">Сервис для авторизаций</param>
    public UserLoginCommandHandler(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(UserLoginCommand command, CancellationToken cancellationToken)
    {
        var userByEmail = await _signInManager.UserManager.FindByEmailAsync(command.Request.Email);

        if (userByEmail is null)
            throw new ArgumentException("User by given email not found");

        var isPasswordCorrect =
            await _signInManager.PasswordSignInAsync(userByEmail, command.Request.Password, true, false);

        Console.WriteLine(isPasswordCorrect);
        
        return isPasswordCorrect.Succeeded;
    }
}