using System;

namespace Models
{
    public class Dialogue : IEntity
    {
        public int UserIdFirst { get; set; }
        public int UserIdSecond { get; set; }
        public DateTime Created { get; set; }
        public int Id { get; set; }
    }
}