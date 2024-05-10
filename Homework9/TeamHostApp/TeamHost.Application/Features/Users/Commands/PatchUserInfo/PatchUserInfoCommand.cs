using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeamHost.Application.DTOs.UserInfo;
using TeamHost.Application.Features.Users.Queries;
using TeamHost.Application.Interfaces.Repositories;
using TeamHost.Domain.Entities.GameEntities;
using TeamHost.Domain.Entities.UserEntities;

namespace TeamHost.Application.Features.Users.Commands.PatchUserInfo;

public class PatchUserInfoCommand : IRequest<GetUserInfoResponse>
{
    public PatchUserInfoCommand(UpdateUserInfoRequest request)
    {
        Request = request;
    }

    public UpdateUserInfoRequest Request { get; set; }
}

internal class PatchUserInfoCommandHandler : IRequestHandler<PatchUserInfoCommand, GetUserInfoResponse>
{
    private readonly IGenericRepository<UserInfo> _userInfoRepository;
    private readonly IGenericRepository<Country> _countryRepository;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;

    public PatchUserInfoCommandHandler(IGenericRepository<UserInfo> userInfoRepository,
        SignInManager<User> signInManager, IGenericRepository<Country> countryRepository, IMapper mapper)
    {
        _userInfoRepository = userInfoRepository;
        _signInManager = signInManager;
        _countryRepository = countryRepository;
        _mapper = mapper;
    }

    public async Task<GetUserInfoResponse> Handle(PatchUserInfoCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var userId = _signInManager.Context.User.Claims
            .FirstOrDefault(i => i.Type.Equals(ClaimTypes.NameIdentifier))?
            .Value;

        if (userId is null)
            throw new NullReferenceException("User not found");

        var userInfo = await _userInfoRepository.Entities
            .Include(i => i.Country)
            .FirstOrDefaultAsync(i => i.UserId == Guid.Parse(userId), cancellationToken: cancellationToken);

        if (userInfo is null)
            throw new NullReferenceException("User info is null");

        if (!string.IsNullOrEmpty(request.FirstName) && !string.IsNullOrWhiteSpace(request.FirstName))
            userInfo.FirstName = request.FirstName;

        if (!string.IsNullOrEmpty(request.Lastname) && !string.IsNullOrWhiteSpace(request.Lastname))
            userInfo.LastName = request.Lastname;

        userInfo.Bio = request.Bio ?? userInfo.Bio;

        if (request.Country is not null)
        {
            Console.WriteLine(request.Country);
            Console.WriteLine((int)request.Country);
            var country = await _countryRepository.Entities
                .FirstOrDefaultAsync(i => i.Id == (int)request.Country, cancellationToken);

            if (country is not null)
                userInfo.Country = country;
        }

        if (request.BirthDate is not null)
        {
            var days = request.BirthDate.Value.Day;
            var months = request.BirthDate.Value.Month;
            var years = request.BirthDate.Value.Year;

            var newDate = new DateTime(years, months, days, 0, 0 ,0, DateTimeKind.Utc);
            userInfo.BirthDate = newDate;
        }

        await _userInfoRepository.UpdateAsync(userInfo);
        return _mapper.Map<GetUserInfoResponse>(userInfo);
    }
}