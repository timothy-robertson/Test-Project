using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Airplane
{
    public class SmallEngine : AirplanePart
    {
        private const float SMALL_ENGINE_MASS = 15.0f;
        private const float SMALL_ENGINE_THRUST = 10.0f;

        public SmallEngine(AirplaneBase plane)
        {
            air = plane;
            air.AddMass(SMALL_ENGINE_MASS);
        }

        protected override void PreformRemove()
        {
            air.AddMass(-SMALL_ENGINE_MASS);
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
            return air.GetString() + "Small Engine\n";
        }

        public override void Update(GameTime time)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Vector2 direction = new Vector2((float)Math.Cos(air.GetRotation()), (float)Math.Sin(air.GetRotation()));
                air.AddForce(direction * SMALL_ENGINE_THRUST);
            }
            air.Update(time);
        }

    }
}
