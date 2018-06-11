using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using SnakeSnack.ViewModels.Commands;
using SnakeSnack.Models;

namespace SnakeSnack.ViewModels
{

    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> HighScoreCollection { get; set; }
        public ObservableCollection<SnakeBodyPart> CircleBodyParts { get; set; }      


        #region Commands
        public ICommand Btn_Username_Confirmed_Clicked_Command { get; set; }
        public ICommand Btn_Start_Game_Clicked_Command { get; set; }
        public ICommand Direction_Key_Pressed_Command { get; set; }

        #endregion

        #region Properties
        //private propeties
        private GameHandler gh;
        private string _btn_Start_Game = "Start Game!";
        private string _userName;
        private bool _userNameEnabled = true;
        private int _canvasHeight = 400;
        private int _canvasWidth = 630;


        public string Btn_Start_Game
        {
            get { return _btn_Start_Game; }
            set
            {
                _btn_Start_Game = value;
                OnPropertyChanged("Btn_Start_Game");
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged("UserNme");
            }
        }

        public bool UserNameEnabled
        {
            get { return _userNameEnabled; }
            set
            {
                _userNameEnabled = value;
                OnPropertyChanged("UserNameEnabled");
            }
        }


        public int CanvasHeight
        {
            get { return _canvasHeight; }
            set
            {
                _canvasHeight = value;
                OnPropertyChanged("CanvasHeight");
            }
        }

        public int CanvasWidth
        {
            get { return _canvasWidth; }
            set
            {
                _canvasWidth = value;
                OnPropertyChanged("CanvasWidth");
            }
        }


        #endregion


        private void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }

        public MainViewModel()
        {
            Btn_Username_Confirmed_Clicked_Command = new DelegateCommand(LockUserName, CanExecuteMethod);
            Btn_Start_Game_Clicked_Command = new DelegateCommand(StartNewGame, CanExecuteMethod);
            Direction_Key_Pressed_Command = new DelegateCommand(Window_KeyDown, CanExecuteMethod);
            CircleBodyParts = new ObservableCollection<SnakeBodyPart>();

            gh = new GameHandler();
            SetupNewField();
            gh.GameTurn.Tick += GameTurn_Elapsed;
        }


        private bool CanExecuteMethod(object parameter)
        {
            return true;
        }

        private void Window_KeyDown(object keyPressed)
        {
            gh.ChangeDirection(keyPressed as string);
        }

        private void LockUserName(object property)
        {
            UserNameEnabled = !UserNameEnabled;
        }

        private void GameTurn_Elapsed(object sender, EventArgs e)
        {
            MakeMove();
        }

        private void GameOver()
        {
            StopGame();
            CircleBodyParts = new ObservableCollection<SnakeBodyPart>();
            SetupNewField();
        }

        public void StartNewGame(object property)
        {
            if (Btn_Start_Game != "Start Game!")
                StopGame();
            
            gh.StartNewGame();
        }

        /// <summary>
        /// Sets up the Starting Field
        /// </summary>
        public void SetupNewField()
        {
            var x = Math.Floor(CanvasWidth / 2d);
            var y = Math.Floor(CanvasHeight / 2d);
            CircleBodyParts.Add(new SnakeBodyPart(x, y));

            for (var i = gh.GetSnakeSize() - 1; i > 0; i--)
            {
                AddNewSnakeBodyPart();
            }
        }


        private bool AddNewSnakeBodyPart()
        {
            double newX = 0;
            double newY = 0;
            var u = CircleBodyParts.Count - 1;
            var directon = gh.GetSnakeDirection();

            switch (directon)
            {
                case "Right":
                    newX = CircleBodyParts[u].X + (SnakeBodyPart.SnakePartSize - 1);
                    newY = CircleBodyParts[u].Y;
                    break;

                case "Left":
                    newX = CircleBodyParts[u].X - (SnakeBodyPart.SnakePartSize - 1);
                    newY = CircleBodyParts[u].Y;
                    break;

                case "Up":
                    newX = CircleBodyParts[u].X;
                    newY = CircleBodyParts[u].Y - (SnakeBodyPart.SnakePartSize - 1);
                    break;

                case "Down":
                    newX = CircleBodyParts[u].X;
                    newY = CircleBodyParts[u].Y + (SnakeBodyPart.SnakePartSize - 1);
                    break;
            }
            if ((newX <= 0 || newX >= CanvasWidth) || newY <= 0 || newY >= CanvasHeight)
            {
                GameOver();
                return false;
            }
            else
            {
                CircleBodyParts.Add(new SnakeBodyPart(newX, newY));
                return true;
            }
        }

        /// <summary>
        /// Stops the timer, saves the score and resets PlayingField
        /// </summary>
        public void StopGame()
        {
            gh.GameTurn.Stop();
            // save HighScore etc
            // new snake/field
            Btn_Start_Game = "Start Game!";
            LockUserName(null);
        }

        /// <summary>
        /// Triggers when GameTurn(Timer) Elapses
        /// </summary>
        public bool MakeMove()
        {
            if(AddNewSnakeBodyPart())
                DeleteLastSnakePart();

            //update GUI score etc

            return true;
        }


        public bool DeleteLastSnakePart()
        {
            try
            {
                CircleBodyParts.RemoveAt(0);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
