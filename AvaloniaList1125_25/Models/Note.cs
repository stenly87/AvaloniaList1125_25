using System;

namespace AvaloniaList1125_25.Models;

// основной класс заметки
public class Note
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateOnly DateCreated { get; set; }
    public Status Status { get; set; }
}