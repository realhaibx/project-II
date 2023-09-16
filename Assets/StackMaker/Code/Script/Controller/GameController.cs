using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using StackMaker.Code.Script.Model.Data;
using StackMaker.Code.Script.Model.Player;
using StackMaker.Code.Script.Value;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace StackMaker.Code.Script.Controller
{
    public class GameController : MonoBehaviour
    {
        #region VARIABLES

        #region PRIVATE

        private int level;
        private readonly int TOTAL_LEVEL = Values.Game.Config.Level.MAX_LEVEL;
        
        #endregion

        #region PUBLIC

        public GameController Instance {get; set; }

        #endregion

        #region SERIALIZATION

        [Tooltip("Win menu")] [Description("Win menu")] [SerializeField]
        private GameObject winMenu;

        [SerializeField]
        private TMP_Text winCoin;
        
        [SerializeField]
        private TMP_Text stack;
        
        [Tooltip("Lose menu")] [Description("Lose script")] [SerializeField]
        private GameObject loseMenu;

        [FormerlySerializedAs("replayMenu")] [SerializeField]
        private GameObject overLay;
        
        [Tooltip("Level Controller")] [Description("Level Controller")] [SerializeField]
        private LevelController levelController;

        private PlayerData data = new PlayerData();

        #endregion

        #endregion

        #region FUNCTIONS

        #region USER DEFINED PRIVATE

        #endregion

        #region USER DEFINED PUBLIC

        public void LoadGame(int id)
        {
            data.SaveData();
            overLay.SetActive(false);
            loseMenu.SetActive(false);
            winMenu.SetActive(false);
            levelController.LoadScene(id);
        }

        public void ResetGame()
        {
            level = data.LoadData().Level;
            LoadGame(level);
            overLay.SetActive(false);
        }

        public void LoseGame(int id)
        {
            overLay.SetActive(false);
            level = id;
            loseMenu.SetActive(true);
        }

        public void WinGame(int id, int stackAmount, int coin)
        {
            overLay.SetActive(false);
            level = id;
            winMenu.SetActive(true);
            var goldAmount = coin * Values.Game.Config.Reward.MUL_REWARD + Values.Game.Config.Reward.BASE_REWARD;
            data.AddGold(goldAmount);
            stack.SetText($"{stackAmount.ToString()}");
            winCoin.SetText(goldAmount.ToString());
        }

        public void NextGame()
        {
            if (level >= TOTAL_LEVEL)
            {
                ResetGame();
            }
            else
            {
                data.NextLevel();
                LoadGame(level + 1);
            }
            data.SaveData();
        }
        
        public void StartGame()
        {
            level = new PlayerData().LoadData().Level;
            LoadGame(level);
        }
        
        #endregion

        #region UNITY

        private void Start()
        {
            overLay.SetActive(true);
            data = data.LoadData();
        }

        #endregion

        #endregion
    }
}