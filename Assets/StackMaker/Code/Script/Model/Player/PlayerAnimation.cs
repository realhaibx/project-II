using StackMaker.Code.Script.Value;
using UnityEngine;

namespace StackMaker.Code.Script.Model.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        #region VARIABLES

        #region PUBLIC

        public Enums.PlayerState CurrentState { get; private set; }

        #endregion

        #region SERIALIZATION

        [SerializeField] public Animator animator;

        #endregion

        #endregion

        #region FUNCTIONS

        #region USER DEFINED PUBLIC

        /// <summary>
        /// Change current animation
        /// </summary>
        /// <param name="newState">New animation</param>
        public void ChangeAnimation(Enums.PlayerState newState)
        {
            animator.SetInteger("state", (int)newState);
            CurrentState = newState;
        }
        
        #endregion
        
        #region UNITY

        #endregion

        #endregion

    }
}