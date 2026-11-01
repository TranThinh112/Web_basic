using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BunnyChat.Models
{
    public class MessageReaction
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = string.Empty;

        public string Emoji { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        // Conversation chứa tin nhắn này
        [BsonRepresentation(BsonType.ObjectId)]
        public string ConversationId { get; set; } = string.Empty;

        // Người gửi
        [BsonRepresentation(BsonType.ObjectId)]
        public string SenderId { get; set; } = string.Empty;

        // Nội dung text
        public string? Content { get; set; }

        // Ảnh đính kèm
        public string? ImgUrl { get; set; }

        // Danh sách reaction thả cảm xúc
        public List<MessageReaction> Reactions { get; set; } = new();

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; }
            = DateTime.UtcNow;
    }
}
