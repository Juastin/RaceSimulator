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

                Console.WriteLine(Data.CurrentRace.Track.Name);
                Visuals.Initialise(Data.CurrentRace);
                //Data.CurrentRace.NewVisuals += OnNewVisuals;
                Data.CurrentRace.Start();
                
                Thread.Sleep(200);
                

            //static void OnNewVisuals(object sender, EventArgs e)
            //{
            //    Visuals.Initialise(Data.CurrentRace);
            //    Data.CurrentRace.NewVisuals += OnNewVisuals;
            //}

                
            for (; ; ) { }
        }
    }
}
