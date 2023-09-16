using System.Collections;
using System.Collections.Generic;
using StackMaker.Code.Script.Helper;
using StackMaker.Code.Script.Value;
using UnityEngine;

namespace StackMaker.Code.Script.Model.Stack
{
    public class StackTower : MonoBehaviour
    {
        #region VARIABLES

        #region PRIVATE

        private Stack<GameObject> stacks;
        private LogHelper logger = new LogHelper();

        #endregion

        #region PUBLIC
        
        public StackTower Instance {get; private set; }

        public int StackCount => stacks.Count - 1; // Return current stack in line

        #endregion
        
        #region SERIALIZE FIELD

        [SerializeField] private Transform stackTransform;        

        #endregion

        #endregion

        #region FUNCTIONS

        #region USER DEFINED PRIVATE

        /// <summary>
        /// Initial the stack tower
        /// </summary>
        private void Init()
        {
            Instance = this;
            stacks = new Stack<GameObject>();
            var stackObj = stackTransform.GetChild(0).gameObject;
            stackObj.GetComponent<BoxCollider>().enabled = false;
            stacks.Push(stackObj); // Push
            // logger.Log($" Start stack : {stacks.Count.ToString()}");
        }   

        #endregion

        #region USER DEFINED PUBLIC

        /// <summary>
        /// Add stack to tower, update tower height
        /// </summary>
        /// <param name="stack">Stack to collect</param>
        public void AddStack(GameObject stack)
        {
            #region ADD STACK TO TOP POSITION

            var topStackPos = stacks.Peek().transform.position;
            var position = topStackPos;

            var newStackPos = new Vector3(position.x  , position.y + Values.Stack.Attribute.THICKNESS, position.z);

            stack.transform.position = newStackPos;
            stack.transform.parent = stackTransform;
            stacks.Push(stack);

            #endregion

            #region DEBUG DATA

            // logger.Log($" Top stack position: {topStackPos.ToString()}");
            // logger.Log($" New stack position: {newStackPos.ToString()}");
            // logger.Log($" Added new stack, total stack: {stacks.Count.ToString()}");

            #endregion
        }

        /// <summary>
        /// Remove a stack from tower
        /// </summary>
        public void RemoveStack()
        {
            Destroy(stacks.Pop());
            // logger.Log($" Removed a stack, total stack: {stacks.Count.ToString()}");
        }

        public void RemoveAllStack()
        {
            while (StackCount > 0)
            {
                RemoveStack();
            }
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