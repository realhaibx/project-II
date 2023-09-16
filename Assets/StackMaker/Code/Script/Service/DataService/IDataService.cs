namespace StackMaker.Code.Script.Service.DataService
{
    public interface IDataService
    {
        #region FUNCTIONS

        #region USER DEFINED PUBLIC

        bool SaveData<T>(string relativePath, T data, bool isEncrypt);

        T LoadData<T>(string relativePath, bool isEncrypt);

        #endregion

        #endregion
    }
}