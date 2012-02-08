using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysicsEngine
{
    public static class PhysicsObjectRegistry
    {
        private static int count = 0;

        public static int GetNewID()
        {
            return count++;
        }
    }
}
