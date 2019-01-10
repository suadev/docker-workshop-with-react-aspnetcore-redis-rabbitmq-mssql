using System;

namespace api.Models
{
    public class LogModel
    {
        public DateTime Time { get; set; }
        public int EventId { get; set; }
        public string Message { get; set; }
    }
}