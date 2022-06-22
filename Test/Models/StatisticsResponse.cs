using System;

namespace Test.Models
{
    public class StatisticsResponse
    {
        public Guid Query { get; set; }
        public int Percent { get; set; }
        public UserStatistics Result { get; set; }
    }
}
