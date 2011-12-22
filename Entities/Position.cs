using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace MongoDb.Entities
{
    public class Position
    {
        public Position()
        {
            Id = ObjectId.GenerateNewId();
        }

        public ObjectId Id { get; set; }
        public Job Job { get; set; }
        public TimeSpan Time { get; set; }
        public int MaximumPersons { get; set; }
        public IEnumerable<Person> Persons { get; set; }
    }
}
