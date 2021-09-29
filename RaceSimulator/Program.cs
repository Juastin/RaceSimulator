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
                Data.NextRace();
                Visuals.Initialise(Data.CurrentRace);
                Console.WriteLine(Data.CurrentRace.Track.Name);
                Visuals.DrawTrack();
                Thread.Sleep(100);
            for (; ; ) { }
        }
    }
}
