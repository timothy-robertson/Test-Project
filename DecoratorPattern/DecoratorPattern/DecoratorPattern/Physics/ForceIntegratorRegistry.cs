using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PhysicsEngine
{
    public static class ForceIntegratorRegistry
    {
        
        private static GravityIntegrator gravity = new GravityIntegrator();
        private static DragIntegrator drag = new DragIntegrator();

        private static Dictionary<ForceIntegrator.Type, List<KeyValuePair<PhysicsObject, ForceIntegratorParams>>> registry = new Dictionary<ForceIntegrator.Type, List<KeyValuePair<PhysicsObject, ForceIntegratorParams>>>();

        //----------------------------------------------------------------------
        //Add To Registry
        //----------------------------------------------------------------------
        public static void AddToRegistry(PhysicsObject obj1, ForceIntegratorParams obj2, ForceIntegrator.Type type)
        {
            List<KeyValuePair<PhysicsObject, ForceIntegratorParams>> directory;
            if (registry.ContainsKey(type))
            {
                if (registry.TryGetValue(type, out directory))
                {
                    directory.Add(new KeyValuePair<PhysicsObject, ForceIntegratorParams>(obj1, obj2));
                    return;
                }
            }

            directory = new List<KeyValuePair<PhysicsObject, ForceIntegratorParams>>();
            directory.Add(new KeyValuePair<PhysicsObject, ForceIntegratorParams>(obj1, obj2));
            registry.Add(type, directory);
        }

        //----------------------------------------------------------------------
        //Remove From Registry
        //----------------------------------------------------------------------
        public static void RemoveFromRegistry(PhysicsObject obj1, ForceIntegratorParams obj2, ForceIntegrator.Type type)
        {
            List<KeyValuePair<PhysicsObject, ForceIntegratorParams>> directory;
            KeyValuePair<PhysicsObject, ForceIntegratorParams> pair = (new KeyValuePair<PhysicsObject, ForceIntegratorParams>(obj1, obj2));
            if (registry.ContainsKey(type))
            {
                if (registry.TryGetValue(type, out directory))
                {
                    if (directory.Contains(pair))
                    {
                        directory.Remove(pair);
                    }
                }
            }       
        }

        //----------------------------------------------------------------------
        //Clear From Registry
        //----------------------------------------------------------------------
        public static void ClearAllForceIntegrators(PhysicsObject obj)
        {
            List<KeyValuePair<PhysicsObject, ForceIntegratorParams>> directory;
            List<KeyValuePair<PhysicsObject, ForceIntegratorParams>> toRemove = new List<KeyValuePair<PhysicsObject, ForceIntegratorParams>>();
            foreach (ForceIntegrator.Type type in Enum.GetValues(typeof(ForceIntegrator.Type)).Cast<ForceIntegrator.Type>())
            {
                if (registry.ContainsKey(type))
                {
                    if (registry.TryGetValue(type, out directory))
                    {
                        foreach (KeyValuePair<PhysicsObject, ForceIntegratorParams> pair in directory)
                        {
                            if (pair.Key.Equals(obj) || pair.Value.Equals(obj))
                            {
                                toRemove.Add(pair);
                            }
                        }
                        foreach (KeyValuePair<PhysicsObject, ForceIntegratorParams> trash in toRemove)
                        {
                            directory.Remove(trash);
                        }
                    }
                }
            }
        }

        //----------------------------------------------------------------------
        //Integrate Forces
        //----------------------------------------------------------------------
        public static void IntegrateForces(GameTime time)
        {
            foreach (ForceIntegrator.Type type in Enum.GetValues(typeof(ForceIntegrator.Type)).Cast<ForceIntegrator.Type>())
            {
                List<KeyValuePair<PhysicsObject, ForceIntegratorParams>> directory;
                if (registry.ContainsKey(type))
                {
                    if (registry.TryGetValue(type, out directory))
                    {
                        if (type.Equals(ForceIntegrator.Type.Gravity))
                        {
                            foreach (KeyValuePair<PhysicsObject, ForceIntegratorParams> pair in directory)
                            {
                                gravity.Integrate(pair, time);
                            }
                        }
                        else if (type.Equals(ForceIntegrator.Type.Drag))
                        {
                            foreach (KeyValuePair<PhysicsObject, ForceIntegratorParams> pair in directory)
                            {
                                drag.Integrate(pair, time);
                            }
                        }
                    }
                }
            }
        }

    }
}
