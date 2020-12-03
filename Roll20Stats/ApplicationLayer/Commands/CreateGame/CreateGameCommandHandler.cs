﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Roll20Stats.InfrastructureLayer.DAL.Context;
using Roll20Stats.InfrastructureLayer.DAL.Entities;
using Roll20Stats.PresentationLayer.DataTransferObjects;

namespace Roll20Stats.ApplicationLayer.Commands.CreateGame
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, ResponseWrapper<CreateGameDto>>
    {
        private readonly IApplicationContext _dbContext;
        private readonly IMapper _mapper;

        public CreateGameCommandHandler(IApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ResponseWrapper<CreateGameDto>> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            if (_dbContext.Games.SingleOrDefault(game => game.Name == request.Name) is { })
            {
                return new ResponseWrapper<CreateGameDto>
                {
                    HasError = true,
                    StatusCode = 409,
                    ErrorMessage = $@"Game with name ""{request.Name}"" already exists."
                };
            }

            var gameToAdd = _mapper.Map<Game>(request);
            await _dbContext.Games.AddAsync(gameToAdd, cancellationToken);
            _dbContext.SaveChanges();
            return _mapper.Map<ResponseWrapper<CreateGameDto>>(gameToAdd);
        }
    }
}