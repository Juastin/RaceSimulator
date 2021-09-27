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
        }
    }
}
