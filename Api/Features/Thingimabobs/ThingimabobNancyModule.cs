using System;
using System.Linq;
using Api.Domain;
using Api.Features.Thingimabobs.Models;
using Nancy;
using Nancy.ModelBinding;

namespace Api.Features.Thingimabobs
{
    public class ThingimabobNancyModule : NancyModule
    {
        public ThingimabobNancyModule() : base("thingimabob")
        {
            Get["/{id:guid}"] = p =>
            {
                var id = (Guid)p.Id;
                using (var db = new DB())
                {
                    var response = db.Thingimabobs
                    .Select(t => new GetThingimabobResponse()
                    {
                        Id = t.Id,
                        WidgetId = t.Widget.Id,
                        Name = t.Name
                    })
                    .FirstOrDefault(w => w.Id == id);
                    if (response == null)
                        return HttpStatusCode.NotFound;
                    return Response.AsJson(response);
                }
            };

            Put["/"] = p =>
            {
                var request = this.Bind<CreateThingimabobRequest>();
                using (var db = new DB())
                {
                    var thingimabob = new Domain.Thingimabob()
                    {
                        Widget = db.Widgets.First(w => w.Id == request.WidgetId),
                        Name = request.Name
                    };

                    db.Thingimabobs.Add(thingimabob);
                    db.SaveChanges();
                    var response = new CreateThingimabobResponse
                    {
                        Id = thingimabob.Id
                    };
                    return Response.AsJson(response);
                }
            };

            Post["/{id:guid}"] = p =>
            {
                var request = this.Bind<UpdateThingimabobRequest>();
                using (var db = new DB())
                {
                    var id = (Guid)p.Id;
                    var thingimabob = db.Thingimabobs.FirstOrDefault(w => w.Id == id);
                    if (thingimabob == null)
                        return HttpStatusCode.NotFound;
                    thingimabob.Name = request.Name;
                    db.SaveChanges();
                }
                return HttpStatusCode.OK;
            };
        }
    }
}