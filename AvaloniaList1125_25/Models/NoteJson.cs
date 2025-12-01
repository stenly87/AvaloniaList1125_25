using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace AvaloniaList1125_25.Models;

// реализация сохранения и загрузки заметок через json
public class NoteJson : INoteSerializer
{
    private string file = "notes.json";

    public void Save(List<Note> notes)
    {
        using (var fs = File.Create(file))
            JsonSerializer.Serialize(fs, notes);
    }

    public List<Note> Load()
    {
        if (File.Exists(file))
        {
            using (var fs = File.OpenRead(file))
                return JsonSerializer.Deserialize<List<Note>>(fs);
        }

        return new List<Note>();
    }
}