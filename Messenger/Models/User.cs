using System;

namespace Models
{
    public class User :IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public  Type { get; set; }
    }
}