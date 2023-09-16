namespace StackMaker.Code.Script.Value
{
    public class Enums
    {
        public enum Direction
        {
            Forward,
            Left,
            Backward,
            Right,
            None
        }

        public enum PlayerState
        {
            Idle = 0,
            Jump = 1,
            Move = 1,
            Cheer = 2,
            Paused = 3
        }

        public enum PusherState
        {
            Idle = 0,
            Bouncing = 1,
            Paused = 2
        }
    }
}