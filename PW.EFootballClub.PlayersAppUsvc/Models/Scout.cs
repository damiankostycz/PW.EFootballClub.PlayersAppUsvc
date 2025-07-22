using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PW.EFootballClub.PlayersAppUsvc.Models;

public class Scout
{      
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("name")]
    public required string Name { get; set; }

    [BsonElement("last_name")]
    public required string LastName { get; set; }

    [BsonElement("country")]
    public required string Country { get; set; }

    [BsonElement("club")]
    public required string Club { get; set; }

    [BsonElement("languages")]
    public required List<string> Languages { get; set; }

    [BsonElement("players_under_observation")]
    public required List<string> PlayersUnderObservation { get; set; }
}