using System;
using Controller;

namespace RaceSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Data.Initialise();

            Data.NextRace();
            Console.WriteLine($"Eerste race: {Data.CurrentRace.Track.Name}");

            Data.NextRace();
            Console.WriteLine($"Tweede race: {Data.CurrentRace.Track.Name}");
        }
    }
}
