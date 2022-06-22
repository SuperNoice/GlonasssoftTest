using System;

namespace Test.Models
{
    public class Request
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        /// <summary>
        /// Use Unix time in milliseconds
        /// </summary>
        public long CreatedAt { get; set; }
        /// <summary>
        /// Use Unix time in milliseconds
        /// </summary>
        public long From { get; set; }
        /// <summary>
        /// Use Unix time in milliseconds
        /// </summary>
        public long To { get; set; }
        public User User { get; set; }
    }
}
