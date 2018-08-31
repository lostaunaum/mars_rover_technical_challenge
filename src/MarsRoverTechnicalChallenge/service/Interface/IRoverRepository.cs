using MarsRoverTechnicalChallenge.DTO;
using System;
using System.Collections.Generic;

namespace MarsRoverTechnicalChallenge.service.Interface
{
    public interface IRoverRepository
    {
        ResponseMessage CreateRover(Rover rover);
        ResponseMessage UpdateRover(int roverID, Rover rover);
        ResponseMessage DeleteRover(int roverID);
        Rover GetRover(int roverID);
        List<Rover> GetAllRovers();
        ResponseMessage MoveRover(int roverID, string movementInstruction);
    }
}
