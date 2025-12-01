namespace AvaloniaList1125_25.Models;

// реализация singleton-объекта для хранения ссылки на бд с списком заметок
public class NoteDBInstance
{
    private static NoteDBInstance instance;
    readonly NoteDB _db;

    // публичная ссылка на базу
    public NoteDB DB
    {
        get => _db;
    }

    private NoteDBInstance()
    {
        // создаем базу заметок и настраиваем ее на работу с json
        _db = new NoteDB(new NoteJson());
    }

    // метод для получения синглтон-объекта
    public static NoteDBInstance Get()
    {
        if (instance == null)
            instance = new();
        return instance;
    }
}