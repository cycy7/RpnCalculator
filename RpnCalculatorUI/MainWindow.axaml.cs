using System;
using Avalonia.Controls;
using RpnCalculatorDomain;

namespace RpnCalculatorUI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();  
    }
}