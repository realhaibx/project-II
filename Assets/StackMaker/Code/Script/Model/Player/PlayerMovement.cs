using System.ComponentModel;
using StackMaker.Code.Script.Helper;
using StackMaker.Code.Script.Value;
using UnityEngine;

namespace StackMaker.Code.Script.Model.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        #region VARIABLES

        #region PRIVATE

        private static readonly float SPEED = Values.Player.Stat.DEFAULT_SPEED;
        private readonly VectorHelper vectorHelper = new VectorHelper();
        private readonly RaycastHelper raycastHelper = new RaycastHelper();
        private PlayerAction playerAction;
        
        #endregion

        #region PUBLIC
        
        public float Speed
        {
            get => speed;
            set => value = speed;
        }

        public static VectorHelper vectors = new VectorHelper();
        public Enums.Direction MoveDirection { get; set; }
        public Vector3 MoveTarget { get; set; }
        public bool IsMoving => MoveDirection != Enums.Direction.None;
        public LogHelper logger = new LogHelper();


        #endregion

        #region SERIALIZATION
        
        [Tooltip("Speed")] [Description("Player speed")] [SerializeField]
        private float speed = SPEED;
        
        #endregion

        #endregion

        #region FUNCTIONS

        #region USER DEFINED PRIVATE

        void Init()
        {
            MoveTarget = transform.position;
            playerAction = transform.GetComponent<PlayerAction>();
        }

        void MoveToTarget()
        {
            transform.position = Vector3.MoveTowards(transform.position, MoveTarget, Time.fixedDeltaTime * speed);
            if (vectorHelper.IsApproximate(transform.position, MoveTarget))
            {
                StopMoving();
            }
        }

        void StopMoving()
        {
            MoveDirection = Enums.Direction.None;
        }
        
        #endregion

        #region USER DEFINED PUBLIC

        public Vector3 GetTargetPosition(Enums.Direction direction)
        {
            Vector3 directionVector = vectorHelper.GetDirectionVector(direction);
            var position = transform.position;
            Vector3 hitPos = raycastHelper.GetTargetPosition(position + Vector3.up * 0.1f, direction);
            return hitPos - Values.Stack.Attribute.WIDTH * directionVector - Vector3.up * 0.375f;
        }
        
        #endregion

        #region UNITY

        private void Start()
        {
            Init();
        }

        private void FixedUpdate()
        {
            if (MoveDirection != Enums.Direction.None)
            {
                MoveToTarget();
            }
            else
            {
                if (!playerAction.IsCheering())
                    playerAction.Idle();
            }
        }

        #endregion

        #endregion
    }
}