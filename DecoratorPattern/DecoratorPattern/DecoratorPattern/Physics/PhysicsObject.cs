using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PhysicsEngine
{
    public class PhysicsObject
    {
        private Vector2 position;
        private Vector2 velocity;
        private Vector2 acceleration;
        private GameTime last_time;
        private float inverse_mass;
        protected float resistance = 1.0f;
        private int ID;
        private Vector2 force;

        public PhysicsObject()
        {
            SetPosition(Vector2.Zero);
            SetVelocity(Vector2.Zero);
            SetAcceleration(Vector2.Zero);
            last_time = new GameTime();
            force = Vector2.Zero;
            ID = PhysicsObjectRegistry.GetNewID();
            //Console.Out.WriteLine("New Ball Created! ID is " + ID);
        }

        #region Getters and Setters

        public void SetMass(float mass)
        {
            SetInverseMass(1.0f / mass);
        }

        public void SetInverseMass(float inverse)
        {
            inverse_mass = inverse;
        }

        public void SetPosition(Vector2 pos)
        {
            position = pos;
        }

        public void SetVelocity(Vector2 vel)
        {
            velocity = vel;
        }

        public void SetAcceleration(Vector2 accel)
        {
            acceleration = accel;
        }

        public void SetLastTime(GameTime time)
        {
            last_time = time;
        }

        public GameTime GetLastTime()
        {
            return last_time;
        }

        public float GetMass()
        {
            return 1.0f / inverse_mass;
        }

        public float GetInverseMass()
        {
            return inverse_mass;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public Vector2 GetVelocity()
        {
            return velocity;
        }

        public Vector2 GetAcceleration()
        {
            return acceleration;
        }
        #endregion

        public void AddForce(Vector2 force)
        {
            //Console.Out.WriteLine("Force = " + force.Length());
            this.force += force;
        }

        public void Integrate(GameTime time)
        {
            //Add the force to the acceleration
            SetAcceleration(/*GetAcceleration() + */(force * inverse_mass));
            //Console.Out.WriteLine("Acceleration = " + GetAcceleration().Length());
            //p' = p + vt + 1/2at^2
            //float deltatime = (float)(time.TotalGameTime.TotalMilliseconds - GetLastTime().TotalGameTime.TotalMilliseconds);
            float deltatime = (time.ElapsedGameTime.Milliseconds);
            SetPosition(GetPosition() + (GetVelocity() * deltatime) + (0.5f * GetAcceleration() * (float)Math.Pow(deltatime, 2)));
            //v' = v*d^t + at
            SetVelocity((GetVelocity() * (float)Math.Pow(resistance, deltatime)) + (GetAcceleration() * deltatime));
            //Console.Out.WriteLine("Velocity = " + GetVelocity().Length());
            //Set the last time, end integration
            SetLastTime(time);
            //Clear the forces
            force = Vector2.Zero;
            //Console.Out.WriteLine("Velocity = " + GetVelocity().LengthSquared());
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == this.GetType())
            {
                PhysicsObject obj1 = (PhysicsObject)obj;
                if (obj1.ID == this.ID)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Equals(PhysicsObject obj)
        {
            if (obj.ID == this.ID)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
