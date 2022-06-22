using System;

namespace Test.Models
{
    public class SignIn
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime SignAt { get; set; }
    }
}
