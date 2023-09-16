using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StackMaker.Code.Script.Misc
{
    public class DoNotDestroyOnLoad : MonoBehaviour
    {  
        #region FUNCTIONS

        #region UNITY

        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        
        #endregion

        #endregion
    }
}