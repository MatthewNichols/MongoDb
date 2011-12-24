using System;
using System.Collections.Generic;
using MongoDb.Entities;
using MongoDb.ViewModels;
using Json = System.String;

namespace MongoDb
{
    internal interface IVolunteers
    {
        /****************** Public Access ***********************/

        // In default form I need to represent several sundays (by default 3, I don't know, do we need "non-default" count).
        // If there is no sunday for asked date in DB, this method should autogenerate it.
        // Dates should be sorted.
        // "from" cannot be earlier than DateTime.Now.Date
        Json GetSundays(DateTime from, int count);

        // Sometimes I need to represent non-sunday. It's for some holiday, for example. The form will consist only of this single day.
        // If there is no such day in DB, this method should autogenerate it.
        // "date" cannot be earlier than DateTime.Now.Date
        Json GetDay(DateTime date);
        
        // Every person can apply to position.
        void ApplyToPositions(DateTime date, IEnumerable<PositionViewModel> positionList, PersonViewModel person);

        /****************** Admin Access ***********************/

        // Admin can edit each day, it's position or volunteer.
        // Maybe it should be not single method...but all data is linked with the day...so, this needs review.
        void UpdateDay(Day day);

        // When updating the day, the positions list must consists only of jobs in DB.
        Json GetJobs();
    }
}