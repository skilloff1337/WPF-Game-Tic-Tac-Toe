﻿using System.Windows;

namespace WPF_Game_Tic_Tac_Toe.Windows;

public partial class DescriptionWindow : Window
{
    public DescriptionWindow()
    {
        InitializeComponent();
    }

    private void Button_CloseWindow(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}