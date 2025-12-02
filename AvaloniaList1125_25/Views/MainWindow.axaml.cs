using System.ComponentModel.DataAnnotations;
using Avalonia.Controls;
using Avalonia.Input;
using AvaloniaList1125_25.ViewModels;

namespace AvaloniaList1125_25;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        // можно модель назначить из кода, а можно из xaml
        //DataContext = new MainWindowVM();
        
    }

    private void ViewNoteDoubleTap(object? sender, TappedEventArgs e)
    {
        (DataContext as MainWindowVM).SendDoubleTap();
    }
}