using StackMaker.Code.Script.Controller;
using UnityEngine;

namespace StackMaker.Code.Script
{
    public class StartGame : MonoBehaviour
    {
        [SerializeField] GameController controller;

        private void Awake()
        {
            controller.StartGame();
        }
    }
}