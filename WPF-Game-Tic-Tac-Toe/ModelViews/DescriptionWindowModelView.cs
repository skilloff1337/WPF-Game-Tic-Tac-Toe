using System.Windows;
using Prism.Commands;

namespace WPF_Game_Tic_Tac_Toe.ModelViews
{
    public class DescriptionWindowModelView
    {
        public DelegateCommand<object> Close { get; }

        public DescriptionWindowModelView()
        {
            Close = new DelegateCommand<object>(obj =>
            {
                if (obj is not Window win)
                    return;
                win.Close();
            });
        }
    }
}