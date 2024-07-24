namespace StudyAssistant.Models
{
    public enum AuthorType
    {
        User,
        AI
    }
    public class ChatMessageModel(AuthorType authorType, string message, bool lastMessage = false)
    {
        public AuthorType Author { get; set; } = authorType;
        public string Message { get; set; } = message;
        public bool LastMessage { get; set; } = lastMessage;
    }
}
