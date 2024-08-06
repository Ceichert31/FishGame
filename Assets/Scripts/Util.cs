using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelperMethods
{
    public static class Util
    {
        /// <summary>
        /// Returns the distance of two vectors with a y of 0
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static float DistanceNoY(Vector3 first, Vector3 second)
        {
            return Vector3.Distance(new Vector3(first.x, 0, first.z), new Vector3(second.x, 0, second.z));
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
            return Vector3.Distance(new Vector3(first.x, differentY, first.z), new Vector3(second.x, differentY, second.z));
        }
    }

}
