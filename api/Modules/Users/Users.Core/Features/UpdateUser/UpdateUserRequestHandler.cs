using AutoMapper;
using MediatR;
using Shared.Core.Entities;
using Shared.Core.Exceptions;
using System.Data;
using Users.Core.Abstractions;
using Users.Core.Model;
using static QRCoder.PayloadGenerator;

namespace Users.Core.Features.UpdateUser;
public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, UpdateUserResponse>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public UpdateUserRequestHandler(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }
    public async Task<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetById(request.Id, cancellationToken);

        if (user is null) throw new HttpNotFoundException();

        if (request.User.Role > request.Role && request.User.Role > user.Role)
            throw new HttpForbiddenException();

        user.Update(
            username: request.Username,
            email: request.Email,
            currentPassword: request.CurrentPassword,
            newPassword: request.NewPassword,
            role: request.Role,
            companyIds: request.CompanyIds
        );

        await _usersRepository.Update(user, cancellationToken);

        return new UpdateUserResponse
        {
            User = _mapper.Map<UserModel>(user)
        };
    }
}
