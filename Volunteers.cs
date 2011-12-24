using System;
using System.Collections.Generic;
using System.Linq;
using FluentMongo.Linq;
using MongoDB.Driver;
using MongoDb.Entities;
using MongoDb.ViewModels;
using Newtonsoft.Json;
using Json = System.String;

namespace MongoDb
{
    public class Volunteers : IVolunteers
    {
        private readonly MongoDatabase db;

        public Volunteers()
        {
            var server = MongoServer.Create();
            db = server.GetDatabase("volunteers");
        }

        public Json GetSundays(DateTime from, int count)
        {
            var sundays = new List<Json>();
            for (var i = 0; i < count * 7; i++)
            {
                if(from.DayOfWeek == DayOfWeek.Sunday)
                    sundays.Add(GetDay(from));
                from += new TimeSpan(1, 0, 0, 0);
            }
            return Json(sundays);
        }

        public Json GetDay(DateTime date)
        {
            var day = (from d in db.GetCollection<Day>("days").AsQueryable() where d.Date == date.Date select d).SingleOrDefault();

            return day != null ? Json(day) : GenerateDefaultDay(date);
        }

        public void ApplyToPosition(DateTime date, IEnumerable<PositionViewModel> position, PersonViewModel person)
        {
            throw new NotImplementedException();
        }

        public void UpdateDay(Day day)
        {
            throw new NotImplementedException();
        }

        public string GetJobs()
        {
            throw new NotImplementedException();
        }

        private static Json Json(object data)
        {
            //Todo: beautify the result of serialize
            return JsonConvert.SerializeObject(data);
        }

        private static void GenerateDefaultJobs(MongoCollection jobsCollection)
        {
            jobsCollection.InsertBatch(new List<Job>
                                           {
                                               new Job {Name = "Commons Coordinator"},
                                               new Job {Name = "Sound & Light"},
                                               new Job {Name = "Greeters"},
                                               new Job {Name = "YRE Family Greeter"}
                                           });
        }

        private Json GenerateDefaultDay(DateTime date)
        {
            var jobsCollection = db.GetCollection<Job>("jobs");

            // Todo: remove this in production
            if(jobsCollection.Count() == 0)
                GenerateDefaultJobs(jobsCollection);

            var jobsList = jobsCollection.AsQueryable();

            var day = new Day
                          {
                              Date = date,
                              Positions = new List<Position>
                                              {
                                                  new Position
                                                      {
                                                          Job = jobsList.SingleOrDefault(x => x.Name == "Commons Coordinator"),
                                                          MaximumPersons = 3,
                                                          Time = new TimeSpan(9, 0, 0)
                                                      },
                                                  new Position
                                                      {
                                                          Job = jobsList.SingleOrDefault(x => x.Name == "Sound & Light"),
                                                          MaximumPersons = 3,
                                                          Time = new TimeSpan(9, 0, 0)
                                                      },
                                                  new Position
                                                      {
                                                          Job = jobsList.SingleOrDefault(x => x.Name == "Sound & Light"),
                                                          MaximumPersons = 3,
                                                          Time = new TimeSpan(11, 0, 0)
                                                      },
                                                  new Position
                                                      {
                                                          Job = jobsList.SingleOrDefault(x => x.Name == "Greeters"),
                                                          MaximumPersons = 3,
                                                          Time = new TimeSpan(11, 0, 0)
                                                      },
                                                  new Position
                                                      {
                                                          Job = jobsList.SingleOrDefault(x => x.Name == "YRE Family Greeter"),
                                                          MaximumPersons = 3,
                                                          Time = new TimeSpan(9, 0, 0)
                                                      },
                                              }
                          };

            db.GetCollection<Day>("days").Save(day);
            return Json(day);
        }
    }
}