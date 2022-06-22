using System;

namespace Test.Models
{
    public class SignIn
    {
        public int Id { get; set; }
        public User User { get; set; }
        /// <summary>
        /// Use Unix time milliseconds
        /// </summary>
        public long SignAt { get; set; }
    }
}
