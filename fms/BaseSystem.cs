using System;
using System.Collections.Generic;

namespace fms
{
    // here we can create common things like timer

    public abstract class BaseSystem
    {
        protected double elapsedMilliseconds = 0;

        DateTime _prevDate = DateTime.Now;

        public void Process(Dictionary<Oid, float> state)
        {
            elapsedMilliseconds += (DateTime.Now - _prevDate).TotalMilliseconds;
            if (elapsedMilliseconds < 0.01) elapsedMilliseconds = 0.01;
            _prevDate = DateTime.Now;

            if (elapsedMilliseconds > 10)
            {
                ProcessState(state);
                elapsedMilliseconds = 0;
            }
        }

        public void Process(double elapsedMilliseconds, Dictionary<Oid, float> state) // for tests only
        {
            this.elapsedMilliseconds = elapsedMilliseconds;

            ProcessState(state);
        }

        protected abstract void ProcessState(Dictionary<Oid, float> state);
    }
}