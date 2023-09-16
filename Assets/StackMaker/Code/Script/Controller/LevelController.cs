using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace StackMaker.Code.Script.Controller
{
    public class LevelController : MonoBehaviour
    {
        
        #region VARIABLES

        #region PUBLIC

        public GameObject loadingScreen;
        public Image loadingBarFill;
        public float speed;
        
        #endregion

        #endregion

        #region FUNCTIONS

        #region USER DEFINED PRIVATE

        // Async load scene
        IEnumerator LoadSceneAsync(int sceneId)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

            loadingScreen.SetActive(true);

            while (!operation.isDone)
            {
                float progressValue = Mathf.Clamp01(operation.progress / speed);
                loadingBarFill.fillAmount = progressValue;

                yield return null;
            }
            
            loadingScreen.SetActive(false);
        }
        
        #endregion

        #region USER DEFINED PUBLIC

        /// <summary>
        /// Load scene with ID
        /// </summary>
        /// <param name="sceneId">Scene build ID</param>
        public void LoadScene(int sceneId)
        {
            StartCoroutine(LoadSceneAsync(sceneId));
        }

        #endregion

        #region UNITY

        #endregion

        #endregion
        
    }
}