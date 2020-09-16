using DataFactory.Database.Entities;
using DataFactory.Database.Repository.Contracts;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace UserStories.Features.Steps.Database.ExampleRepository
{
    [Binding]
    public class ExampleRepositorySteps : StepsBase
    {
        private readonly IExampleRepositoryTable exampleRepositoryTable;

        private ExampleTableEntity exampleTable;

        public ExampleRepositorySteps(IExampleRepositoryTable exampleRepositoryTable)
        {
            this.exampleRepositoryTable = exampleRepositoryTable;
        }

        [Given(@"The user removes all data from example repository")]
        public void TheUserRemovesAllDataFromExampleRepository()
        {
            exampleRepositoryTable.DeleteAll();
        }

        [When(@"The user performs a new insert into example repository with the following properties")]
        public void TheUserPerformsANewInsertIntoExampleRepositoryWithTheFollowingProperties(Table table)
        {
            exampleTable = table.CreateInstance<ExampleTableEntity>();
            exampleRepositoryTable.InsertExample(exampleTable);
        }

        [Then(@"The user is able to obtain the created example from example repository")]
        public void TheUserIsAbleToObtainTheCreatedExampleFromExampleRepository()
        {
            var realExample = exampleRepositoryTable.GetExample(exampleTable.Id);

            realExample.ExampleOne.Should().Be(exampleTable.ExampleOne);
            realExample.ExampleTwo.Should().Be(exampleTable.ExampleTwo);
        }
    }
}