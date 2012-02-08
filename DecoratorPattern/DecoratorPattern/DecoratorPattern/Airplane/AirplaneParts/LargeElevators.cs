using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Airplane
{
    public class LargeElevators : AirplanePart
    {
        private const float ELEVATOR_MASS = 40.0f;
        private const float ELEVATOR_INFLUENCE = 30.0f;

        public LargeElevators(AirplaneBase plane)
        {
            air = plane;
            air.AddMass(ELEVATOR_MASS);
        }

        protected override void PreformRemove()
        {
            air.AddMass(-ELEVATOR_MASS);
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
            return air.GetString() + "Large Elevator\n";
        }

        public override void Update(GameTime time)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                float rotation = air.GetRotation() - ELEVATOR_INFLUENCE;
                Vector2 direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
                air.AddForce(direction * ELEVATOR_INFLUENCE);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                float rotation = air.GetRotation() + ELEVATOR_INFLUENCE;
                Vector2 direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
                air.AddForce(direction * ELEVATOR_INFLUENCE);
            }
            air.Update(time);
        }

    }
}
