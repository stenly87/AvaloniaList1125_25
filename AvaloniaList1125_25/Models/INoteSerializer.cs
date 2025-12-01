using System.Collections.Generic;

namespace AvaloniaList1125_25.Models;

// интерфейс, дающий возможность организовать хранение заметок в любом формате
public interface INoteSerializer
{
    void Save(List<Note> notes);
    List<Note> Load();
}