﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.ParticipantAnswerQuery
{
    public class GetAllParticipantAnswerQuery : IRequest<IEnumerable<ParticipantAnswerResponse>>
    {
        public class GetAllParticipantAnswerQueryHandler : IRequestHandler<GetAllParticipantAnswerQuery, IEnumerable<ParticipantAnswerResponse>>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public GetAllParticipantAnswerQueryHandler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<ParticipantAnswerResponse>> Handle(GetAllParticipantAnswerQuery query, CancellationToken cancellationToken)
            {
                var items = await _context.ParticipantAnswers.ToListAsync();

                if (items == null || !items.Any())
                {
                    return default;
                }

                return _mapper.Map<IEnumerable<ParticipantAnswerResponse>>(items.AsReadOnly());
            }
        }
    }
}