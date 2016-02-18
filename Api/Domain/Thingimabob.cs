using System;

namespace Api.Domain
{
    public class Thingimabob
    {
        public Guid Id { get; set; }
        public virtual Widget Widget { get; set; }
        public string Name { get; set; }
    }
}