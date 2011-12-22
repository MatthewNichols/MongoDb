using MongoDB.Bson;

namespace MongoDb.Entities
{
    public class Person
    {
        public Person()
        {
            Id = ObjectId.GenerateNewId();
        }

        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}