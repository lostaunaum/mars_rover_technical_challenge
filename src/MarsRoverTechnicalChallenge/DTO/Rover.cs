namespace MarsRoverTechnicalChallenge.DTO
{
    public class Rover
    {
        public int RoverID { get; set; }
        public string RoverName { get; set; }
        public int CurrentX { get; set; } = 0;
        public int CurrentY { get; set; } = 0;
        public CardinalDirections CurrentDirection { get; set; } = CardinalDirections.North;

    }
}
