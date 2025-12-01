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
    private NoteVM _selectedNote;
    // ObservableCollection уведомляет интерфейс, если в нем меняется кол-во значений
    public ObservableCollection<NoteVM> Notes { get; set; }

    // команды представлены свойствами, для того, чтобы работал Binding
    public VMCommand AddNoteCommand { get; set; }
    public VMCommand EditNoteCommand { get; set; }

    public NoteVM SelectedNote
    {
        get => _selectedNote;
        set
        {
            if (Equals(value, _selectedNote)) return;
            _selectedNote = value;
            OnPropertyChanged();
        }
    }

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
            else if (window.Removed)
            {
                NoteDBInstance.Get().DB.DeleteNote(SelectedNote.Value);
            }
        });
        
        // инициализация команды редактирования
        EditNoteCommand = new(async () =>
        {
            if (SelectedNote == null)
                return;

            EditNoteWindow window = new EditNoteWindow(SelectedNote);

            Window main = null;
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                main = desktop.MainWindow;
            await window.ShowDialog(main);

            if (window.Saved)
            {
                NoteDBInstance.Get().DB.UpdateNote(SelectedNote.Value);
            }
            else if (window.Removed)
            {
                NoteDBInstance.Get().DB.DeleteNote(SelectedNote.Value);
                Notes.Remove(SelectedNote);
            }
        });
    }
}