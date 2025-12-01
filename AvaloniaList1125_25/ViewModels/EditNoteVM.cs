using System;
using System.Collections.Generic;
using AvaloniaList1125_25.Models;

namespace AvaloniaList1125_25.ViewModels;

public class EditNoteVM : BaseVM
{
    // хранимая ссылка на редактируемую заметку
    NoteVM _note;
    // хранимая ссылка на закрытие окна
    Action<bool> closeWindow;

    // свойство с заметкой для привязки
    public NoteVM EditNote
    {
        get => _note;
        set
        {
            _note = value;
            OnPropertyChanged();
        }
    }

    // список статусов для комбобокса
    public List<Status> Statuses { get; set; }

    // команды сохранить и удалить (инициализация в конструкторе)
    public VMCommand CloseCommand { get; set; }
    public VMCommand RemoveCommand { get; set; }

    public EditNoteVM()
    {
        // загрузка списка статусов
        Statuses = new(Enum.GetValues<Status>());

        // инициализация команд
        CloseCommand = new(() => { closeWindow(false); });

        RemoveCommand = new(() => { closeWindow(true); });
    }

    // методы для сохранения ссылок на заметку и метод закрытия
    public void SetEditNote(NoteVM note)
    {
        EditNote = note;
    }
    public void SetWindowClose(Action<bool> action)
    {
        closeWindow = action;
    }
}