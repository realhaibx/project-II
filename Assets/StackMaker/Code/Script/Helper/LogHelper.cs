using System.Globalization;
using StackMaker.Code.Script.Value;
using UnityEngine;

namespace StackMaker.Code.Script.Helper
{
    public class LogHelper
    {
        #region VARIABLES

        #region PRIVATE

        private static readonly bool LOG_DEBUG_ENABLED = Values.Game.Config.Global.ENABLE_LOG_DEBUG;
        private static readonly bool DRAW_DEBUG_ENABLED = Values.Game.Config.Global.ENABLE_DRAW_DEBUG;
        private static readonly float DRAW_DEBUG_TIME = Values.Game.Config.Global.DEBUG_DRAW_TIME;
        private static readonly bool DRAW_DEBUG_DEPTH = Values.Game.Config.Global.DEBUG_DRAW_DEPTH;
        private static readonly Color DRAW_DEBUG_COLOR = Values.Game.Config.Global.DEBUG_DRAW_COLOR;
        
        #endregion

        #endregion

        #region FUNCTIONS

        #region USER DEFINED PRIVATE

        private void DrawLine3(Vector3 startPos, Vector3 endPos, Color color, float timeStep, bool isDepth)
        {
            if (DRAW_DEBUG_ENABLED)
            {
                Debug.DrawLine(startPos,endPos,color,timeStep,isDepth);
                Log($"Draw {startPos.ToString()} - {endPos.ToString()} with {color.ToString()}");
            }
        }

        private void LogToConsole(string s)
        {
            if (LOG_DEBUG_ENABLED)
            {
                Debug.Log($"[{GetCurrentTime()}] : {s}");
            }
        }

        private static string GetCurrentTime()
        {
            return System.DateTime.Now.ToString(CultureInfo.InvariantCulture);
        }
        
        #endregion

        #region USER DEFINED PUBLIC

        public void Log(string s)
        {
            LogToConsole(s);
        }

        public void Draw(Vector3 start, Vector3 end)
        {
            DrawLine3(start, end, DRAW_DEBUG_COLOR, DRAW_DEBUG_TIME, DRAW_DEBUG_DEPTH);         
        }
        
        #endregion

        #endregion
    }
}