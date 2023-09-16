using System;
using StackMaker.Code.Script.Value;
using UnityEngine;

namespace StackMaker.Code.Script.Helper
{
    public class RaycastHelper
    {
        #region VARIABLES

        #region PRIVATE

        private VectorHelper vectorHelper = new VectorHelper();
        private LogHelper logger = new LogHelper();
        private bool IS_TRIGGER = Values.Game.Config.Raycast.ENABLE_RAYCAST_TRIGGER;
        private float DISTANCE = Values.Game.Config.Raycast.RAYCAST_DISTANCE;
        private LayerMask LAYER;
        private bool isHit;
        
        #endregion

        #endregion

        #region FUNCTIONS

        #region USER DEFINED PRIVATE

        private RaycastHit RayCastInDirection(Vector3 position, Vector3 direction, float distance, LayerMask mask)
        {
            RaycastHit hit = default(RaycastHit);
            isHit = Physics.Raycast(position, direction, out hit, distance, mask);
            if (isHit)
            {
                logger.Draw(position, direction);
            }
            
            return hit;
        }
        
        #endregion

        #region USER DEFINED PUBLIC

        public Vector3 GetTargetPosition(Vector3 position, Enums.Direction direction)
        {
            Vector3 vectorDirection = vectorHelper.GetDirectionVector(direction);
            LAYER =  LayerMask.NameToLayer(Values.Game.Config.Raycast.LAYER);
            RaycastHit hit = RayCastInDirection(position, vectorDirection, DISTANCE, 1 << LayerMask.NameToLayer("Wall"));
            if (isHit)
                return hit.collider.transform.position;
            else
            {
                return position;
            }
            
        }
        
        #endregion

        #region UNITY
        
        #endregion

        #endregion
    }
}