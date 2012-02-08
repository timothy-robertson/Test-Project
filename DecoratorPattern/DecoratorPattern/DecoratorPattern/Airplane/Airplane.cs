using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PhysicsEngine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Airplane
{
    public abstract class AirplaneBase
    {
        protected const float GRAVITY = 0.0098f;

        protected PhysicsObject model;
        protected Texture2D image;
        protected bool hasLaunched = false;
        protected float powerLeft;
        protected float rotation = 0.0f;
        protected float initialSpeed = 0.0f;

        public abstract void Launch(GameTime time);
        public abstract void Draw(SpriteBatch batch);
        public abstract void Update(GameTime time);
        public abstract String GetString();

        public virtual void SetPosition(Vector2 pos)
        {
            model.SetPosition(pos);
        }

        public virtual Vector2 GetPosition()
        {
            return model.GetPosition();
        }

        public virtual void AddMass(float mass)
        {
            model.SetMass(model.GetMass() + mass);
        }

        public virtual void AddPower(float power)
        {
            if (!hasLaunched)
            {
                powerLeft += power;
            }
        }

        public virtual void IncreaseInitialVelocity(float mag)
        {
            initialSpeed += mag;
        }

        public virtual void AddForce(Vector2 force)
        {
            if (powerLeft > 0)
            {
                model.AddForce(force);
                powerLeft -= 1.0f;
            }
        }

        public virtual float GetRotation()
        {
            if (hasLaunched)
            {
                return (float)Math.Atan2(model.GetVelocity().Y, model.GetVelocity().X);
            }
            else
            {
                return rotation;
            }
        }

        public virtual void AddInitialRotation(float angle)
        {
            rotation += angle;
        }

        public virtual void SetInitialRotation(float angle)
        {
            rotation = angle;
        }

    }
}
