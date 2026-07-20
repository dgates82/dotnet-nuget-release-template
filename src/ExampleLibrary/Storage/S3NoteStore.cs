using System.Globalization;
using System.Text;
using System.Text.Json;
using Amazon.S3;
using Amazon.S3.Model;
using ExampleLibrary.Models;

namespace ExampleLibrary.Storage;

public class S3NoteStore : INoteStore
{

    private const string KeyPrefix = "notes/";
    private const string TitleMetadataKey = "title";
    private const string CreatedAtMetadataKey = "created-at";

    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public S3NoteStore(IAmazonS3 s3Client, string bucketName)
    {
        _s3Client = s3Client ?? throw new ArgumentNullException(nameof(s3Client));

        if (string.IsNullOrEmpty(bucketName))
        {
            throw new ArgumentException("Bucket name must no be null or empty", nameof(bucketName));
        }
        
        _bucketName = bucketName;
    }

    public async Task PutNoteAsync(Note note, CancellationToken cancellationToken = default)
    {
        if (note is null)
        {
            throw new ArgumentNullException(nameof(note));
        }
        
        if (string.IsNullOrWhiteSpace(note.Id))
        {
            throw new ArgumentException("Note Id must not be null or empty", nameof(note));
        }
        
        var json = JsonSerializer.Serialize(note, JsonOptions);

        var request = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = BuildKey(note.Id),
            ContentBody = json
        };
        
        request.Metadata.Add(TitleMetadataKey, note.Title);
        request.Metadata.Add(CreatedAtMetadataKey, note.CreatedAt.ToString("o"));
        
        await _s3Client.PutObjectAsync(request, cancellationToken);
    }

    public async Task<Note?> GetNoteAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Id must not be null or empty", nameof(id));
        }

        try
        {
            var response = await _s3Client.GetObjectAsync(_bucketName, BuildKey(id), cancellationToken);
            
            using var reader = new StreamReader(response.ResponseStream, Encoding.UTF8);
            var json = await  reader.ReadToEndAsync();
            
            return JsonSerializer.Deserialize<Note>(json, JsonOptions);
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<IEnumerable<NoteSummary>> ListNotesAsync(CancellationToken cancellationToken = default)
    {
        var summaries = new List<NoteSummary>();
        
        var listRequest = new ListObjectsV2Request
        {
            BucketName = _bucketName,
            Prefix = KeyPrefix
        };
        
        ListObjectsV2Response listResponse;

        do
        {
            listResponse = await _s3Client.ListObjectsV2Async(listRequest, cancellationToken);

            foreach (var obj in listResponse.S3Objects)
            {
                var headResponse = await _s3Client.GetObjectMetadataAsync(
                    new GetObjectMetadataRequest
                    {
                        BucketName = _bucketName,
                        Key = obj.Key
                    }, cancellationToken);

                summaries.Add(MapToSummary(obj.Key, headResponse.Metadata));
            }

            listRequest.ContinuationToken = listResponse.NextContinuationToken;
        } while (listResponse.IsTruncated == true);

        return summaries;
    }

    public async Task DeleteNoteAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Id must not be null or empty", nameof(id));
        }
        
        await _s3Client.DeleteObjectAsync(_bucketName, BuildKey(id), cancellationToken);
    }
    
    private static string BuildKey(string id) => $"{KeyPrefix}{id}.json";
    
    private static string ExtractIdFromKey(string key) =>
        key.Substring(KeyPrefix.Length, key.Length - KeyPrefix.Length - ".json".Length);

    private static NoteSummary MapToSummary(string key, MetadataCollection metadata)
    {
        var id = ExtractIdFromKey(key);
        var title = metadata[TitleMetadataKey] ?? string.Empty;
        var createdAt = DateTimeOffset.Parse(
            metadata[CreatedAtMetadataKey],
            CultureInfo.InvariantCulture,
            DateTimeStyles.RoundtripKind);

        return new NoteSummary(id, title, createdAt);
    }
    
}
