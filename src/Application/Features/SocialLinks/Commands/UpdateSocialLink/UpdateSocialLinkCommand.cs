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

namespace Application.Features.SocialLinks.Commands.UpdateSocialLink
{
    public class UpdateSocialLinkCommand : IRequest<UpdatedSocialLinkDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string GithubAdress { get; set; }
        public string[] Roles { get; } = { "User" };

        public class UpdateSocialLinkCommandHandler : IRequestHandler<UpdateSocialLinkCommand, UpdatedSocialLinkDto>
        {
            private readonly ISocialLinkRepository _socialLinkRepository;
            private readonly IMapper _mapper;
            private readonly SocialLinkBusinessRules _socialLinkBusinessRules;

            public UpdateSocialLinkCommandHandler(ISocialLinkRepository socialLinkRepository, IMapper mapper, SocialLinkBusinessRules socialLinkBusinessRules)
            {
                _socialLinkRepository = socialLinkRepository;
                _mapper = mapper;
                _socialLinkBusinessRules = socialLinkBusinessRules;
            }

            public async Task<UpdatedSocialLinkDto> Handle(UpdateSocialLinkCommand request, CancellationToken cancellationToken)
            {
                await _socialLinkBusinessRules.UserSocialMediaAddressGithubUrlCanNotBeDuplicated(request.GithubAdress);

                var mappedSocialLinkAddress = _mapper.Map<SocialLink>(request);
                var updatedSocialLinkAddress = await _socialLinkRepository.UpdateAsync(mappedSocialLinkAddress);
                var socialLinkToReturn = _mapper.Map<UpdatedSocialLinkDto>(updatedSocialLinkAddress);
                return socialLinkToReturn;
            }
        }
    }
}
