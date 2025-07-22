using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PW.EFootballClub.PlayersAppUsvc.Models
{
    public class Player
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PlayerId { get; set; } = string.Empty;

        [BsonElement("name")]
        public required string Name { get; set; }

        [BsonElement("last_name")]
        public required string LastName { get; set; }

        [BsonElement("date_of_birth")]
        [BsonRepresentation(BsonType.DateTime)]
        public required DateTime DateOfBirth { get; set; }

        [BsonElement("sex")]
        public required string Sex { get; set; }

        [BsonElement("position")]
        public required string Position { get; set; }

        [BsonElement("stats")]
        public required PlayerStats PlayerStats { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? TeamID { get; set; }


    }


}