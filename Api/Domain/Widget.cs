using System;
using System.Collections.Generic;

namespace Api.Domain
{
    public class Widget
    {
        public Widget()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual List<Thingamibob> Thingamibobs { get; set; }
    }
}