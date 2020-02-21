using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace CalendarEvent
{
    public class EventManager
    {
        public static List<Event> events = new List<Event>();
        public static void AddEvent(Event even) => events.Add(even);
        public static void RemoveEvent(int hashCode)
        {
            foreach (var x in events)
            {
                if (x.HashCode == hashCode)
                {
                    RemoveEvent(x);
                    return;
                }
            }
        }
        public static void RemoveEvent(Event even) => events.Remove(even);

        public static void SaveEvents()
        {
            using (StreamWriter sw = new StreamWriter(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/data.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                new JsonSerializer().Serialize(writer, events);
            }
        }
        public static void LoadEvents(string path)
        {
            var serializer = new JsonSerializer();
            try
            {
                var sr = new StreamReader(path);
                using (var jsonTextReader = new JsonTextReader(sr))
                {
                    events = serializer.Deserialize<List<Event>>(jsonTextReader);
                }
            }
            catch
            {
                SaveEvents();
                LoadEvents(path);
            }
        }
        public static bool IsDateContainsEvent(DateTime date) => events.Any(x => x.StartTime.Day == date.Day && x.StartTime.Month == date.Month && x.StartTime.Year == date.Year);

        public static List<Event> GetEventsByDate(DateTime date)
        {
            List<Event> output = new List<Event>();
            foreach (var x in events)
            {
                if (x.StartTime.Day == date.Day && x.StartTime.Month == date.Month && x.StartTime.Year == date.Year)
                    output.Add(x);
            }
            return output;
        }
        public static Event GetEvent(string text)
        {
            foreach (var x in events)
            {
                if (x.Name == text)
                    return x;
            }
            return null;
        }
        public static void ChangeEvent(int hash, DateTime no, DateTime startTime, DateTime endTime, string name, string place, string description)
        {
            GetEvent(hash).Change(no, startTime, endTime, name, place, description);
        }
        public static Event GetEvent(int hash)
        {
            foreach (var x in events)
            {
                if (x.HashCode == hash)
                    return x;
            }
            return null;
        }
        public static int GetHashCodeEvent(string name)
        {
            foreach (var x in events)
            {
                if (x.Name == name)
                    return x.HashCode;
            }
            return 0;
        }
        public static List<Event> GetEventByDateTimeNow()
        {
            List<Event> output = new List<Event>();
            var events = GetEventsByDate(DateTime.Now);
            foreach (var x in events)
            {
                if (x.NotifyTime.Hour == DateTime.Now.Hour && x.NotifyTime.Minute == DateTime.Now.Minute && x.NotifyTime.Second == DateTime.Now.Second)
                {
                    output.Add(x);
                }
            }
            return output;
        }
        public static DateTime[] GetEventStarted()
        {
            int count = 0;
            DateTime[] even = new DateTime[events.Count];
            for (int i = 0; i < even.Length; i++)
            {
                    even[count] = events[i].StartTime;
                    count++;
            }
            return even;
        }
    }
}
