using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SocialLinks.Commands.CreateSocialLink
{
    public class CreateSocialLinkCommandValidator : AbstractValidator<CreateSocialLinkCommand>
    {
        public CreateSocialLinkCommandValidator()
        {
            RuleFor(p => p.GithubAdress).NotEmpty().NotNull();
        }
    }
}
