namespace UkolPlanovac;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Add a new event 'EVENT;name;yyyy-mm-dd' or type: 'LIST', 'STATS' or 'END'");
            bool isInputValid = false;

            while (!isInputValid)
            {
                string input = Console.ReadLine();

                if (input.Contains("EVENT;"))
                {
                    (bool isEventValid, string name, DateTime date) = Event.ParseEvent(input);

                    if (isEventValid)
                    {
                        Event newEvent = new Event(name, date);
                        Event.AddEvent(newEvent);
                        isInputValid = true;
                    }
                    else
                    {
                        Console.WriteLine("The event has a invalid format");
                    }
                }
                else if (input == "LIST")
                {
                    Event.PrintList();
                    isInputValid = true;
                }
                else if (input == "STATS")
                {
                    Event.PrintStats();
                    isInputValid = true;
                }
                else if (input == "END")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }
    }
}
