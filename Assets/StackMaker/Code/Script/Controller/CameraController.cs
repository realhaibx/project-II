using System.Collections;
using System.Collections.Generic;
using StackMaker.Code.Script.Helper;
using StackMaker.Code.Script.Model.Player;
using StackMaker.Code.Script.Value;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region VARIABLES

    #region PRIVATE

    private static Vector3 START_OFFSET = Values.Game.Config.Camera.START_OFFSET;
    private static Vector3 FINISH_OFFSET = Values.Game.Config.Camera.FINISH_OFFSET;
    private static float SPEED = Values.Game.Config.Camera.SPEED;
    private LogHelper logger = new LogHelper();

    #endregion

    #region PUBLIC

    #endregion

    #region SERIALIZATION
    [SerializeField] private PlayerAction target;
    [SerializeField] private Vector3 startOffset = START_OFFSET;
    [SerializeField] private Vector3 finishOffset = FINISH_OFFSET;
    [SerializeField] private float speed = SPEED;
    
    #endregion

    #endregion

    #region FUNCTIONS

    #region USER DEFINED PRIVATE

    private void FollowPlayer()
    {
        if (!target.IsCheering())
        {
            Vector3 offset = new Vector3(0, Values.Stack.Attribute.THICKNESS, -Values.Stack.Attribute.THICKNESS/2) *
                             target.player.Tower.StackCount;
            
            transform.position =
                Vector3.Lerp(transform.position, target.transform.position + offset + startOffset, 2 * Time.fixedDeltaTime * speed);
        }
        else
        {
            Quaternion rotate = Quaternion.LookRotation(target.transform.position - transform.position );  
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotate, 2 * Time.fixedDeltaTime * speed);
            transform.position = Vector3.Lerp(transform.position, target.transform.position + finishOffset + target.player.Tower.StackCount * new Vector3 (0, 0.3f, 0), 2 * Time.fixedDeltaTime * speed);
        }
    }
    
    #endregion

    #region USER DEFINED PUBLIC

    #endregion

    #region UNITY

    void Update()
    {
        FollowPlayer();
    }
    
    #endregion

    #endregion
}
