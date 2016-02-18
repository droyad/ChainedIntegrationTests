using Api.Features.Widgets.Models;
using FluentAssertions;
using NUnit.Framework;

namespace IntegrationTests.Widgets
{
    public class AddWidgetScenario : BddTest
    {
        private const string WidgetName = "Foo";
        public CreateWidgetResponse NewWidget { get; private set; }

        public void WhenAWidgetIsAdded()
        {
            var request = new CreateWidgetRequest() { Name = WidgetName };
            NewWidget = Browser.Put<CreateWidgetResponse>("/widget", request);
        }


        public void ThenItCanBeRetrievedAgain()
        {
            var result = Browser.Get($"/widget/{NewWidget.Id}").AsJson<GetWidgetResponse>();
            result.Name.Should().Be(WidgetName);
        }

        [Test]
        public override void Execute()
        {
            base.Execute();
        }
    }
}