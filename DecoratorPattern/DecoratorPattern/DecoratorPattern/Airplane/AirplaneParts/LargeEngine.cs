using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Airplane
{
    public class LargeEngine : AirplanePart
    {
        private const float LARGE_ENGINE_MASS = 50.0f;
        private const float LARGE_ENGINE_THRUST = 60.0f;

        public LargeEngine(AirplaneBase plane)
        {
            air = plane;
            air.AddMass(LARGE_ENGINE_MASS);
        }

        protected override void PreformRemove()
        {
            air.AddMass(-LARGE_ENGINE_MASS);
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
            return air.GetString() + "Large Engine\n";
        }

        public override void Update(GameTime time)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Vector2 direction = new Vector2((float)Math.Cos(air.GetRotation()), (float)Math.Sin(air.GetRotation()));
                air.AddForce(direction * LARGE_ENGINE_THRUST);
            }
            air.Update(time);
        }

    }
}
