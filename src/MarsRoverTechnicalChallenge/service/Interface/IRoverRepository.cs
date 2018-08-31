using MarsRoverTechnicalChallenge.DTO;
using System;
using System.Collections.Generic;

namespace MarsRoverTechnicalChallenge.service.Interface
{
    public interface IRoverRepository
    {
        ReponseMessage CreateRover(Rover rover);
        ReponseMessage UpdateRover(int roverID, Rover rover);
        ReponseMessage DeleteRover(int roverID);
        Rover GetRover(int roverID);
        List<Rover> GetAllRovers();
        ReponseMessage MoveRover(int roverID, string movementInstruction);
    }
}
