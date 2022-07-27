using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using WPF_Game_Tic_Tac_Toe.Types;

namespace WPF_Game_Tic_Tac_Toe.Models
{
    public class TicTacToe
    {
        private readonly Regex _regexX;
        private readonly Regex _regex0;

        private readonly StringBuilder _stringBuilder = new(15);
        private readonly Button[,] _buttons;
        private readonly int _maxIndex;

        public TicTacToe(Button[,] buttons, GameModeType gameModeType)
        {
            _buttons = buttons;
            if (gameModeType == GameModeType.Classic)
            {
                _maxIndex = 3;
                _regexX = new Regex(@"[X]{3}", RegexOptions.Compiled);
                _regex0 = new Regex(@"[0]{3}", RegexOptions.Compiled);
            }
            else
            {
                _maxIndex = 15;
                _regexX = new Regex(@"[X]{5}", RegexOptions.Compiled);
                _regex0 = new Regex(@"[0]{5}", RegexOptions.Compiled);
            }
        }

        private List<string> GetValueVertical()
        {
            if (_stringBuilder.Length > 0)
                _stringBuilder.Clear();

            var result = new List<string>();

            for (var i = 0; i < _maxIndex; i++)
            {
                for (var j = 0; j < _maxIndex; j++)
                {
                    var button = _buttons[j, i];
                    if (button == null)
                    {
                        _stringBuilder.Append('!');
                        continue;
                    }

                    _stringBuilder.Append(button.Content);
                }

                result.Add(_stringBuilder.ToString());
                _stringBuilder.Clear();
            }

            return result;
        }

        private List<string> GetValueHorizont()
        {
            if (_stringBuilder.Length > 0)
                _stringBuilder.Clear();

            var result = new List<string>();

            for (var i = 0; i < _maxIndex; i++)
            {
                for (var k = 0; k < _maxIndex; k++)
                {
                    var button = _buttons[i, k];
                    if (button == null)
                    {
                        _stringBuilder.Append('!');
                        continue;
                    }

                    _stringBuilder.Append(button.Content);
                }

                result.Add(_stringBuilder.ToString());
                _stringBuilder.Clear();
            }

            return result;
        }

        private string GetValueLTRB()
        {
            if (_stringBuilder.Length > 0)
                _stringBuilder.Clear();

            for (var i = 0; i < _maxIndex; i++)
            {
                var button = _buttons[i, i];
                if (button == null)
                {
                    _stringBuilder.Append('!');
                    continue;
                }

                _stringBuilder.Append(button.Content);
            }

            return _stringBuilder.ToString();
        }

        private string GetValueLBRT()
        {
            if (_stringBuilder.Length > 0)
                _stringBuilder.Clear();

            for (int i = 0, k = _maxIndex - 1; k >= 0; k--, i++)
            {
                var button = _buttons[i, k];
                if (button == null)
                {
                    _stringBuilder.Append('!');
                    continue;
                }

                _stringBuilder.Append(button.Content);
            }

            return _stringBuilder.ToString();
        }

        public WinnerType CheckWinner()
        {
            var listHorizont = GetValueHorizont();
            var listVertical = GetValueVertical();
            var diagonalLTRB = GetValueLTRB(); // Left top -> Right bot
            var diagonalLBRT = GetValueLBRT(); // Left bot -> Right top

            if (_regexX.Match(diagonalLTRB).Success || _regexX.Match(diagonalLBRT).Success)
                return WinnerType.PlayerOne;


            if (_regex0.Match(diagonalLTRB).Success || _regex0.Match(diagonalLBRT).Success)
                return WinnerType.PlayerTwo;


            for (var i = 0; i < 3; i++)
            {
                if (_regexX.Match(listHorizont[i]).Success || _regexX.Match(listVertical[i]).Success)
                    return WinnerType.PlayerOne;


                if (_regex0.Match(listHorizont[i]).Success || _regex0.Match(listVertical[i]).Success)
                    return WinnerType.PlayerTwo;
            }

            return WinnerType.None;
        }
    }
}