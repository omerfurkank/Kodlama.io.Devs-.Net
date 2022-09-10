using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Queries.GetByIdTechnology
{
    public class GetByIdTechnologyQuery : IRequest<TechnologyGetByIdDto>
    {
        public int Id { get; set; }

        public class GetByIdTechnologyQueryHandler : IRequestHandler<GetByIdTechnologyQuery, TechnologyGetByIdDto>
        {
            ITechnologyRepository _technologyRepository;
            IMapper _mapper;
            TechnologyBusinessRules _technologyBusinessRules;

            public GetByIdTechnologyQueryHandler(ITechnologyRepository technologyRepository,
                IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<TechnologyGetByIdDto> Handle(GetByIdTechnologyQuery request, CancellationToken cancellationToken)
            {
                //not exist programming language name, i will fix it soon
                var programmingLanguage = await _technologyRepository.GetAsync(p => p.Id == request.Id);
                _technologyBusinessRules.TechnologyShouldExistWhenRequested(programmingLanguage);
                var programmingLanguageGetByIdDto = _mapper.Map<TechnologyGetByIdDto>(programmingLanguage);

                return programmingLanguageGetByIdDto;
            }
        }
    }
}
