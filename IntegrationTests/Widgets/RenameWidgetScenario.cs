using System;
using Api.Features.Widgets.Models;
using FluentAssertions;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;

namespace IntegrationTests.Widgets
{
    public class RenameWidgetScenario : BddTest
    {
        private Guid _widgetId;
        private BrowserResponse _response;

        public void GivenAWidgetExists()
        {
            _widgetId =  TestHelpers.Given<AddWidgetScenario>()
                .NewWidget
                .Id;
        }

        public void WhenTheNameOfTheWidgetIsChanged()
        {
            _response = Browser.Post($"/widget/{_widgetId}", new UpdateWidgetRequest() {Name = "Zanzibar"});
        }

        public void ThenOkWasReturned()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        public void AndThenTheWidgetHasTheNewNameWhenRetrieved()
        {
            Browser.Get($"/widget/{_widgetId}").AsJson<GetWidgetResponse>().Name.Should().Be("Zanzibar");
        }


        [Test]
        public override void Execute()
        {
            base.Execute();
        }
    }
}