using System.Windows;
using System.Windows.Controls;
using Prism.Commands;
using Prism.Mvvm;
using WPF_Game_Tic_Tac_Toe.Models;
using WPF_Game_Tic_Tac_Toe.Types;
using WPF_Game_Tic_Tac_Toe.Windows;

namespace WPF_Game_Tic_Tac_Toe.ModelViews
{
    public class TicTacToeBigWindowModelView : BindableBase
    {
        public string FirstNick => _model.FirstNickName;
        public string SecondNick => _model.SecondNickName;
        public string NowWalking => _model.NowWalking;
        public int AllPlayedGames => _model.AllPlayedGames;
        public int FirstPlayerWins => _model.FirstPlayerWins;
        public int SecondPlayerWins => _model.SecondPlayerWins;

        private readonly Game _model = new(15);
        private int GameTurnNumber => _model.GameTurnNumber;
        private Button[,] GetButtons => _model.Buttons;

        public DelegateCommand<string> SetFirstNickName { get; }
        public DelegateCommand<string> SetSecondNickName { get; }
        public DelegateCommand<object> ClickOnCell { get; }
        public DelegateCommand Restart { get; }
        public DelegateCommand<object> Back { get; }

        public TicTacToeBigWindowModelView()
        {
            _model.PropertyChanged += (_, e) => { RaisePropertyChanged(e.PropertyName); };

            SetFirstNickName = new DelegateCommand<string>(str =>
            {
                if (IsBadNickName(str))
                    return;

                _model.SetFirstNickName(str);
            });

            SetSecondNickName = new DelegateCommand<string>(str =>
            {
                if (IsBadNickName(str))
                    return;

                _model.SetSecondNickName(str);
            });

            ClickOnCell = new DelegateCommand<object>(obj =>
            {
                if (obj is not Button btn)
                    return;

                btn.Content = NowWalking == FirstNick ? "X" : "0";
                btn.IsEnabled = false;

                var column = Grid.GetColumn(btn);
                var row = Grid.GetRow(btn);

                _model.SetButton(row, column, btn);
                _model.AddGameTurnNumber();

                if (GameTurnNumber <= 9)
                    return;

                var ticTacToe = new TicTacToe(GetButtons, GameModeType.Big);
                var winner = ticTacToe.CheckWinner();

                if (winner != WinnerType.None)
                    ShowWinner(winner == WinnerType.PlayerOne ? FirstNick : SecondNick);

                if (GameTurnNumber <= 225) return;

                MessageBox.Show("The winner is undecided! Restarting the game.");
                RestartGame();
            });
            Restart = new DelegateCommand(RestartGame);
            Back = new DelegateCommand<object>(obj =>
            {
                if (obj is not Window win)
                    return;

                var mainWin = new MainWindow();
                mainWin.Show();

                win.Close();
            });
        }

        private void ShowWinner(string nickName)
        {
            MessageBox.Show($"WINNER {nickName}");

            if (nickName == FirstNick)
                _model.AddFirstPlayerWin();
            else
                _model.AddSecondPlayerWin();

            RestartGame();
        }

        private void RestartGame()
        {
            if (GameTurnNumber == 1)
            {
                MessageBox.Show("Error! The first move is impossible to restart the game.");
                return;
            }

            _model.ZeroingButtons(15);
            _model.SetGameTurnNumber(1);
            _model.AddPlayedGamesStats();
        }

        private bool IsBadNickName(string nick)
        {
            if (nick == FirstNick || nick == SecondNick)
            {
                MessageBox.Show("The nickname cannot be changed to the same or to the nickname of the opponent.");
                return true;
            }

            if (!string.IsNullOrEmpty(nick))
                return false;

            MessageBox.Show("The nickname cannot be empty or contain only spaces.");
            return true;
        }
    }
}