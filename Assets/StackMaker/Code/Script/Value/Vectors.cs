using UnityEngine;

namespace StackMaker.Code.Script.Value
{
    public static class Vectors
    {
        public static readonly Vector3[] DIRECTION_VECTOR;

        static Vectors()
        {
            DIRECTION_VECTOR = new[] {
                Vector3.forward,
                Vector3.left,
                Vector3.back,
                Vector3.right,
                Vector3.zero,
            };
        }
    }
}