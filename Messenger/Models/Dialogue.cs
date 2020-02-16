using System;

namespace Models
{
    public class Dialogue : IEntity
    {
        public int Id { get; set; }
        public int UserIdFirst { get; set; }
        public int UserIdSecond { get; set; }
        public DateTime Created { get; set; }

    }
}