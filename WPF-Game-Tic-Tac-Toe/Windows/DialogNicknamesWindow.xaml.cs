using System.Windows;

namespace WPF_Game_Tic_Tac_Toe.Windows;

public partial class DialogNicknamesWindow : Window
{
    private readonly MainWindow _mainWindow;
    private readonly string _firstPlayerNick;
    private readonly string _secondPlayerNick;
    
    public DialogNicknamesWindow(MainWindow mainWindow, string firstName, string secondNick)
    {
        _mainWindow = mainWindow;
        _firstPlayerNick = firstName;
        _secondPlayerNick = secondNick;
        InitializeComponent();
        UpdateTexts();
    }

    private void Button_ChangeNickNames(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(FirstName.Text) || string.IsNullOrWhiteSpace(SecondName.Text))
        {
            MessageBox.Show("The nickname cannot be empty or contain only spaces.");
            return;
        }

        _mainWindow.UpdateNickNames(FirstName.Text, SecondName.Text);
    }

    private void UpdateTexts()
    {
        FirstName.Text = _firstPlayerNick;
        SecondName.Text = _secondPlayerNick;
    }
}