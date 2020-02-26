using System.Collections.Generic;

namespace fms
{
    public class LandingGearSystem : BaseSystem
    {
        protected override void ProcessState(Dictionary<Oid, float> state)
        {
            var flaps = state[Oid.ThrottleFlapsSel];
            var altitude = state[Oid.DataRadioAltitude];
        }
    }
}