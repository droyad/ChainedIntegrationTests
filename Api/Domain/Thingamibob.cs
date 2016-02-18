using System;

namespace Api.Domain
{
    public class Thingamibob
    {
        public Thingamibob()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}