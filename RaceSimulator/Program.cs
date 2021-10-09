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
                Visuals.DrawTrack(Data.CurrentRace.Track);
                Data.CurrentRace.Start();
                Thread.Sleep(200);
            for (; ; ) {
                //if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                //{
                //    foreach (Model.IParticipant participant in Data.CurrentRace.Participants)
                //    {
                //        Data.CurrentRace.CalculateTraveledDistance(participant);
                //    }
                //}

            }
        }
    }
}
