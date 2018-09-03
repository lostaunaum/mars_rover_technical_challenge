using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRoverTechnicalChallenge.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System;

namespace FunctionalTests
{
    [TestClass]
    public class RoverControllerTests : BaseTests
    {
        public static string _baseURL;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _baseURL = GetBaseUrl() + "/api/v1/Rovers";
        }

        [TestMethod]
        public async Task GetAllRovers_NoRoversCreated_Equals0_Success()
        {
            //Clean the repository
            var deleteAll = await Utility.DeleteAsync(_baseURL);
            Assert.IsTrue(deleteAll.StatusCode == HttpStatusCode.OK);

            //Get all rovers
            var httpResponseMessage = await Utility.GetAsync(_baseURL);
            Assert.IsTrue(httpResponseMessage.StatusCode == HttpStatusCode.OK);

            //Transform the json into object
            var jsonString = await httpResponseMessage.Content.ReadAsStringAsync();
            var listOfRovers = JsonConvert.DeserializeObject<List<Rover>>(jsonString);

            Assert.IsTrue(listOfRovers.Count == 0);
        }

        [TestMethod]
        public async Task GetRoverByID_NotExistingID_NotFound()
        {
            var httpResponseMessage = await Utility.GetAsync(_baseURL + "/9999");
            Assert.IsTrue(httpResponseMessage.StatusCode == HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task CreateRover_Success()
        {
            var roverID = RandomizeRoverId(9);
            var roverName = "MyRover";

            //Create rover
            var httpResponseMessage = await Utility.PostAsync(_baseURL + "/" + roverID + "/" + roverName);
            Assert.IsTrue(httpResponseMessage.StatusCode == HttpStatusCode.Created);
            
            //Get rover
            var httpResponseMessageGet = await Utility.GetAsync(_baseURL + "/" + roverID);
            Assert.IsTrue(httpResponseMessageGet.StatusCode == HttpStatusCode.OK);

            //Transform Rover
            var jsonString = await httpResponseMessageGet.Content.ReadAsStringAsync();
            var rover = JsonConvert.DeserializeObject<Rover>(jsonString);

            //Assert values are persisted
            Assert.IsTrue(rover.RoverID.ToString() == roverID);
            Assert.IsTrue(rover.RoverName == roverName);
        }
        
        [TestMethod]
        public async Task MoveRover_Success()
        {
            var roverID = RandomizeRoverId(9);
            var roverName = "MyRover";
            var movement = "LLMMMMMMMMMM"; // This will make the rover go south so X-axis should be -10

            //Create The rover
            var httpResponseMessage = await Utility.PostAsync(_baseURL + "/" + roverID + "/" + roverName);
            Assert.IsTrue(httpResponseMessage.StatusCode == HttpStatusCode.Created);

            //Move the rover
            var httpResponseMovementMessage = await Utility.PutAsync(_baseURL + "/" + roverID + "/move/" + movement);
            Assert.IsTrue(httpResponseMovementMessage.StatusCode == HttpStatusCode.OK);

            //Get the rover to see if data is persisted
            var httpResponseMessageGet = await Utility.GetAsync(_baseURL + "/" + roverID);
            Assert.IsTrue(httpResponseMessageGet.StatusCode == HttpStatusCode.OK);

            var jsonString = await httpResponseMessageGet.Content.ReadAsStringAsync();
            var rover = JsonConvert.DeserializeObject<Rover>(jsonString);

            Assert.IsTrue(rover.RoverID.ToString() == roverID);
            Assert.IsTrue(rover.RoverName == roverName);
            Assert.IsTrue(rover.CurrentX == -10);
            Assert.IsTrue(rover.CurrentDirection == CardinalDirections.South);
        }


        [TestMethod]
        public async Task Movement_IsValidated()
        {
            var roverID = RandomizeRoverId(9);
            var roverName = "MyRover";
            var movement = "ZZZZERS"; // this should fail the validation

            //Create The rover
            var httpResponseMessage = await Utility.PostAsync(_baseURL + "/" + roverID + "/" + roverName);
            Assert.IsTrue(httpResponseMessage.StatusCode == HttpStatusCode.Created);

            //Move the rover
            var httpResponseMovementMessage = await Utility.PutAsync(_baseURL + "/" + roverID + "/move/" + movement);
            Assert.IsTrue(httpResponseMovementMessage.StatusCode == HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task CreateTenRovers_Success()
        {
            //Clean the repository
            var deleteAll = await Utility.DeleteAsync(_baseURL);
            Assert.IsTrue(deleteAll.StatusCode == HttpStatusCode.OK);

            //Create all the rovers
            for (var i = 0; i < 10; i++)
            {
                var roverID = RandomizeRoverId(9);
                var roverName = "MyRover" + roverID;
                
                var httpResponseMessage = await Utility.PostAsync(_baseURL + "/" + roverID + "/" + roverName);
                Assert.IsTrue(httpResponseMessage.StatusCode == HttpStatusCode.Created);
            }
            
            //Get all the rovers
            var httpResponseMessageGet = await Utility.GetAsync(_baseURL);
            Assert.IsTrue(httpResponseMessageGet.StatusCode == HttpStatusCode.OK);

            var jsonString = await httpResponseMessageGet.Content.ReadAsStringAsync();
            var rovers = JsonConvert.DeserializeObject<List<Rover>>(jsonString);

            Assert.IsTrue(rovers.Count == 10);
        }

        public static string RandomizeRoverId(int length)
        {
            var chars = "0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }
    }
}
