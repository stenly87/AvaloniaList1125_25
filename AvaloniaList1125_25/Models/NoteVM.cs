using System;
using AvaloniaList1125_25.ViewModels;

namespace AvaloniaList1125_25.Models;

// оболочка для заметки, нужна для отображения изменений 
// в свойствах при изменении заметки
public class NoteVM : BaseVM
{
    private Note _note;

    public string Title
    {
        get => _note.Title;
        set
        {
            _note.Title = value;
            OnPropertyChanged();
        }
    }

    public string Content
    {
        get => _note.Content;
        set
        {
            _note.Content = value;
            OnPropertyChanged();
        }
    }

    public DateOnly DateCreated
    {
        get => _note.DateCreated;
        set
        {
            _note.DateCreated = value;
            OnPropertyChanged();
        }
    }

    public Status Status
    {
        get => _note.Status;
        set
        {
            _note.Status = value;
            OnPropertyChanged();
        }
    }

    public NoteVM(Note note)
    {
        _note = note;
    }

    public Note Value
    {
        get => _note;
    }
}