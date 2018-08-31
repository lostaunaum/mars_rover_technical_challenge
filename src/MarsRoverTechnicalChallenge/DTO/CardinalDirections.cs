using System.ComponentModel;

namespace MarsRoverTechnicalChallenge.DTO
{
    public enum CardinalDirections
    {
        [Description("North")]
        North = 0,

        [Description("South")]
        South = 1,

        [Description("East")]
        East = 2,

        [Description("West")]
        West = 3,
    }
}
