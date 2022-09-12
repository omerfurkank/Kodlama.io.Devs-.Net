using Application.Features.SocialLinks.Dtos;
using Application.Features.SocialLinks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SocialLinks.Commands.CreateSocialLink
{
    public class CreateSocialLinkCommand : IRequest<CreatedSocialLinkDto>, ISecuredRequest
    {
        public int UserId { get; set; }
        public string GithubAdress { get; set; }
        public string[] Roles { get; } = { "User" };

        public class CreateSocialLinkCommandHandler : IRequestHandler<CreateSocialLinkCommand, CreatedSocialLinkDto>
        {
            private readonly ISocialLinkRepository _socialLinkRepository;
            private readonly IMapper _mapper;
            private readonly SocialLinkBusinessRules _socialLinkBusinessRules;

            public CreateSocialLinkCommandHandler(ISocialLinkRepository socialLinkRepository, IMapper mapper, SocialLinkBusinessRules socialLinkBusinessRules)
            {
                _socialLinkRepository = socialLinkRepository;
                _mapper = mapper;
                _socialLinkBusinessRules = socialLinkBusinessRules;
            }

            public async Task<CreatedSocialLinkDto> Handle(CreateSocialLinkCommand request, CancellationToken cancellationToken)
            {
                await _socialLinkBusinessRules.UserSocialMediaAddressGithubUrlCanNotBeDuplicated(request.GithubAdress);

                var mappedSocialLinkAddress = _mapper.Map<SocialLink>(request);
                var createdSocialLinkAddress = await _socialLinkRepository.AddAsync(mappedSocialLinkAddress);
                var socialLinkToReturn = _mapper.Map<CreatedSocialLinkDto>(createdSocialLinkAddress);
                return socialLinkToReturn;
            }
        }
    } 
}
