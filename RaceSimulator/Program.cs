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
                Data.CurrentRace.Start();
                Visuals.Initialise(Data.CurrentRace);
                Console.WriteLine(Data.CurrentRace.Track.Name);
                //Visuals.DrawTrack(Data.CurrentRace.Track);
                Thread.Sleep(100);
            for (; ; ) { }
        }
    }
}
