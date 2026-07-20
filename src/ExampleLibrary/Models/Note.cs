using System;

namespace ExampleLibrary.Models;

public class Note
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public Note(string id, string title, string body, DateTimeOffset createdAt)
    {
        Id = id;
        Title = title;
        Body = body;
        CreatedAt = createdAt;
    }
}
