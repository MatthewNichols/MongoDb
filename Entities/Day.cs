using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace MongoDb.Entities
{
    public class Day
    {
        private DateTime datePart;

        public Day()
        {
            Id = ObjectId.GenerateNewId();
        }

        public ObjectId Id { get; set; }

        public DateTime Date
        {
            get { return datePart; }
            set { datePart = value.Date; }
        }

        public IEnumerable<Position> Positions { get; set; }
        public bool Active { get; set; }
    }
}