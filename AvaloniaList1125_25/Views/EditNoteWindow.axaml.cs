using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaList1125_25.Models;
using AvaloniaList1125_25.ViewModels;

namespace AvaloniaList1125_25.Views;

public partial class EditNoteWindow : Window
{
    public EditNoteWindow(NoteVM editNote)
    {
        InitializeComponent();
        // так как мы получаем заметку на редактирование
        // ее надо передать в датаконтекст, который назначен в xaml
        if (DataContext is EditNoteVM vm)
        {
            // сохраняем заметку
            vm.SetEditNote(editNote);
            // предусматриваем метод для закрытия окна
            // т.к. вьюмодель не имеет ссылки на окно для управления им
            vm.SetWindowClose(remove =>
            {
                Saved = !remove;
                Removed = remove;

                Close(Task.CompletedTask);
            });
        }
    }

    // свойства, по которым основная вьюмодель будет понимать нужно ли заметку сохранить или удалить
    public bool Saved { get; set; }
    public bool Removed { get; set; }
}