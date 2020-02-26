using System;
using System.Collections.Generic;
using Xunit;
using static fms.Oid;

namespace fms.tests
{
    public class LandingGearSystemTests
    {
        [Fact]
        public void NormalFly()
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

            lgs.Process(1, state);

            var result = state[SoundLandGearHorn];

            Assert.Equal(0, result);

        }

        [Fact]
        public void FlapsOutOfRange()
        {
            BaseSystem lgs = new LandingGearSystem();
            var state = new Dictionary<Oid, float>
            {
                {DataRadioAltitude, 1000},
                {DataEng1, 1},
                {DataEng2, 1},
                {ThrottleFlapsSel, 30},
                {ThrottleLeverAngle1, 0},
                {ThrottleLeverAngle2, 0},
                {DataGearNActual, 0},
                {DataGearLActual, 0},
                {DataGearRActual, 0},
                {SoundLandGearHorn, 0}
            };

            lgs.Process(1, state);

            var result = state[SoundLandGearHorn];

            Assert.Equal(1, result);

        }

        [Fact]
        public void EngineINOP()
        {
            BaseSystem lgs = new LandingGearSystem();
            var state = new Dictionary<Oid, float>
            {
                {DataRadioAltitude, 700},
                {DataEng1, 0},
                {DataEng2, 1},
                {ThrottleFlapsSel, 5},
                {ThrottleLeverAngle1, 25},
                {ThrottleLeverAngle2, 25},
                {DataGearNActual, 0},
                {DataGearLActual, 0},
                {DataGearRActual, 0},
                {SoundLandGearHorn, 0}
            };

            lgs.Process(1, state);

            var result = state[SoundLandGearHorn];

            Assert.Equal(1, result);

        }

        [Fact]
        public void ThrottleLeverAngleOutOfRange()
        {
            BaseSystem lgs = new LandingGearSystem();
            var state = new Dictionary<Oid, float>
            {
                {DataRadioAltitude, 700},
                {DataEng1, 1},
                {DataEng2, 1},
                {ThrottleFlapsSel, 5},
                {ThrottleLeverAngle1, 19},
                {ThrottleLeverAngle2, 33},
                {DataGearNActual, 0},
                {DataGearLActual, 0},
                {DataGearRActual, 0},
                {SoundLandGearHorn, 0}
            };

            lgs.Process(1, state);

            var result = state[SoundLandGearHorn];

            Assert.Equal(1, result);

        }

        [Fact]
        public void Altitude()
        {
            BaseSystem lgs = new LandingGearSystem();
            var state = new Dictionary<Oid, float>
            {
                {DataRadioAltitude, 700},
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

            lgs.Process(1, state);

            var result = state[SoundLandGearHorn];

            Assert.Equal(1, result);

        }

        [Fact]
        public void SilenceHornBtn()
        {
            BaseSystem lgs = new LandingGearSystem();
            var state = new Dictionary<Oid, float>
            { 
                {DataRadioAltitude, 1000},
                {DataEng1, 1},
                {DataEng2, 1},
                {ThrottleFlapsSel, 5},
                {ThrottleLeverAngle1, 25},
                {ThrottleLeverAngle2, 35},
                {DataGearNActual, 0},
                {DataGearLActual, 0},
                {DataGearRActual, 0},
                {SoundLandGearHorn, 0},
                {SilenceLandGearHornBtn, 1}
            };

            lgs.Process(1, state);

            var result = state[SoundLandGearHorn];

            Assert.Equal(0, result);

        }
    }
}
