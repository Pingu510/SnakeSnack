using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
//using System.Timers;
using System.Windows.Threading;

namespace SnakeSnack
{
    public class GameHandler
    {
        public DispatcherTimer GameTurn = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(500), IsEnabled = false }; //0.5s
        Random rand = new Random(1123451);
        Snake snake;

        public GameHandler()
        {
            snake = new Snake();
        }
        //public void MakeGameField()
        //{
        //    snake = new Snake();
        //}

        public void StartNewGame()
        {
            snake = new Snake();
            snake.StartTime = DateTime.Now;
            GameTurn.Start();
        }

        public void EraseSnake()
        {
            snake = null;
        }


        public void ChangeDirection(string key)
        {
            if (!(key == "Up" || key == "Down" || key == "Left" || key == "Right")) return;

            Snake.Direction direction = Snake.Direction.Right;
            switch (key)
            {
                case "Up":
                    direction = Snake.Direction.Up;
                    break;
                case "Down":
                    direction = Snake.Direction.Down;
                    break;
                case "Left":
                    direction = Snake.Direction.Left;
                    break;
                case "Right":
                    direction = Snake.Direction.Right;
                    break;
            }

            snake.ChangeDirection(direction);
        }

        public int GetSnakeSize()
        {
            return snake.Size;
        }

        public string GetSnakeDirection()
        {
            var dir = snake.CurrentDirection;
            return dir.ToString();
        }

        public (int, int) GetFoodCordinates(int fieldWidth, int fieldHeight)
        {
            var x = rand.Next(fieldWidth);
            var y = rand.Next(fieldHeight);
            return (x, y);
        }

    }
}
