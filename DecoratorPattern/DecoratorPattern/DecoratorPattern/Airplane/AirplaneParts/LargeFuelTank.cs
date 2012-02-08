using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Airplane
{
    public class LargeFuelTank : AirplanePart
    {
        private const float TANK_MASS = 75.0f;
        private const float TANK_POWER = 50.0f;

        public LargeFuelTank(AirplaneBase plane)
        {
            air = plane;
            air.AddMass(TANK_MASS);
            air.AddPower(TANK_POWER);
        }

        protected override void PreformRemove()
        {
            air.AddMass(-TANK_MASS);
            air.AddPower(-TANK_POWER);
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
            return air.GetString() + "Large Fuel Tank\n";
        }

        public override void Update(GameTime time)
        {
            air.Update(time);
        }

    }
}
