using StackMaker.Code.Script.Value;
using UnityEngine;

namespace StackMaker.Code.Script.Model.Player
{
    public class PlayerInput : MonoBehaviour
    {
        #region VARIABLES

        #region PRIVATE

        private bool tap;
        private bool isDraging; // Is drag or not
        private Vector2 startTouch, swipeDelta;

        private static readonly float MIN_TOUCH = Values.Game.Config.Touch.SENSITIVE;
        private static readonly int INPUT = 0; // Do not change
        
        private Player player;
        
        #endregion

        #region PUBLIC

        public Enums.Direction MovingDirection { get; private set; } // Player direction
        public bool IsDragAble = true; //

        #endregion

        #region SERIALIZATION

        #endregion

        #endregion

        #region FUNCTIONS

        #region USER DEFINED PRIVATE

        /// <summary>
        /// Handle user input, add to this if you want to add touch input handler, excute every frame
        /// </summary>
        private void HandleInput()
        {
            HandleStandaloneInput();
            HandleMobileInput();
        }

        /// <summary>
        /// Handle stand alone input ( mouse or virtual touch )
        /// </summary>
        private void HandleStandaloneInput()
        {
            if (Input.GetMouseButtonDown(INPUT))
            {
                tap = true;
                isDraging = true;
                startTouch = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(INPUT))
            {
                isDraging = false;
                Reset();
            }
        }

        /// <summary>
        /// Handle touch input ( Mobile device and touch screen input )
        /// </summary>
        private void HandleMobileInput()
        {
            if (Input.touches.Length > INPUT)
            {
                if (Input.touches[INPUT].phase == TouchPhase.Began)
                {
                    tap = true;
                    isDraging = true;
                    startTouch = Input.touches[INPUT].position;
                }
                else if (Input.touches[INPUT].phase == TouchPhase.Ended || Input.touches[INPUT].phase == TouchPhase.Canceled)
                {
                    isDraging = false;
                    Reset();
                }
            }
        }

        /// <summary>
        /// Calculate the distance of touch
        /// </summary>
        private void CalculateDistance()
        {
            swipeDelta = Vector2.zero;
            if (isDraging)
            {
                if (Input.touches.Length < INPUT)
                    swipeDelta = Input.touches[INPUT].position - startTouch;
                else if (Input.GetMouseButton(INPUT))
                    swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        /// <summary>
        /// Decide which direction to move on
        /// </summary>
        private void HandleMovingDirection()
        {
            if (!(swipeDelta.magnitude > MIN_TOUCH)) return;
            //Which direction?
            var x = swipeDelta.x;
            var y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or Right
                MovingDirection = x < 0 ? Enums.Direction.Left : Enums.Direction.Right;
            }
            else
            {
                //Up or Down
                MovingDirection = y < 0 ? Enums.Direction.Backward : Enums.Direction.Forward;
            }

            if (!player.Movement.IsMoving && IsDragAble)
            {
                player.Action.Move(MovingDirection);
            }

            // Reset to a new touch
            Reset();
        }
        
        /// <summary>
        /// Reset touch after calculate the distance
        /// </summary>
        private void Reset()
        {
            startTouch = swipeDelta = Vector2.zero;
            isDraging = false;
        }

        /// <summary>
        /// Reset tap every frame
        /// </summary>
        private void ResetTouch()
        {
            tap = false;
        }
        
        #endregion
        
        #region UNITY

        private void Start()
        {
            player = GetComponent<Player>();    
        }
        
        private void Update()
        {
            ResetTouch();
            HandleInput();
            CalculateDistance();
            HandleMovingDirection();
        }
        
        #endregion

        #endregion

        


        
    }
}