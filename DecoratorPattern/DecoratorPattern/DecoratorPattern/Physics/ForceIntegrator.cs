using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PhysicsEngine
{
    public abstract class ForceIntegrator
    {
        public enum Type{Gravity, Drag};

        public abstract void Integrate(KeyValuePair<PhysicsObject, ForceIntegratorParams> objects, GameTime time);
    }
}
