using MediatR;
using Microsoft.AspNetCore.Identity;
using TeamHost.Application.DTOs.User;
using TeamHost.Application.Interfaces.Repositories;
using TeamHost.Domain.Entities.UserEntities;

namespace TeamHost.Application.Features.Users.Commands.PostUserRegister;

/// <summary>
/// Команда на регистрацию юзера
/// </summary>
public class UserRegisterCommand : IRequest<bool>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос с информацией о регистрируемом пользователе</param>
    public UserRegisterCommand(UserRegisterDto request)
    {
        Request = request;
    }

    /// <summary>
    /// Запрос с информацией о регистрируемом пользователе
    /// </summary>
    public UserRegisterDto Request { get; set; }
}

/// <summary>
/// Обработчик команды на регистрацию юзера
/// </summary>
internal class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, bool>
{
    private readonly SignInManager<User> _signInManager;
    private readonly IGenericRepository<UserInfo> _userInfoRepository;
    private readonly IGenericRepository<Domain.Entities.WalletEntities.Wallet> _walletRepository;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="signInManager">Сервис для авторизаций</param>
    /// <param name="userInfoRepository">Репозиторий для инфы юзера</param>
    /// <param name="walletRepository">Репозиторий для кошельков</param>
    public UserRegisterCommandHandler(SignInManager<User> signInManager,
        IGenericRepository<UserInfo> userInfoRepository, IGenericRepository<Domain.Entities.WalletEntities.Wallet> walletRepository)
    {
        _signInManager = signInManager;
        _userInfoRepository = userInfoRepository;
        _walletRepository = walletRepository;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        var newUser = new User
        {
            UserName = request.Request.Username,
            Email = request.Request.Email
        };

        var result = await _signInManager.UserManager.CreateAsync(newUser, request.Request.Password);
        var newUserInfo = new UserInfo
        {
            User = newUser
        };

        var newUserWallet = new Domain.Entities.WalletEntities.Wallet 
        {
            User = newUser,
            Money = 0
        };

        foreach (var error in result.Errors)
        {
            Console.WriteLine(error.Description);
        }

        await _userInfoRepository.AddAsync(newUserInfo);
        await _walletRepository.AddAsync(newUserWallet);
        return result.Succeeded;
    }
}