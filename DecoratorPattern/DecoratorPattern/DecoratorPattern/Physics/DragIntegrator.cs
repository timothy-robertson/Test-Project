using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
//
namespace PhysicsEngine
{
    public class DragIntegrator : ForceIntegrator
    {
        public override void Integrate(KeyValuePair<PhysicsObject, ForceIntegratorParams> objects, GameTime time)
        {
            DragParams param = (DragParams)objects.Value;
            PhysicsObject obj1 = objects.Key;
            float k1 = param.GetK1();
            float k2 = param.GetK2();

            Vector2 force;
            force = obj1.GetVelocity();

            float dragCoeff = force.Length();
            dragCoeff = k1 * dragCoeff + k2 * dragCoeff * dragCoeff;

            if (force.LengthSquared() > 1.0f)
            {
                force.Normalize();
            }
            force *= -dragCoeff;

            obj1.AddForce(force);
        }

    }
}
