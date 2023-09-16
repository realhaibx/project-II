using System.Collections;
using System.Collections.Generic;
using StackMaker.Code.Script.Helper;
using UnityEngine;
using StackMaker.Code.Script.Model.Data;
using StackMaker.Code.Script.Model.Stack;

namespace StackMaker.Code.Script.Model.Player
{
    public class Player : MonoBehaviour
    {
        #region VARIABLES

        #region PRIVATE

        private LogHelper logger = new LogHelper();
        public PlayerData Data { get; set; }

        
        #endregion

        #region PUBLIC

        public int CollectedStack = 0;
        
        public PlayerAnimation Animation { get; set; }
        public PlayerMovement Movement { get; set; }
        
        public PlayerAction Action {get; set; }
        public PlayerInput Input { get; set; }
        
        #endregion

        #region SERIALIZATION

        [SerializeField]
        [Tooltip("The name of the player.")]
        public StackTower Tower;
        
        #endregion

        #endregion

        #region FUNCTIONS

        #region USER DEFINED PRIVATE

        #endregion

        #region USER DEFINED PUBLIC

        public int GetLevel()
        {
            return Data.Level;
        }

        public int GetGold()
        {
            return Data.Gold;
        }

        public void ReloadData()
        {
            Data = Data.LoadData();
        }
        #endregion

        #region UNITY

        // Call on game start
        private void Awake()
        {
            Animation = GetComponent<PlayerAnimation>();
            Action = GetComponent<PlayerAction>();
            Movement = GetComponent<PlayerMovement>();
            Input = GetComponent<PlayerInput>();
            Data = new PlayerData().LoadData();
        }

        private void Update()
        {
            
        }
        #endregion

        #endregion
    }
}