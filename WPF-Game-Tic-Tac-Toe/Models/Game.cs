using System.Windows.Controls;
using Prism.Mvvm;

namespace WPF_Game_Tic_Tac_Toe.Models
{

    public class Game : BindableBase
    {
        public string FirstNickName { get; private set; } = "Player №1";
        public string SecondNickName { get; private set; } = "Player №2";
        public string NowWalking { get; private set; } = "Player №1";
        public int AllPlayedGames { get; private set; } = 0;
        public int FirstPlayerWins { get; private set; } = 0;
        public int SecondPlayerWins { get; private set; } = 0;
        public int GameTurnNumber { get; private set; } = 1;
        public Button[,] Buttons { get; private set; }

        public Game(int maxIndexButton)
        {
            Buttons = new Button[maxIndexButton, maxIndexButton];
        }

        public void SetFirstNickName(string value)
        {
            FirstNickName = value;
            SetCurrentNowWalking();
            RaisePropertyChanged("FirstNick");
        }

        public void SetSecondNickName(string value)
        {
            SecondNickName = value;
            SetCurrentNowWalking();
            RaisePropertyChanged("SecondNick");
        }

        public void AddPlayedGamesStats()
        {
            AllPlayedGames++;
            RaisePropertyChanged("AllPlayedGames");
        }

        public void AddFirstPlayerWin()
        {
            FirstPlayerWins++;
            RaisePropertyChanged("FirstPlayerWins");
        }

        public void AddSecondPlayerWin()
        {
            SecondPlayerWins++;
            RaisePropertyChanged("SecondPlayerWins");
        }

        public void AddGameTurnNumber()
        {
            GameTurnNumber++;
            RaisePropertyChanged("GameTurnNumber");
            SetCurrentNowWalking();
        }

        public void SetGameTurnNumber(int value)
        {
            GameTurnNumber = value;
            RaisePropertyChanged("GameTurnNumber");
            SetCurrentNowWalking();
        }

        public void SetButton(int row, int column, Button value)
        {
            Buttons[row, column] = value;
        }

        public void ZeroingButtons(int maxIndexButton)
        {
            for (var i = 0; i < maxIndexButton; i++)
            {
                for (var j = 0; j < maxIndexButton; j++)
                {
                    var button = Buttons[i, j];
                    if (button == null)
                        continue;

                    button.Content = null;
                    button.IsEnabled = true;
                    button = null;
                }
            }
        }

        private void SetCurrentNowWalking()
        {
            NowWalking = GameTurnNumber % 2 == 0 ? SecondNickName : FirstNickName;
            RaisePropertyChanged("NowWalking");
        }
    }
}