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

namespace Application.Features.SocialLinks.Commands.DeleteSocialLink
{
    public class DeleteSocialLinkCommand : IRequest<DeletedSocialLinkDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public string[] Roles { get; } = { "User" };

        public class DeleteSocialLinkCommandHandler : IRequestHandler<DeleteSocialLinkCommand, DeletedSocialLinkDto>
        {
            private readonly ISocialLinkRepository _socialLinkRepository;
            private readonly IMapper _mapper;
            private readonly SocialLinkBusinessRules _socialLinkBusinessRules;

            public DeleteSocialLinkCommandHandler(ISocialLinkRepository socialLinkRepository, IMapper mapper, SocialLinkBusinessRules socialLinkBusinessRules)
            {
                _socialLinkRepository = socialLinkRepository;
                _mapper = mapper;
                _socialLinkBusinessRules = socialLinkBusinessRules;
            }

            public async Task<DeletedSocialLinkDto> Handle(DeleteSocialLinkCommand request, CancellationToken cancellationToken)
            {

                var mappedSocialLinkAddress = _mapper.Map<SocialLink>(request);
                var deletedSocialLinkAddress = await _socialLinkRepository.DeleteAsync(mappedSocialLinkAddress);
                var socialLinkToReturn = _mapper.Map<DeletedSocialLinkDto>(deletedSocialLinkAddress);
                return socialLinkToReturn;
            }
        }
    }
}
