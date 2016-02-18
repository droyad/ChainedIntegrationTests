using System;
using Api.Features.Thingimabobs.Models;
using FluentAssertions;
using IntegrationTests.Thingimabobs;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;

namespace IntegrationTests.Thingimabobs
{
    public class RenameThingimabobScenario : BddTest
    {
        private Guid _widgetId;
        private BrowserResponse _response;

        public void GivenAThingimabobExists()
        {
            _widgetId =  TestHelpers.Given<AddThingimabobScenario>()
                .NewThingimabob
                .Id;
        }

        public void WhenTheNameOfTheThingimabobIsChanged()
        {
            _response = Browser.Post($"/thingimabob/{_widgetId}", new UpdateThingimabobRequest() {Name = "Whatchyoucallit"});
        }

        public void ThenOkWasReturned()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        public void AndThenTheThingimabobHasTheNewNameWhenRetrieved()
        {
            Browser.Get($"/thingimabob/{_widgetId}").AsJson<GetThingimabobResponse>().Name.Should().Be("Whatchyoucallit");
        }


        [Test]
        public override void Execute()
        {
            base.Execute();
        }
    }
}