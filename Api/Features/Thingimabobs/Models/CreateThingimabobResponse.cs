using System;

namespace Api.Features.Thingimabobs.Models
{
    public class CreateThingimabobResponse
    {
        public Guid Id { get; set; }
        public Guid WidgetId { get; set; }
        public string Name { get; set; }
    }
}