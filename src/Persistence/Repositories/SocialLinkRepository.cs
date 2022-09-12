using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class SocialLinkRepository : EfRepositoryBase<SocialLink, BaseDbContext>, ISocialLinkRepository
    {
        public SocialLinkRepository(BaseDbContext context) : base(context)
        {
        }

    }
}
