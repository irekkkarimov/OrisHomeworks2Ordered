using MediatR;
using Microsoft.AspNetCore.Identity;
using TeamHost.Domain.Entities.UserEntities;

namespace TeamHost.Application.Features.Users.Commands.PostUserLogout;

/// <summary>
/// Команда на Logout юзера
/// </summary>
public class UserLogoutCommand : IRequest
{
}

/// <summary>
/// Обработчик команды на Logout юзера
/// </summary>
internal class UserLogoutCommandHandler : IRequestHandler<UserLogoutCommand>
{
    private readonly SignInManager<User> _signInManager;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="signInManager">Сервис для авторизаций</param>
    public UserLogoutCommandHandler(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }

    /// <inheritdoc/>
    public async Task Handle(UserLogoutCommand request, CancellationToken cancellationToken)
    {
        await _signInManager.SignOutAsync();
    }
}