namespace Test.Models
{
    public class StatisticsRequest
    {
        public string UserId { get; set; }
        /// <summary>
        /// Use Unix time in milliseconds
        /// </summary>
        public long From { get; set; }
        /// <summary>
        /// Use Unix time in milliseconds
        /// </summary>
        public long To { get; set; }
    }
}
