using Dapper.FluentMap.Mapping;

namespace DataFactory.Database.Entities.Mappers
{
    public class ExampleMapper : EntityMap<ExampleTableEntity>
    {
        public ExampleMapper()
        {
            Map(m => m.Id).ToColumn("example_id");
            Map(m => m.ExampleOne).ToColumn("example_one");
            Map(m => m.ExampleTwo).ToColumn("example_two");
        }
    }
}