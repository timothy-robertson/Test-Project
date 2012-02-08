using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PhysicsEngine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Airplane
{
    public class FighterJet : AirplaneBase
    {
        private const float FIGHTER_JET_MASS = 1000.0f;
        private const float FIGHTER_JET_K1 = 0.01f;
        private const float FIGHTER_JET_K2 = 0.5f;

        public FighterJet(Texture2D jet)
        {
            image = jet;
            model = new PhysicsObject();
            model.SetMass(FIGHTER_JET_MASS);
        }

        public override void Launch(GameTime time)
        {
            hasLaunched = true;
            Vector2 velocity = new Vector2((float)Math.Cos(rotation) * initialSpeed, (float)Math.Sin(rotation) * initialSpeed);
            model.SetVelocity(velocity);
            ForceIntegratorRegistry.AddToRegistry(model, new GravityParams(GRAVITY), ForceIntegrator.Type.Gravity);
            ForceIntegratorRegistry.AddToRegistry(model, new DragParams(FIGHTER_JET_K1, FIGHTER_JET_K2), ForceIntegrator.Type.Drag);
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
            return "Fighter Jet\nMass: " + model.GetMass() + "\nParts:\n";
        }
    }
}
