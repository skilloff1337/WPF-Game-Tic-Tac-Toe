using System.Windows;
using Prism.Commands;
using WPF_Game_Tic_Tac_Toe.Windows;

namespace WPF_Game_Tic_Tac_Toe.ModelViews
{
    public class MainWindowModelView
    {
        public DelegateCommand LoadClassicMode { get; }
        public DelegateCommand LoadBigMode { get; }
        public DelegateCommand LoadDescription { get; }


        public MainWindowModelView()
        {
            LoadClassicMode = new DelegateCommand(LoadTicTacToeClassicWindow);
            LoadBigMode = new DelegateCommand(LoadTicTacToeBigWindow);
            LoadDescription = new DelegateCommand(LoadDescriptionWindow);
        }

        private void LoadDescriptionWindow()
        {
            var desc = new DescriptionWindow();
            desc.ShowDialog();
        }

        private void LoadTicTacToeBigWindow()
        {
            var big = new TicTacToeBig();
            big.Show();
            Application.Current.MainWindow!.Close();
        }

        private void LoadTicTacToeClassicWindow()
        {
            var classic = new TicTakToeClassic();
            classic.Show();
            Application.Current.MainWindow!.Close();
        }
    }
}