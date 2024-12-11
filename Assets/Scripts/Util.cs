using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelperMethods
{
    public static class Util
    {
        /// <summary>
        /// Returns the parameter vector as itself, but with no y value
        /// </summary>
        /// <param name="vector3"></param>
        /// <returns></returns>
        public static Vector3 VectorNoY(Vector3 vector3)
        {
            return vector3 - Vector3.up * vector3.y;
        }

        public static Vector3 VectorSameY(Vector3 vector3, float y)
        {
            return vector3 - Vector3.up * vector3.y + Vector3.up * y;
        }

        /// <summary>
        /// Returns the distance of two vectors with a y of 0
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static float DistanceNoY(Vector3 first, Vector3 second)
        {
            return new Vector3(first.x - second.x, 0, first.z - second.z).magnitude;
        }

        /// <summary>
        /// Version of this method allowing you to put your own Y value to compare against just in case u don't want the Y to be 0
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="differentY"></param>
        /// <returns></returns>
        public static float DistanceNoY(Vector3 first, Vector3 second, float differentY)
        {
            return new Vector3(first.x - second.x, differentY, first.z - second.z).magnitude;
        }

        /// <summary>
        /// A TryGetComponent for parents of transforms
        /// Returns the transform parameter if the parameter does not have a parent
        /// </summary>
        /// <param name="currentTransform"></param>
        /// <returns></returns>
        public static Transform TryGetParent(Transform currentTransform)
        {
            try
            {
                return currentTransform.parent;
            }
            catch
            {
                Debug.Log("No Parent Exists For This Object. Returning Same Transform");
                return currentTransform;
            }
        }

        /// <summary>
        /// Checks the timer against the current time and returns true if the timer is up
        /// </summary>
        /// <param name="timer"></param>
        /// <returns></returns>
        public static bool CheckTimer(float timer)
        {
            return timer < Time.time;
        }


        /// <summary>
        /// Helper method that returns whether one object is looking at another
        /// 1 = The two transforms are looking directly at each other
        /// 0 = The two trasforms are perpendicular to each other
        /// -1 = The Two transforms are looking away from each other
        /// </summary>
        /// <param name="currentTransform"></param>
        /// <param name="target"></param>
        /// <param name="threshold"></param>
        /// <returns></returns>
        public static bool IsLookingAtTarget(Transform currentTransform, Transform target, float threshold)
        {
            if (target == null) return false;

            Vector3 forward = currentTransform.forward.normalized;

            // Direction vector to the target
            Vector3 toTarget = (target.position - currentTransform.position).normalized;

            // Compute the dot product
            float dot = Vector3.Dot(forward, toTarget);

            // Check if the dot product is above the threshold
            return dot > threshold;
        }
    }

}
