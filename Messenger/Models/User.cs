using System;

namespace Models
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public DateTime Birthday { get; set; }
        public string Password { get; set; }
        public DateTime SignInTime { get; set; }
        public string Token { get; set; }
    }
}