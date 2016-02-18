using System;

namespace Api.Features.Thingimabobs.Models
{
    public class UpdateThingimabobRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}