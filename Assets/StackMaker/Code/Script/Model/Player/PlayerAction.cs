using System;
using System.Collections;
using System.Collections.Generic;
using StackMaker.Code.Script.Controller;
using StackMaker.Code.Script.Helper;
using StackMaker.Code.Script.Value;
using UnityEngine;

namespace StackMaker.Code.Script.Model.Player
{
    public class PlayerAction : MonoBehaviour
    {
        #region VARIABLES

        #region PRIVATE

        private const string WALL = "Wall";
        private const string STACK = "Collectible";
        private const string FILL = "Fill";
        private const string FINISH = "FinishLine";
        private const string CHEST = "Chest";

        private float JUMP_TIME = Values.Game.Config.Time.JUMP;

        private static LogHelper logger = new LogHelper();
        private readonly VectorHelper vectorHelper = new VectorHelper();
        private readonly RaycastHelper raycastHelper = new RaycastHelper();
        


        #endregion
        
        #region PUBLIC
        
        public Player player;
        
        #endregion

        #region SERIALIZE FIELD

        [SerializeField] public GameController controller;
        [SerializeField] public Transform playerTransform;

        #endregion

        #endregion

        #region FUNCTIONS

        #region USER DEFINED PRIVATE

        private void Init()
        {
            player = GetComponent<Player>();
        }

        void UpdateHeight(bool isUp)
        {
            playerTransform.position +=
                new Vector3(0, Values.Stack.Attribute.THICKNESS * (isUp ? 1 : -1), 0);
        }

        void TakeStack(GameObject obj)
        {
            Jump();
            player.CollectedStack++;
            UpdateHeight(true);
            obj.transform.GetComponent<BoxCollider>().enabled = false;
            player.Tower.AddStack(obj);
        }

        void MoveToChest()
        {
            Cheer();
        }

        void DropStack(GameObject obj)
        {
            Jump();
            if (player.Tower.StackCount == 0)
            {
                Stop(transform.position);

                player.Input.IsDragAble = false;
                
                if (obj.transform.parent.CompareTag(FINISH))
                {
                    controller.WinGame(player.GetLevel(), player.CollectedStack, player.Tower.StackCount);
                }
                else
                {
                    controller.LoseGame(player.GetLevel());
                }
                return;
            }

            obj.transform.GetComponent<BoxCollider>().enabled = false;
            obj.transform.GetChild(0).gameObject.SetActive(false);
            obj.transform.GetChild(1).gameObject.SetActive(true);
            UpdateHeight(false);
            player.Tower.RemoveStack();
        }

        void Jump()
        {
            if (IsCheering())
            {
                return;
            }

            player.Animation.ChangeAnimation(Enums.PlayerState.Jump);
            StartCoroutine(DoJump());
        }

        public void Idle()
        {
            if (IsCheering())
            {
                return;
            }

            player.Animation.ChangeAnimation(Enums.PlayerState.Idle);
        }
        
        void Cheer()
        {
            player.Data.SaveData();
            player.Input.IsDragAble = false;
            player.Animation.ChangeAnimation(Enums.PlayerState.Cheer);
        }

        public void Stop(Vector3 pos)
        {
            player.Movement.MoveTarget = transform.position;
            player.Movement.MoveDirection = Enums.Direction.None;
            player.transform.position = pos;
        }

        IEnumerator DoJump()
        {
            WaitForSeconds wait = new WaitForSeconds(JUMP_TIME);
            yield return wait;
            Idle();
        }

        #endregion

        #region USER DEFINED PUBLIC

        public bool IsCheering()
        {
            return player.Animation.CurrentState == Enums.PlayerState.Cheer;
        }
        
        public void Move(Enums.Direction direction)
        {

            var target = player.Movement.GetTargetPosition(direction);
            if (vectorHelper.IsApproximate(transform.position, target))
            {
                return;
            }
            
            player.Movement.MoveDirection = direction;
            player.Movement.MoveTarget = target;
        }
        
        #endregion

        #region UNITY

        private void Start()
        {
            Init();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.transform.tag)
            {
                case STACK:
                    TakeStack(other.gameObject);
                    break;
                case FILL:
                    DropStack(other.gameObject);
                    break;
                case FINISH:
                    MoveToChest();
                    break;
                case CHEST:
                    Win();
                    break;
            }
        }

        private void Win()
        {
            player.Input.IsDragAble = false;
            playerTransform.position -= new Vector3(0, player.Tower.StackCount * Values.Stack.Attribute.THICKNESS, 0);
            controller.WinGame(player.GetLevel(), player.CollectedStack, player.Tower.StackCount);
            player.Tower.RemoveAllStack();
            player.Data.SaveData();
        }

        #endregion

        #endregion
    }
}