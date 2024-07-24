using Avalonia.Controls;
using Avalonia.Threading;
using StudyAssistant.ViewModels;
using System;

namespace StudyAssistant.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContextChanged += MainWindow_DataContextChanged;
        }

        private void MainWindow_DataContextChanged(object sender, EventArgs e)
        {
            if (DataContext is MainWindowViewModel vm)
            {
                vm.RequestScrollToBottom += ViewModel_RequestScrollToBottom;
            }
        }

        private void ViewModel_RequestScrollToBottom()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                ChatMessagesScrollViewer.ScrollToEnd();
            });
        }
    }
}