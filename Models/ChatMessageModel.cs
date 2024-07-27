using System.ComponentModel;

namespace StudyAssistant.Models
{
    public enum AuthorType
    {
        User,
        AI
    }
    public class ChatMessageModel(AuthorType authorType, string message, bool lastMessage = false) : INotifyPropertyChanged
    {
        public AuthorType Author { get; set; } = authorType;
        public string Message { get; set; } = message;

        private bool _lastMessage = lastMessage;
        public bool LastMessage
        {
            get => _lastMessage;
            set
            {
                if (_lastMessage != value)
                {
                    _lastMessage = value;
                    OnPropertyChanged(nameof(LastMessage));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
