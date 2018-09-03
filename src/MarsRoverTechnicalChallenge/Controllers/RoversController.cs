using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MarsRoverTechnicalChallenge.Configuration;
using MarsRoverTechnicalChallenge.DTO;
using MarsRoverTechnicalChallenge.service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MarsRoverTechnicalChallenge.Controllers
{
    [Route("api/v1/[controller]")]
    public class RoversController : Controller
    {
        private readonly IRoverRepository _repository;
        private readonly IOptions<ServiceConfiguration> _configuration;
        public RoversController(IRoverRepository repository, IOptions<ServiceConfiguration> configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        // GET /rovers
        // Returns all the rovers within our local dictionary
        [ProducesResponseType(typeof(List<Rover>), 200)]
        [ProducesResponseType(500)]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetAllRovers());
        }

        // GET /rovers/{id}
        // Returns one single rover by ID
        [ProducesResponseType(typeof(List<Rover>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _repository.GetRover(id);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // POST /rovers/{roverID}/{roverName}
        // Creates a new instance of a rover and inserts into our repository
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        [HttpPost("{roverID:int}/{roverName}")]
        public IActionResult Post(int roverID, string roverName)
        {
            //we rather use a validate method so we can send helpful user errors
            var rover = new Rover { RoverID = roverID, RoverName = roverName };

            var result = _repository.CreateRover(rover);

            if (result.Success)
            {
                return Created(_configuration.Value.Host + "api/v1/rovers/" + roverID, result);
            }

            return BadRequest();
        }

        // PUT /rovers/{roverID}/{roverName}
        // Updates an exsting rover with a new name
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpPut("{roverID:int}/{roverName}")]
        public IActionResult Put(int roverID, string roverName)
        {
            //we rather use a validate method so we can send helpful user errors
            var rover = new Rover { RoverID = roverID, RoverName = roverName };

            var result = _repository.UpdateRover(roverID, rover);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        // PUT /rovers/{roverID}/{roverName}
        // Updates the X-axis and Y-axis values for the rover
        // It will move the rover according to the instructions given
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpPut("{roverID:int}/move/{movementInstruction}")]
        public ActionResult MoveRover(int roverID, string movementInstruction)
        {
            var validationResult = IsMovementValid(movementInstruction);

            if (roverID == 0 || !validationResult.Item1)
            {
                return BadRequest(validationResult.Item2);
            }
            
            var result = _repository.MoveRover(roverID, movementInstruction);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        // DELETE /rovers/{roverID}
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(_repository.DeleteRover(id));
        }

        // DELETE /rovers
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpDelete]
        public ActionResult Delete()
        {
            _repository.DeleteAllRovers();

            return Ok();
        }

        private Tuple<bool, string> IsMovementValid(string movement)
        {
            movement = movement.ToUpper();
            var result = new Tuple<bool, string>(true, "");

            if (string.IsNullOrWhiteSpace(movement))
            {
                result = new Tuple<bool, string>(false, "You cant send empty space as a valid movement!");
                return result;
            }

            if (!@Regex.IsMatch(movement, @"^[LRM]+$"))
            {
                result = new Tuple<bool, string>(false, "Only valid movement commands are “L”, “R”, “M”");
                return result;
            }

            return result;
        }
    }
}
