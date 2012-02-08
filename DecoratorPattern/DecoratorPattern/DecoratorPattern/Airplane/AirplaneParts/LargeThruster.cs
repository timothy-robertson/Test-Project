using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Airplane
{
    public class LargeThruster : AirplanePart
    {
        private const float MASS = 40.0f;
        private const float THRUST = 40.0f;

        public LargeThruster(AirplaneBase plane)
        {
            air = plane;
            air.AddMass(MASS);
            air.IncreaseInitialVelocity(THRUST);
        }

        protected override void PreformRemove()
        {
            air.AddMass(-MASS);
            air.IncreaseInitialVelocity(-THRUST);
        }

        public override void Launch(GameTime time)
        {
            air.Launch(time);
        }

        public override void Draw(SpriteBatch batch)
        {
            air.Draw(batch);
        }

        public override String GetString()
        {
            return air.GetString() + "Large Thruster\n";
        }

        public override void Update(GameTime time)
        {
            air.Update(time);
        }

    }
}
