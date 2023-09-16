using System;
using System.Numerics;
using StackMaker.Code.Script.Value;
using static UnityEngine.Vector3;
using Vector3 = UnityEngine.Vector3;

namespace StackMaker.Code.Script.Helper
{
    public class VectorHelper
    {
        #region VARIABLES

        #region PRIVATE

        private static float MAX_ACCEPTANCE = Values.Game.Config.Distance.DISTANCE_MAX_DIFFERENCE;
        
        #endregion

        #endregion

        #region FUNCTIONS

        #region USER DEFINED PUBLIC

        /// <summary>
        /// Get direction vector 
        /// </summary>
        /// <param name="direction">Direction</param>
        /// <returns></returns>
        public Vector3 GetDirectionVector(Enums.Direction direction)
        {
            return Vectors.DIRECTION_VECTOR[(int) direction];
        }

        public Enums.Direction GetDirection(Vector3 vector3)
        {
            foreach (Enums.Direction direction in (Enums.Direction[])Enum.GetValues(typeof(Enums.Direction)))
            {
                if (IsApproximate(vector3, GetDirectionVector(direction)))
                {
                    return direction;
                }
            }
            
            return Enums.Direction.None;
        }

        /// <summary>
        /// Get the approximate distance
        /// </summary>
        /// <param name="vectorA"></param>
        /// <param name="vectorB"></param>
        /// <returns></returns>
        public bool IsApproximate(Vector3 vectorA, Vector3 vectorB)
        {
            Vector3 subVector = vectorA - vectorB;
            return Math.Abs(subVector.magnitude) < MAX_ACCEPTANCE;
        }
        
        #endregion

        #endregion
    }
}