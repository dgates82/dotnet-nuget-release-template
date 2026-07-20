using System;

namespace ExampleLibrary.Models;

public class NoteSummary
{
    public string Id { get; set; }
    public string Title { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public NoteSummary(string id, string title, DateTimeOffset createdAt)
    {
        Id = id;
        Title = title;
        CreatedAt = createdAt;
    }
}
