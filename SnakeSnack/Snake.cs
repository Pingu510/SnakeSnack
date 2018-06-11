using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeSnack
{
    public class Snake
    {
        public bool Alive { get; set; }
        public int Score { get; set; }
        public int Size { get; set; }
                
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Direction CurrentDirection { get; set; }

        /// <summary>
        /// Possible directions the sake can move
        /// </summary>
        public enum Direction
        {
            Right,
            Left,
            Up,
            Down
        }

        public Snake()
        {
            ResetSnake();
        }

        /// <summary>
        /// Resets Snake to default values.
        /// </summary>
        public void ResetSnake()
        {
            Alive = true;
            Score = 0;
            Size = 4;
            StartTime = DateTime.Now;
            CurrentDirection = Direction.Right;
        }

        /// <summary>
        /// Changes to the new direction if possible
        /// </summary>
        public bool ChangeDirection(Direction newDirection)
        {
            switch (newDirection)
            {
                case Direction.Right:
                    if (CurrentDirection == Direction.Left) return false;
                    break;
                case Direction.Left:
                    if (CurrentDirection == Direction.Right) return false;
                    break;
                case Direction.Up:
                    if (CurrentDirection == Direction.Down) return false;
                    break;
                case Direction.Down:
                    if (CurrentDirection == Direction.Up) return false;
                    break;
            }

            CurrentDirection = newDirection;
            return true;
        }

    }
}
