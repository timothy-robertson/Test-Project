using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Airplane
{
    public class MediumEngine : AirplanePart
    {
        private const float MEDIUM_ENGINE_MASS = 25.0f;
        private const float MEDIUM_ENGINE_THRUST = 25.0f;

        public MediumEngine(AirplaneBase plane)
        {
            air = plane;
            air.AddMass(MEDIUM_ENGINE_MASS);
        }

        protected override void PreformRemove()
        {
            air.AddMass(-MEDIUM_ENGINE_MASS);
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
            return air.GetString() + "Medium Engine\n";
        }

        public override void Update(GameTime time)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Vector2 direction = new Vector2((float)Math.Cos(air.GetRotation()), (float)Math.Sin(air.GetRotation()));
                air.AddForce(direction * MEDIUM_ENGINE_THRUST);
            }
            air.Update(time);
        }

    }
}
