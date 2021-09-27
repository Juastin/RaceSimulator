using System;
using System.Threading;
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
                Console.WriteLine(Data.CurrentRace.Track.Name);
                Visuals.DrawTrack(Data.CurrentRace.Track);
                Thread.Sleep(100);
            
            
            
            
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
