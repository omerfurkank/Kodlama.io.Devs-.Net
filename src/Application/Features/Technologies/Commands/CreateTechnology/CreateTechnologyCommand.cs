using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
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

namespace Application.Features.Technologies.Commands.CreateTechnology
{
    public class CreateTechnologyCommand : IRequest<CreatedTechnologyDto>,ISecuredRequest
    {
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }
        public string[] Roles => new string[] { "admin" };

        public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreatedTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _technologyBusinessRules;

            public CreateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<CreatedTechnologyDto> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
            {
               // _technologyBusinessRules.TechnologyNameCanNotBeDuplicatedWhenInserted(request.Name);

                var mappedTechnology = _mapper.Map<Technology>(request);
                var createdTechnology = await _technologyRepository.AddAsync(mappedTechnology);
                var technologyToReturn = _mapper.Map<CreatedTechnologyDto>(createdTechnology);

                return technologyToReturn;
            }
        }

    }
}
