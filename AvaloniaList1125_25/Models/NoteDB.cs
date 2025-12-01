using System;
using System.Collections.Generic;
using System.Linq;

namespace AvaloniaList1125_25.Models;

public class NoteDB
{
    private readonly INoteSerializer _serializer;
    List<Note> notes;

    public NoteDB(INoteSerializer serializer)
    {
        _serializer = serializer;
        notes = serializer.Load();
    }

    public Note CreateNote()
    {
        Note note = new Note { DateCreated = DateOnly.FromDateTime(DateTime.Today) };
        notes.Add(note);
        _serializer.Save(notes);
        return note;
    }

    public void UpdateNote(Note note)
    {
        _serializer.Save(notes);
    }

    public void DeleteNote(Note note)
    {
        notes.Remove(note);
        _serializer.Save(notes);
    }

    public List<Note> GetLast10Notes()
    {
        if (notes.Count <= 10)
            return notes.OrderByDescending(s => s.DateCreated).ToList();
        else
            return notes.TakeLast(10).OrderByDescending(s => s.DateCreated).ToList();
    }
}