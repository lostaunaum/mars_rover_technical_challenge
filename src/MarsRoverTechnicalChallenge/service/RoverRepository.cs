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

        public ResponseMessage CreateRover(Rover rover)
        {
            var result = new ResponseMessage();
            
            try
            {
                if (_collection.ContainsKey(rover.RoverID))
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

        public ResponseMessage UpdateRover(int roverID, Rover rover)
        {
            var result = new ResponseMessage();
            
            try
            {
                if (_collection.ContainsKey(roverID))
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

        public ResponseMessage MoveRover(int roverID, string movementInstruction)
        {
            var result = new ResponseMessage();
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

        public ResponseMessage DeleteRover(int roverID)
        {
            var result = new ResponseMessage();

            try
            {
                if (_collection.ContainsKey(roverID))
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

        public void DeleteAllRovers()
        {
            _collection.Clear();
        }

        public Rover GetRover(int roverID)
        {
            if (_collection.ContainsKey(roverID))
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
