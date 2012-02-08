using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Airplane
{
    public abstract class AirplanePart : AirplaneBase
    {
        protected AirplaneBase air;
        
        protected abstract void PreformRemove();

        public AirplaneBase RemovePart()
        {
            PreformRemove();
            return air;
        }

        public override void SetPosition(Vector2 pos)
        {
            air.SetPosition(pos);
        }

        public override Vector2 GetPosition()
        {
            return air.GetPosition();
        }

        public override void AddMass(float mass)
        {
            air.AddMass(mass);
        }

        public override void AddPower(float power)
        {
            air.AddPower(power);
        }

        public override void IncreaseInitialVelocity(float mag)
        {
            air.IncreaseInitialVelocity(mag);
        }

        public override void AddForce(Vector2 force)
        {
            air.AddForce(force);
        }

        public override float GetRotation()
        {
            return air.GetRotation();
        }

        public override void AddInitialRotation(float angle)
        {
            air.AddInitialRotation(angle);
        }

        public override void SetInitialRotation(float angle)
        {
            air.SetInitialRotation(angle);
        }
    }
}
