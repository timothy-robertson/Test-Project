using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PhysicsEngine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Airplane
{
    public class SmallAirplane : AirplaneBase
    {
        private const float SMALL_AIRPLANE_MASS = 100.0f;
        private const float SMALL_AIRPLANE_K1 = 0.05f;
        private const float SMALL_AIRPLANE_K2 = 0.2f;

        public SmallAirplane(Texture2D air)
        {
            image = air;
            model = new PhysicsObject();
            model.SetMass(SMALL_AIRPLANE_MASS);
        }

        public override void Launch(GameTime time)
        {
            hasLaunched = true;
            Vector2 velocity = new Vector2((float)Math.Cos(rotation) * initialSpeed, (float)Math.Sin(rotation) * initialSpeed);
            model.SetVelocity(velocity);
            ForceIntegratorRegistry.AddToRegistry(model, new GravityParams(GRAVITY), ForceIntegrator.Type.Gravity);
            ForceIntegratorRegistry.AddToRegistry(model, new DragParams(SMALL_AIRPLANE_K1, SMALL_AIRPLANE_K2), ForceIntegrator.Type.Drag);
        }

        public override void Draw(SpriteBatch batch)
        {
            Vector2 pos = GetPosition();
            Vector2 center = new Vector2(pos.X + ((float)image.Width / 2.0f), pos.X + ((float)image.Height / 2.0f));
            batch.Draw(image,
                       pos,
                       new Rectangle(0, 0, image.Width, image.Height),
                       Color.White,
                       GetRotation(),
                       new Vector2(image.Width / 2, image.Height / 2), 
                       1.0f,
                       SpriteEffects.None,
                       0.5f);
        }

        public override void Update(GameTime time)
        {
            model.Integrate(time);
        }

        public override String GetString()
        {
            return "Small Aircraft\nMass: " + model.GetMass() + "\nParts:\n";
        }
    }
}
