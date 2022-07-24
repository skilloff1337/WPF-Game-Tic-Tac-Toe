using System.Windows;

namespace WPF_Game_Tic_Tac_Toe.Windows
{
    public partial class MainWindow : Window
    {
        private string _firstNick = "Player №1";
        private string _secondNick = "Player №2";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_ShowChangeNick(object sender, RoutedEventArgs e)
        {
            var newWindow = new DialogNicknamesWindow(this, _firstNick, _secondNick);
            newWindow.ShowDialog();
        }

        private void Button_ShowDescription(object sender, RoutedEventArgs e)
        {
            var newWindow = new DescriptionWindow();
            newWindow.ShowDialog();
        }

        private void Button_OpenClassicTicTacToe(object sender, RoutedEventArgs e)
        {
            var newWindow = new TicTakToeClassic(_firstNick, _secondNick);
            newWindow.Show();
            this.Close();
        }

        private void Button_OpenBigTicTacToe(object sender, RoutedEventArgs e)
        {
            var newWindow = new TicTacToeBig(_firstNick, _secondNick);
            newWindow.Show();
            this.Close();
        }

        public void UpdateNickNames(string firstNick, string secondNick)
        {
            _firstNick = firstNick;
            _secondNick = secondNick;
            TextFirstNick.Text = firstNick + "(X)";
            TextSecondNick.Text = secondNick + "(0)";
        }
    }
}