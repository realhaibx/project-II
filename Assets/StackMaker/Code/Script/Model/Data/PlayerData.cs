using Newtonsoft.Json;
using StackMaker.Code.Script.Service.DataService;
using StackMaker.Code.Script.Value;

namespace StackMaker.Code.Script.Model.Data
{
    public class PlayerData
    {
        #region VARIABLES

        #region PRIVATE

        private bool isEncrypted = Values.Player.Config.ENCRYPT_DATA;
        private static JsonDataService dataService = new JsonDataService();
        private string dataPath = Values.Player.Path.STAT_PATH;

        #endregion
        
        #region PUBLIC

        [JsonProperty(PropertyName = "Gold")]
        public int Gold { get; private set; }
        
        [JsonProperty(PropertyName = "Level")]
        public int Level { get; private set; }
        
        #endregion

        #endregion

        #region FUNCTIONS

        #region USER DEFINED PRIVATE

        private void Init()
        {
            dataPath = dataService.AbsolutePathOf(dataPath);
        }
        
        private void CreateNewData()
        {
            Gold = Values.Player.Stat.DEFAULT_GOLD;
            Level = Values.Player.Stat.DEFAULT_LEVEL;
            SaveData();
        }
        
        private void AddLevel(int amount)
        {
            Level += amount;
        }
        
        #endregion

        #region USER DEFINED PUBLIC

        public PlayerData LoadData()
        {
            var data = dataService.LoadData<PlayerData>(dataPath,isEncrypted);
            if (data != null)
            {
                Gold = data.Gold;
                Level = data.Level;
            }
            else
            {
                CreateNewData();
            }
            return this;
        }

        public bool SaveData()
        {
            return dataService.SaveData(dataPath, this, isEncrypted);
        }

        public void AddGold(int amount)
        {
            Gold += amount;
        }

        public void NextLevel()
        {
            Level++;
        }

        public void PreviousLevel()
        {
            Level--;
        }

        public PlayerData()
        {
            this.Gold = Values.Player.Stat.DEFAULT_GOLD;
            this.Level = Values.Player.Stat.DEFAULT_LEVEL;
        }

        public PlayerData(int gold, int level)
        {
            this.Gold = gold;
            this.Level = level;
        }
        
        #endregion

        #region UNITY

        private void Start()
        {
            Init();
        }        

        #endregion
        
        #endregion
    }
}