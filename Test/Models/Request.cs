using System;
using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class Request
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; }
    }
}
