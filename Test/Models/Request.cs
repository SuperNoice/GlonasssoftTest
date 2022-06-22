using System;

namespace Test.Models
{
    public class Request
    {
        public Guid Guid { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserGuid { get; set; }
    }
}
