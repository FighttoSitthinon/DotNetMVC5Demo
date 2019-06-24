using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotNetMVC5Demo.Models
{
    public class UserModel
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Role")]
        public string Role { get; set; }
    }
}