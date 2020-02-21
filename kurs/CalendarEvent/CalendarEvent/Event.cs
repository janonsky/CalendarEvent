using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarEvent
{
    public class Event
    {
        private DateTime notifyTime;
        public DateTime NotifyTime { get => notifyTime; set => notifyTime = value; }
        private int hashCode;
        public int HashCode { get => hashCode; }
        private DateTime startTime;
        public DateTime StartTime { get => startTime; set => startTime = value; }
        private DateTime endTime;
        public DateTime EndTime { get => endTime; set => endTime = value; }
        private string name;
        public string Name { get => name; set => name = value; }
        private string place;
        public string Place { get => place; set => place = value; }
        private string description;
        public string Description { get => description; set => description = value; }

        public Event(DateTime notifyTime, DateTime startTime, DateTime endTime, string name, string place, string description)
        {
            this.hashCode = this.GetHashCode();
            this.startTime = startTime;
            this.endTime = endTime;
            this.name = name;
            this.place = place;
            this.description = description;
            this.notifyTime = notifyTime;
        }
        public void Change(DateTime notifyTime, DateTime startTime, DateTime endTime, string name, string place, string description)
        {
            this.startTime = startTime;
            this.endTime = endTime;
            this.name = name;
            this.place = place;
            this.description = description;
            this.notifyTime = notifyTime;
        }
    }
}
