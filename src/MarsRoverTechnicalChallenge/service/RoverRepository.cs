using MarsRoverTechnicalChallenge.service.Interface;
using MarsRoverTechnicalChallenge.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace MarsRoverTechnicalChallenge.service
{
    public class RoverRepository : IRoverRepository
    {
        private readonly Dictionary<int, Rover> _collection;

        public RoverRepository()
        {
            _collection = new Dictionary<int, Rover>();
        }

        public ReponseMessage CreateRover(Rover rover)
        {
            var result = new ReponseMessage();
            
            try
            {
                if (_collection[rover.RoverID] != null)
                {
                    result.ErrorMessage = "RoverId is already in use. Please select a different RoverID";
                    result.StatusCode = HttpStatusCode.BadRequest;
                    result.Success = false;

                    return result;
                }

                _collection[rover.RoverID] = rover;

                result.StatusCode = HttpStatusCode.Created;
                result.Success = true;

                return result;
            }
            catch (System.Exception ex)
            {                
                result.ErrorMessage = ex.Message;
                result.StatusCode = HttpStatusCode.InternalServerError;
                result.Success = false;

                return result;
            }
        }

        public ReponseMessage UpdateRover(int roverID, Rover rover)
        {
            var result = new ReponseMessage();
            
            try
            {
                if (_collection[roverID] != null)
                {
                    _collection[rover.RoverID] = rover;
                }
                else
                {
                    result.ErrorMessage = "Rover not found";
                    result.StatusCode = HttpStatusCode.NotFound;
                    result.Success = false;

                    return result;
                }

                result.StatusCode = HttpStatusCode.OK;
                result.Success = true;

                return result;
            }
            catch (System.Exception ex)
            {

                result.ErrorMessage = ex.Message;
                result.StatusCode = HttpStatusCode.InternalServerError;
                result.Success = false;

                return result;
            }
        }

        public ReponseMessage MoveRover(int roverID, string movementInstruction)
        {
            var result = new ReponseMessage();
            var rover = GetRover(roverID);

            try
            {
                var commands = movementInstruction.ToCharArray().Select(c => c.ToString()).ToList();

                for (int i = 0; i < commands.Count; i++)
                {
                    CalculateMovement(rover, commands[i]);
                }

                return UpdateRover(roverID, rover);
            }
            catch (System.Exception ex)
            {

                result.ErrorMessage = ex.Message;
                result.StatusCode = HttpStatusCode.InternalServerError;
                result.Success = false;

                return result;
            }
        }

        public ReponseMessage DeleteRover(int roverID)
        {
            var result = new ReponseMessage();

            try
            {
                if (_collection[roverID] != null)
                {
                    _collection.Remove(roverID);
                }
                else
                {
                    result.ErrorMessage = "Rover not found";
                    result.StatusCode = HttpStatusCode.NotFound;
                    result.Success = false;

                    return result;
                }

                result.StatusCode = HttpStatusCode.OK;
                result.Success = true;

                return result;
            }
            catch (System.Exception ex)
            {

                result.ErrorMessage = ex.Message;
                result.StatusCode = HttpStatusCode.InternalServerError;
                result.Success = false;

                return result;
            }
        }

        public Rover GetRover(int roverID)
        {
            var result = new ReponseMessage();

            if (_collection[roverID] != null)
            {
                return _collection[roverID];
            }
            else
            {
                return null;
            }
        }

        public List<Rover> GetAllRovers()
        {
            return _collection.Select(r => r.Value).ToList();
        }

        private void CalculateMovement(Rover rover, string directionCommand)
        {
            switch (rover.CurrentDirection)
            {
                case CardinalDirections.North:
                    if (directionCommand == "L")
                    {
                        rover.CurrentDirection = CardinalDirections.West;
                    }
                    else if (directionCommand == "R")
                    {
                        rover.CurrentDirection = CardinalDirections.East;
                    }
                    else
                    {
                        rover.CurrentX += 1;
                    }
                    break;

                case CardinalDirections.South:
                    if (directionCommand == "L")
                    {
                        rover.CurrentDirection = CardinalDirections.East;
                    }
                    else if (directionCommand == "R")
                    {
                        rover.CurrentDirection = CardinalDirections.West;
                    }
                    else
                    {
                        rover.CurrentX -= 1;
                    }
                    break;

                case CardinalDirections.East:
                    if (directionCommand == "L")
                    {
                        rover.CurrentDirection = CardinalDirections.North;
                    }
                    else if (directionCommand == "R")
                    {
                        rover.CurrentDirection = CardinalDirections.South;
                    }
                    else
                    {
                        rover.CurrentY -= 1;
                    }
                    break;

                case CardinalDirections.West:
                    if (directionCommand == "L")
                    {
                        rover.CurrentDirection = CardinalDirections.South;
                    }
                    else if (directionCommand == "R")
                    {
                        rover.CurrentDirection = CardinalDirections.North;
                    }
                    else
                    {
                        rover.CurrentY += 1;
                    }
                    break;
            }
        }
    }
}
