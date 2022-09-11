using Application.Features.Auths.Rules;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands.Login
{
    public class LoginCommand : IRequest<AccessToken>
    {
        public UserForLoginDto UserForLoginDto { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, AccessToken>
        {
            private readonly IUserRepository _userRepository;
            private readonly UserBusinessRules _userBusinessRules;
            private readonly ITokenHelper _tokenHelper;

            public LoginCommandHandler(IUserRepository userRepository, UserBusinessRules userBusinessRules, ITokenHelper tokenHelper)
            {
                _userRepository = userRepository;
                _userBusinessRules = userBusinessRules;
                _tokenHelper = tokenHelper;
            }

            public async Task<AccessToken> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAsync(x => x.Email == request.UserForLoginDto.Email,
                    include: u => u.Include(u => u.UserOperationClaims).ThenInclude(o => o.OperationClaim));

                var operationClaims = new List<OperationClaim>();

                foreach (var userOperationClaim in user.UserOperationClaims)
                    operationClaims.Add(userOperationClaim.OperationClaim);

                var accessToken = _tokenHelper.CreateToken(user,operationClaims);

                return accessToken;
            }
        }
    }
}
