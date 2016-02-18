using System;
using Api.Features.Thingimabobs.Models;
using Api.Features.Widgets.Models;
using FluentAssertions;
using IntegrationTests.Widgets;
using NUnit.Framework;

namespace IntegrationTests.Thingimabobs
{
    public class AddThingimabobScenario : BddTest
    {
        private Guid _widgetId;
        public CreateThingimabobResponse NewThingimabob { get; private set; }

        public void GivenAWidgetExists()
        {
            _widgetId = TestHelpers.Given<AddWidgetScenario>()
                .NewWidget
                .Id;
        }

        public void WhenAThingimabobIsAdded()
        {
            var request = new CreateThingimabobRequest() { WidgetId = _widgetId, Name = "Fred" };
            NewThingimabob = Browser.Put<CreateThingimabobResponse>("/thingimabob", request);
        }


        public void ThenItCanBeRetrievedAgain()
        {
            var result = Browser.Get($"/thingimabob/{NewThingimabob.Id}").AsJson<GetThingimabobResponse>();
            result.Name.Should().Be("Fred");
        }

        public void AndThenWePurposefullyFailTheTest()
        {
            Assert.Fail();
        }

        [Test]
        public override void Execute()
        {
            base.Execute();
        }
    }
}