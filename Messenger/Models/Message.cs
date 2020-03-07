using System;

namespace Models
{
    public class Message : IEntity
    {
        public int DialogueId { get; set; }
        public int UserIdFrom { get; set; }
        public string Data { get; set; }
        public DateTime MsgTime { get; set; }
        public int Id { get; set; }
    }
}