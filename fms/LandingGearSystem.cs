using System;
using System.Collections.Generic;

namespace fms
{
    public class LandingGearSystem : BaseSystem
    {
        private Dictionary<Oid, float> _stateTemp;

        private bool _isStateChanged;
        private bool _isAltLessThan800;
        private bool _isAltLessThan200;
        private bool _isFlaps15Thru25;
        private bool _isEngineInop;
        private bool _isTLAlessThan20;
        private bool _isTLAlessThan34;
        private bool _isFlapsMoreThan25;
        private bool _isLandingGearDown;


        protected override void ProcessState(Dictionary<Oid, float> state)
        {
            CheckStateChanged(state);

            bool isFlapsAndAltitudeOutOfRange = _isAltLessThan800 || _isAltLessThan200 || _isFlaps15Thru25;
            bool isTLAoutOfRange = (_isTLAlessThan20 || _isEngineInop) && _isTLAlessThan34;

            var SilenceHornBtn = state.ContainsKey(Oid.SilenceLandGearHornBtn)? state[Oid.SilenceLandGearHornBtn]: 0;
            bool isSilenceHornButtonDown = SilenceHornBtn == 1;

            if ((isFlapsAndAltitudeOutOfRange && isTLAoutOfRange || _isFlapsMoreThan25) && !_isLandingGearDown)
            {
                if (!isSilenceHornButtonDown || (isSilenceHornButtonDown && _isStateChanged))
                {
                    state[Oid.SoundLandGearHorn] = 1;
                }
                else
                {
                    state[Oid.SoundLandGearHorn] = 0;
                }
            }
            else 
            {
                state[Oid.SoundLandGearHorn] = 0;
            }
        }

        private void CheckStateChanged(Dictionary<Oid, float> state)
        {
            _isStateChanged = false;
            var flaps = state[Oid.ThrottleFlapsSel];
            var altitude = state[Oid.DataRadioAltitude];

            if (_isAltLessThan800 != (flaps <= 10 && altitude < 800))
            {
                _isStateChanged = true;
                _isAltLessThan800 = flaps <= 10 && altitude < 800;
            }

            if (_isAltLessThan200 != (flaps <= 10 && altitude < 200))
            {
                _isStateChanged = true;
                _isAltLessThan200 = flaps <= 10 && altitude < 200;
            }

            if (_isFlaps15Thru25 != (flaps >= 15 && flaps <= 25))
            {
                _isStateChanged = true;
                _isFlaps15Thru25 = flaps >= 15 && flaps <= 25;
            }

            if (_isFlapsMoreThan25 != (flaps > 25))
            {
                _isStateChanged = true;
                _isFlapsMoreThan25 = flaps > 25;
            }

            var tla1 = state[Oid.ThrottleLeverAngle1];
            var tla2 = state[Oid.ThrottleLeverAngle2];

            if (_isEngineInop != (state[Oid.DataEng1] == 0 || state[Oid.DataEng2] == 0))
            {
                _isStateChanged = true;
                _isEngineInop = state[Oid.DataEng1] == 0 || state[Oid.DataEng2] == 0;
            }

            if (_isTLAlessThan20 != (Math.Min(tla1,tla2) < 20))
            {
                _isStateChanged = true;
                _isTLAlessThan20 = Math.Min(tla1, tla2) < 20;
            }

            if (_isTLAlessThan34 != (Math.Max(tla1, tla2) < 34))
            {
                _isStateChanged = true;
                _isTLAlessThan34 = Math.Max(tla1, tla2) < 34;
            }

            if (_isLandingGearDown != (state[Oid.DataGearLActual] == 1 && state[Oid.DataGearNActual] == 1 && state[Oid.DataGearRActual] == 1))
            {
                _isLandingGearDown = state[Oid.DataGearLActual] == 1 && state[Oid.DataGearNActual] == 1 && state[Oid.DataGearRActual] == 1;
            }

            var SilenceHornBtn = state.ContainsKey(Oid.SilenceLandGearHornBtn) ? state[Oid.SilenceLandGearHornBtn] : 0;
            if (SilenceHornBtn == 0)
            {
                _isStateChanged = false;
            }
        }
    }
}