using System;
using Controller;

namespace RaceSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Data.Initialise();
            Visuals.Initialise();
            Data.NextRace();
            Visuals.DrawTrack(Data.CurrentRace.Track);
            
            
            
            /*
            Data.NextRace();

            Console.WriteLine($"Eerste race: {Data.CurrentRace.Track.Name}");

            Data.NextRace();
            Console.WriteLine($"Tweede race: {Data.CurrentRace.Track.Name}");

            Data.NextRace();
            Console.WriteLine($"Third race: {Data.CurrentRace.Track.Name}");

            Data.NextRace();
            Console.WriteLine($"Fourth race?: {Data.CurrentRace.Track.Name}");
            */

        }
    }
}
