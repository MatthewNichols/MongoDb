using MongoDB.Bson;

namespace MongoDb.Entities
{
    public class Job
    {
        public Job()
        {
            Id = ObjectId.GenerateNewId();
        }

        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }
}