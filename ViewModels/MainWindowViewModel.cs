using CommunityToolkit.Mvvm.Input;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using StudyAssistant.KernelAgents;
using StudyAssistant.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StudyAssistant.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public event Action RequestScrollToBottom;

        private string? _chatInput;
        public string? ChatInput
        {
            get => _chatInput;
            set => SetProperty(ref _chatInput, value);
        }

        private ObservableCollection<ChatMessageModel>? _chatMessages;
        public ObservableCollection<ChatMessageModel> ChatMessages
        {
            get => _chatMessages;
            set => SetProperty(ref _chatMessages, value);
        }

        private TutorAgent _agent;
        public ICommand SendMessageCommand { get; }

        public MainWindowViewModel(TutorAgent agent)
        {
            ChatMessages = new ObservableCollection<ChatMessageModel>();
            _agent = agent;
            SendMessageCommand = new RelayCommand(SendMessage);
        }

        private async void SendMessage()
        {
            if (!string.IsNullOrEmpty(ChatInput))
            {
                ChatMessages.Add(new ChatMessageModel(AuthorType.User, ChatInput));
                ChatInput = string.Empty;
                var userMessage = ChatMessages.Last().Message;
                _agent.ChatHistory.AddUserMessage(userMessage);
                var recentHistory = new ChatHistory
                {
                    _agent.ChatHistory.First(author => author.Role == AuthorRole.System)
                };
                recentHistory.AddRange(_agent.ChatHistory.TakeLast(20));

                var response = await _agent.ChatService.GetChatMessageContentAsync(recentHistory, new OpenAIPromptExecutionSettings { Temperature = 0.7 });
                ChatMessages.Add(new ChatMessageModel(AuthorType.AI, response.ToString(), true));
                _agent.ChatHistory.AddAssistantMessage(response.ToString());
                await Task.Delay(TimeSpan.FromSeconds(5));
                ChatMessages.Last().LastMessage = false;
                RequestScrollToBottom?.Invoke();
            }
        }
    }
}
