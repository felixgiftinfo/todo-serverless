using System;
using System.Collections.Generic;
using System.Text;

namespace Todo_Serverless.Models
{
    public class TodoModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public string StartTime { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string EndTime { get; set; }
        public bool Completed { get; set; }
        public bool Cancelled { get; set; }
        public bool Missed { get; set; }


        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateModified { get; set; }
    }
}
