using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace WPF_Game_Tic_Tac_Toe.Scripts;

public class TicTacToe
{
    private readonly StringBuilder _stringBuilder = new(15);
    private readonly Button[,] _buttons;
    private readonly int _maxIndex;

    public TicTacToe(Button[,] buttons, int maxIndex)
    {
        _buttons = buttons;
        _maxIndex = maxIndex;
    }

    public List<string> GetValueVertical()
    {
        if (_stringBuilder.Length > 0)
            _stringBuilder.Clear();

        var result = new List<string>();

        for (var i = 0; i < _maxIndex; i++)
        {
            for (var j = 0; j < _maxIndex; j++)
            {
                var button = _buttons[j, i];
                if (button.Content == null)
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

    public List<string> GetValueHorizont()
    {
        if (_stringBuilder.Length > 0)
            _stringBuilder.Clear();

        var result = new List<string>();

        for (var i = 0; i < _maxIndex; i++)
        {
            for (var k = 0; k < _maxIndex; k++)
            {
                var button = _buttons[i, k];
                if (button.Content == null)
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

    public string GetValueLTRB()
    {
        if (_stringBuilder.Length > 0)
            _stringBuilder.Clear();

        for (var index = 0; index < _maxIndex; index++)
        {
            var button = _buttons[index, index];
            if (button.Content == null)
            {
                _stringBuilder.Append('!');
                continue;
            }

            _stringBuilder.Append(button.Content);
        }

        return _stringBuilder.ToString();
    }

    public string GetValueLBRT()
    {
        if (_stringBuilder.Length > 0)
            _stringBuilder.Clear();

        for (int firstIndex = 0, secondIndex = _maxIndex - 1; secondIndex >= 0; secondIndex--, firstIndex++)
        {
            var button = _buttons[firstIndex, secondIndex];
            if (button.Content == null)
            {
                _stringBuilder.Append('!');
                continue;
            }

            _stringBuilder.Append(button.Content);
        }

        return _stringBuilder.ToString();
    }
}