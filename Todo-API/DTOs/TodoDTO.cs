using System;
using System.Collections.Generic;
using System.Text;

namespace Todo_API.DTOs
{
    public class TodoDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public string StartTime { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string EndTime { get; set; }
    }

    public class TodoReadDTO : TodoDTO
    {
        public string Id { get; set; }
        public bool Completed { get; set; }
        public bool Cancelled { get; set; }
        public bool Missed { get; set; }
    }
}
