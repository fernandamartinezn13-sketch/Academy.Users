using Academy.Users.Application.Users.Commands.UserRegister;
using Academy.Users.Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Users.Application.Users.Commands.UserManagement;

public class ManagementUserCommandHandler : IRequestHandler<ManagementUserCommand, Result<GetManagementUserCommandResponse>>
{
public Task<Result<GetManagementUserCommandResponse>> Handle(ManagementUserCommand request, CancellationToken cancellationToken)
{
    throw new NotImplementedException();
}
}
