using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Rules
{
    public class UserBusinessRules
    {
        private readonly IUserRepository userRepository;

        public UserBusinessRules(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task UserEmailCannotBeDuplicatedWhenInserted(string email)
        {
            var user = await userRepository.GetAsync(u => u.Email.ToLower() == email.ToLower());
            if (user != null) throw new BusinessException("An account already registered using this mail");
        }
    }
}
