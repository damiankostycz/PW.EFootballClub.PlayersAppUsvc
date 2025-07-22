using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PW.EFootballClub.PlayersAppUsvc.Models;

public class IndividualTraining
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("date")]
    public DateTime Date { get; set; }

    [BsonElement("type")]
    public string Type { get; set; } = string.Empty;

    [BsonElement("duration")]
    public required int Duration { get; set; }

    [BsonElement("exercises")]
    public required List<Exercise> Exercises { get; set; }

    [BsonElement("is_done")]
    public bool IsDone { get; set; }

    [BsonElement("PlayerID")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? PlayerId { get; set; }

    [BsonElement("CoachID")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? CoachId { get; set; }
}