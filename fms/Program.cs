using System;
using System.Collections.Generic;
using System.Threading;
using static fms.Oid;

namespace fms
{
    internal class Program
    {
        private static readonly Random _rnd = new Random();

        private static void Main(string[] args)
        {
            BaseSystem lgs = new LandingGearSystem();
            var state = new Dictionary<Oid, float>
            {
                {DataRadioAltitude, 1000},
                {DataEng1, 1},
                {DataEng2, 1},
                {ThrottleFlapsSel, 5},
                {ThrottleLeverAngle1, 0},
                {ThrottleLeverAngle2, 0},
                {DataGearNActual, 0},
                {DataGearLActual, 0},
                {DataGearRActual, 0},
                {SoundLandGearHorn, 0}
            };

            while (true)
            {
                lgs.Process(state);

                if (state[SoundLandGearHorn] != 0)
                    Console.WriteLine("SoundLandGearHorn playing");

                if (state[DataRadioAltitude] > 600)
                    state[DataRadioAltitude] -= 5;
                else
                    state[DataRadioAltitude] += 5;

                Thread.Sleep(_rnd.Next(30, 60));
            }
        }
    }
}