using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SocialLinks.Rules
{
    public class SocialLinkBusinessRules
    {
        private readonly ISocialLinkRepository _socialLinkRepository;

        public SocialLinkBusinessRules(ISocialLinkRepository socialLinkRepository)
        {
            _socialLinkRepository = socialLinkRepository;
        }

        public async Task UserSocialMediaAddressGithubUrlCanNotBeDuplicated(string requestGithubUrl)
        {
            var userSocialMediaAddress = await _socialLinkRepository.GetAsync(x => x.GithubAdress == requestGithubUrl);

            if (userSocialMediaAddress != null)
                throw new BusinessException("this github adress already exists");
        }
    }
}
