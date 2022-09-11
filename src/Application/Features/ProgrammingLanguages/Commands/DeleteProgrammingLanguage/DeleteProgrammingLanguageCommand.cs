using Application.Features.ProgrammingLanguages.Dtos;
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

namespace Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage
{
    public class DeleteProgrammingLanguageCommand : IRequest<CreatedProgrammingLanguageDto>,ISecuredRequest
    {
        public int Id { get; set; }
        public string[] Roles => new string[] { "admin" };

        public class DeleteProgrammingLanguageCommandHandler : IRequestHandler<DeleteProgrammingLanguageCommand, CreatedProgrammingLanguageDto>
        {
            IProgrammingLanguageRepository _programmingLanguageRepository;
            IMapper _mapper;

            public DeleteProgrammingLanguageCommandHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper)
            {
                _programmingLanguageRepository = programmingLanguageRepository;
                _mapper = mapper;
            }

            public async Task<CreatedProgrammingLanguageDto> Handle(DeleteProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                var mappedProgrammingLanguage = _mapper.Map<ProgrammingLanguage>(request);
                var deletedProgrammingLanguage = await _programmingLanguageRepository.DeleteAsync(mappedProgrammingLanguage);
                var programmingLanguageToReturn = _mapper.Map<CreatedProgrammingLanguageDto>(deletedProgrammingLanguage);

                return programmingLanguageToReturn;
            }
        }

    }
}
