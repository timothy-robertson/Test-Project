using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PhysicsEngine
{
    public class GravityIntegrator : ForceIntegrator
    {

        public override void Integrate(KeyValuePair<PhysicsObject, ForceIntegratorParams> objects, GameTime time)
        {
            PhysicsObject obj1 = objects.Key;
            float acceleration = ((GravityParams)objects.Value).GetAcceleration();

            Vector2 accelerationVector = new Vector2(0.0f, acceleration);

            obj1.AddForce(accelerationVector * obj1.GetMass());
        }

    }
}
