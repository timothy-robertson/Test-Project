using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysicsEngine
{
    public class GravityParams : ForceIntegratorParams
    {
        private float acceleration;

        public GravityParams(float accel)
        {
            acceleration = accel;
        }

        public float GetAcceleration()
        {
            return acceleration;
        }

        public void SetAcceleration(float accel)
        {
            acceleration = accel;
        }
    }
}
