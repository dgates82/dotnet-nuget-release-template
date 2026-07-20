using ExampleLibrary.Models;

namespace ExampleLibrary.Storage;

public interface INoteStore
{

    Task PutNoteAsync(Note note, CancellationToken cancellationToken = default);
    Task<Note?> GetNoteAsync(string id,  CancellationToken cancellationToken = default);
    Task<IEnumerable<NoteSummary>> ListNotesAsync(CancellationToken cancellationToken = default);
    Task DeleteNoteAsync(string id, CancellationToken cancellationToken = default);

}