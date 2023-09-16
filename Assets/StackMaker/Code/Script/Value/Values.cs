using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using StackMaker.Code.Script.Value;
using UnityEngine;

namespace StackMaker.Code.Script.Value
{
    public static class Values
    {
        public static class Game
        {
            public static class Config
            {
                public static class Global
                {
                    public static readonly bool ENABLE_LOG_DEBUG = true;
                    public static readonly bool ENABLE_DRAW_DEBUG = false;
                    public static readonly bool DEBUG_DRAW_DEPTH  = true;
                    public static readonly float DEBUG_DRAW_TIME  = 2.0f;
                    public static readonly Color DEBUG_DRAW_COLOR = Color.green;
                }
                public static class Path
                {
                    public static readonly string DATA_PATH = "/Resouce";
                }

                public static class Distance
                {
                    public static readonly float DISTANCE_THRESHOLD = 0.001f;
                    public static readonly float DISTANCE_RAYCAST_LENGTH = 1000.0f;
                    public static readonly float DISTANCE_MAX_DIFFERENCE = 0.001f;
                }

                public static class Touch
                {
                    public static readonly bool ENABLE_TOUCH = true;
                    public static readonly float SENSITIVE = 10.0f;
                }

                public static class Raycast
                {
                    public static readonly float RAYCAST_DISTANCE = Distance.DISTANCE_RAYCAST_LENGTH;
                    public static readonly bool ENABLE_RAYCAST_TRIGGER = true;
                    public static readonly string LAYER = Layer.WALL;
                }

                public static class Camera
                {
                    public static readonly Vector3 START_OFFSET = new Vector3(2, 7, -5);
                    public static readonly Vector3 FINISH_OFFSET = new Vector3(0, 5, -5);
                    public static float SPEED = 1.0f;
                }

                public static class Time
                {
                    public static readonly float JUMP = 0.567f;
                    public static readonly float IDLE = 1.333f;
                    public static readonly float CHEER = 6.467f;
                }

                public static class Reward
                {
                    public static readonly int BASE_REWARD = 50;
                    public static readonly int MUL_REWARD = 2;
                }

                public static class Level
                {
                    public static readonly int MAX_LEVEL = 5;
                }
            }

            public static class Tag
            {
                public static readonly string STACK = "Collectible";
                public static readonly string WALL = "Wall";
                public static readonly string FILL = "Fill";
                public static readonly string FINISH = "FinishLine";
            }

            public static class Layer
            {
                public static readonly string WALL = "Wall";
            }
        }
        public static class DataService
        {
            public static readonly string DEFAULT_KEY = "ggdPhkeOoiv6YMiPWa34kIuOdDUL7NwQFg6l1DVdwN8=";
            public static readonly string DEFAULT_IV = "JZuM0HQsWSBVpRHTeRZMYQ==";
        }

        public static class Player
        {
            public class StateMachine
            {
                public static readonly Enums.PlayerState? NULL_STATE = null;
                public static readonly Enums.PlayerState DEFAULT_STATE = Enums.PlayerState.Idle;
            }

            public static class Stat
            {
                public static readonly int DEFAULT_GOLD = 0;
                public static readonly int DEFAULT_LEVEL = 1;
                public static readonly float DEFAULT_SPEED = 10.0f;
            }

            public static class Path
            {
                private static readonly string DATA_PATH = $"{Game.Config.Path.DATA_PATH}/PlayerStat";
                public static readonly string STAT_PATH = $"/PlayerStat.json";
            }

            public static class Config
            {
                public static readonly bool ENCRYPT_DATA = true;
            }
        }

        public static class Stack
        {
            public static class Attribute
            {
                public static readonly float HEIGHT = 0.2f;
                public static readonly float THICKNESS = 0.3f;
                public static readonly float WIDTH = 1.0f;
            }
        }
    }
}