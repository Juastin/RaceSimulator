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

                Visuals.Initialise(Data.CurrentRace);
                //Data.CurrentRace.NewVisuals += OnNewVisuals;
                Data.CurrentRace.Start();
                
                Thread.Sleep(200);
                
            for (; ; ) { }
        }
    }
}
