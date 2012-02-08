using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysicsEngine
{
    public class DragParams : ForceIntegratorParams
    {
        private float k1;
        private float k2;

        public DragParams(float K1, float K2)
        {
            k1 = K1;
            k2 = K2;
        }

        public float GetK1()
        {
            return k1;
        }

        public float GetK2()
        {
            return k2;
        }

        public void SetK1(float K1)
        {
            k1 = K1;
        }

        public void SetK2(float K2)
        {
            k2 = K2;
        }
    }
}
