﻿using Roll20Stats.PresentationLayer.DataTransferObjects;

namespace Roll20Stats.ApplicationLayer.Commands.CreateGame
{
    public class CreateGameCommand : ResponseWrapper<CreateGameDto>
    {
        public string Name { get; set; }
    }
}