using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaList1125_25.Models;

namespace AvaloniaList1125_25.Views;

public partial class ViewNoteWindow : Window
{
    public ViewNoteWindow(NoteVM viewNote)
    {
        InitializeComponent();
        DataContext = viewNote;
    }
}