using System;
using System.Linq;
using Api.Domain;
using Api.Features.Widgets.Models;
using Nancy;
using Nancy.ModelBinding;

namespace Api.Features.Widgets
{
    public class WidgetNancyModule : NancyModule
    {
        public WidgetNancyModule() : base("/widget")
        {
            Get["/{id:guid}"] = p =>
            {
                var id = (Guid) p.Id;
                using (var db = new DB())
                {
                    var response = db.Widgets
                    .Select(w => new GetWidgetResponse()
                    {
                        Id = w.Id,
                        Name = w.Name
                    })
                    .FirstOrDefault(w => w.Id == id);
                    if (response == null)
                        return HttpStatusCode.NotFound;
                    return Response.AsJson(response);
                }
            };

            Put["/"] = p =>
            {
                var request = this.Bind<CreateWidgetRequest>();
                var widget = new Widget() {Name = request.Name};
                using (var db = new DB())
                {
                    db.Widgets.Add(widget);
                    db.SaveChanges();
                }
                var response =  new CreateWidgetResponse
                {
                    Id = widget.Id
                };
                return Response.AsJson(response);
            };

            Post["/{id:guid}"] = p =>
            {
                var request = this.Bind<UpdateWidgetRequest>();
                using (var db = new DB())
                {
                    var id = (Guid) p.Id;
                    var widget = db.Widgets.FirstOrDefault(w => w.Id == id);
                    if (widget == null)
                        return HttpStatusCode.NotFound;
                    widget.Name = request.Name;
                    db.SaveChanges();
                }
                return HttpStatusCode.OK;
            };
        }
    }
}