using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PW.EFootballClub.PlayersAppUsvc.Models;

public class TeamTraining
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("date")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime Date { get; set; }

    [BsonElement("duration")]
    public int Duration { get; set; }

    [BsonElement("type")]
    public string Type { get; set; } = string.Empty;

    [BsonElement("TeamID")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string TeamID { get; set; }
}