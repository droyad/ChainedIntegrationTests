using System;

namespace Api.Features.Thingimabobs.Models
{
    public class CreateThingimabobRequest
    {
        public Guid WidgetId { get; set; }
        public string Name { get; set; }
    }
}