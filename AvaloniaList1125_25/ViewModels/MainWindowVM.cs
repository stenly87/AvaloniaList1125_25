using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using AvaloniaList1125_25.Models;
using AvaloniaList1125_25.Views;

namespace AvaloniaList1125_25.ViewModels;

public class MainWindowVM : BaseVM
{
    // ObservableCollection уведомляет интерфейс, если в нем меняется кол-во значений
    public ObservableCollection<NoteVM> Notes { get; set; }

    // команды представлены свойствами, для того, чтобы работал Binding
    public VMCommand AddNoteCommand { get; set; }
    public VMCommandWithArg<NoteVM> EditNoteCommand { get; set; }

    public MainWindowVM()
    {
        // при запуске первого окна, сначала запускается этот конструктор
        
        // подгружаются имеющиеся заметки
        Notes = new(NoteDBInstance.Get().DB.GetLast10Notes().Select(s => new NoteVM(s)).ToList());

        // инициализация команды добавления
        AddNoteCommand = new(async () =>
        {
            var newNote = NoteDBInstance.Get().DB.CreateNote();
            EditNoteWindow window = new EditNoteWindow(new NoteVM(newNote));

            Window main = null;
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                main = desktop.MainWindow;
            await window.ShowDialog(main);

            if (window.Saved)
            {
                NoteDBInstance.Get().DB.UpdateNote(newNote);
                Notes.Add(new NoteVM(newNote));
            }
        });
        
        // инициализация команды редактирования
        EditNoteCommand = new(async s =>
        {
            if (s == null)
                return;

            EditNoteWindow window = new EditNoteWindow(s);

            Window main = null;
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                main = desktop.MainWindow;
            await window.ShowDialog(main);

            if (window.Saved)
            {
                NoteDBInstance.Get().DB.UpdateNote(s.Value);
            }
            else if (window.Removed)
            {
                NoteDBInstance.Get().DB.DeleteNote(s.Value);
                Notes.Remove(s);
            }
        });
    }

    public NoteVM SelectedNote { get; set; }
    public void SendDoubleTap()
    {
        if (SelectedNote != null)
            new ViewNoteWindow(SelectedNote).Show();
    }
}