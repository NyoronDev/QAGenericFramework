using System;

namespace DataFactory.Database.Entities
{
    public class ExampleTableEntity : BaseTableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string ExampleOne { get; set; }

        public string ExampleTwo { get; set; }
    }
}