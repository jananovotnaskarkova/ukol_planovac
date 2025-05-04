using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Globalization;

namespace UkolPlanovac
{
    public class Event
    {
        private String EventName;
        private DateTime EventDate;

        private static List<Event> EventList = [];
        private static Dictionary<DateTime, int> EventDict = [];
        
        public Event(string name, DateTime date)
        {
            EventName = name;
            EventDate = date;
        }

        public static (bool, string, DateTime) CheckInput(string input)
        {
            String[] inputSplitted = input.Split(";");
            DateTime date = DateTime.Now;
            bool result = false;

            if ((inputSplitted.Length == 3) && (inputSplitted[0] == "EVENT") && DateTime.TryParseExact(inputSplitted[2], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
               result = true;                      
            }

            return (result, inputSplitted[1], date);  
        }

        public static void AddEventToList(Event newEvent)
        {
            EventList.Add(newEvent);
        }

        public static void AddEventToCalendar(Event newEvent)
        {
            if (EventDict.ContainsKey(newEvent.EventDate))
            {
                EventDict[newEvent.EventDate] ++; 
            }
            else
            {
                EventDict[newEvent.EventDate] = 1;
            }
        }

        public void PrintEvent()
        {
            int remainingDays = (EventDate - DateTime.Today).Days;
            if (remainingDays >= 0)
            {
                Console.WriteLine($"'{EventName}' with date {EventDate:yyyy-MM-dd} will happen in {remainingDays} days");
            }
            else if (remainingDays < 0)
            {
                Console.WriteLine($"'{EventName}' with date {EventDate:yyyy-MM-dd} happened {-remainingDays} days ago");
            }            
        }

        public static void PrintList()
        {
            if (EventList.Count() == 0)
            {
                Console.WriteLine("Event list is empty");
                return;
            }

            foreach (Event eventInList in EventList)
            {
                eventInList.PrintEvent();
            }
        }

        public static void PrintStats()
        {
            List<DateTime> DateList = [];

            if (EventDict.Count() == 0)
            {
                Console.WriteLine("Event statistics are empty");
                return;
            }

            foreach (KeyValuePair<DateTime, int> entry in EventDict)
            {
                DateList.Add(entry.Key);
            }

            DateList.Sort();

            foreach (DateTime date in DateList)
            {
                Console.WriteLine($"Date: {date:yyyy-MM-dd}: events: {EventDict[date]}");
            }
        }        
    }
}