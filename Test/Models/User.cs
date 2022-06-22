using System;

namespace Test.Models
{
    public class User
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }

        public User()
        {
            Guid = Guid.NewGuid();
        }
    }
}
