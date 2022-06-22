using System;
using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class User
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
    }
}
