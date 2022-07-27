using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using WPF_Game_Tic_Tac_Toe.Data;
using WPF_Game_Tic_Tac_Toe.Scripts;

namespace WPF_Game_Tic_Tac_Toe.Windows;

public partial class TicTacToeBig : Window
{
    private static readonly Regex _regexX = new(@"[X]{5}", RegexOptions.Compiled);
    private static readonly Regex _regex0 = new(@"[0]{5}", RegexOptions.Compiled);

    private readonly Game _gameInfo = new();
    private readonly Button[,] _buttons = new Button[15, 15];

    private readonly string _firstNick;
    private readonly string _secondNick;

    private const string SYMBOL_X = "X";
    private const string SYMBOL_O = "0";

    public TicTacToeBig()
    {
        _firstNick = "firstNick";
        _secondNick = "secondNick";
        InitializeComponent();
        NicknamesPlayers.Text = $"{"firstNick"}(X)\n{"secondNick"}(0)";
        UpdateTexts();
        CreateButtons();
    }

    private void Button_ClickPlayer(object sender, RoutedEventArgs e)
    {
        if (sender is not Button btn)
            return;

        var currentMove = _gameInfo.GameTurnNumber % 2 == 0 ? SYMBOL_O : SYMBOL_X;
        btn.Content = currentMove;
        btn.IsEnabled = false;

        if (_gameInfo.GameTurnNumber >= 9)
            CheckWinner();

        _gameInfo.GameTurnNumber++;
        UpdateTexts();
    }

    private void CheckWinner()
    {
        var tic = new TicTacToe(_buttons, 15);

        var listHorizont = tic.GetValueHorizont();
        var listVertical = tic.GetValueVertical();
        var diagonalLTRB = tic.GetValueLTRB(); // Left top -> Right bot
        var diagonalLBRT = tic.GetValueLBRT(); // Left bot -> Right top

        if (_regexX.Match(diagonalLTRB).Success || _regexX.Match(diagonalLBRT).Success)
        {
            ShowWinner(_firstNick);
            return;
        }

        if (_regex0.Match(diagonalLTRB).Success || _regex0.Match(diagonalLBRT).Success)
        {
            ShowWinner(_secondNick);
            return;
        }

        for (var i = 0; i < 15; i++)
        {
            if (_regexX.Match(listHorizont[i]).Success || _regexX.Match(listVertical[i]).Success)
            {
                ShowWinner(_firstNick);
                return;
            }

            if (_regex0.Match(listHorizont[i]).Success || _regex0.Match(listVertical[i]).Success)
            {
                ShowWinner(_secondNick);
                return;
            }
        }
    }

    private void ShowWinner(string nickName)
    {
        MessageBox.Show($"WINNER {nickName}");

        if (nickName == _firstNick)
            _gameInfo.FirstPlayerWins++;
        else
            _gameInfo.SecondPlayerWins++;

        RestartGame();
    }


    private void CreateButtons()
    {
        var count = 0;
        for (var h = 0; h < 15; h++)
        {
            for (var w = 0; w < 15; w++)
            {
                var newBtn = new Button();

                newBtn.Name = "Button" + count;
                newBtn.Click += Button_ClickPlayer;
                newBtn.FontSize = 25;

                Grid.SetColumn(newBtn, w);
                Grid.SetRow(newBtn, h);
                Grid.Children.Add(newBtn);

                count++;

                _buttons[h, w] = newBtn;
            }
        }
    }

    private void Button_OpenMainWindow(object sender, RoutedEventArgs e)
    {
        var newWindow = new MainWindow();
        newWindow.Show();
        this.Close();
    }

    private void RestartGame(object sender = null, RoutedEventArgs e = null)
    {
        for (var i = 0; i < 15; i++)
        {
            for (var j = 0; j < 15; j++)
            {
                _buttons[i, j].Content = null;
                _buttons[i, j].IsEnabled = true;
            }
        }

        _gameInfo.GameTurnNumber = 1;
        _gameInfo.AllPlayedGames++;
        UpdateTexts();
    }

    private void UpdateTexts()
    {
        NowWalking.Text = _gameInfo.GameTurnNumber % 2 == 0 ? _secondNick : _firstNick;
        Statistics.Text = $"Games played: {_gameInfo.AllPlayedGames}" +
                          $"\n{_firstNick} wins: {_gameInfo.FirstPlayerWins}" +
                          $"\n{_secondNick} wins: {_gameInfo.SecondPlayerWins}";
    }
}