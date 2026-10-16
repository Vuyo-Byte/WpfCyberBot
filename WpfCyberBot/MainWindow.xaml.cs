
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CyberChatBot
{
    public partial class MainWindow : Window
    {
        private ChatBot bot;
        private User user;
        private AudioPlayer audioPlayer;

        private bool waitingForName = true;
        private bool conversationEnded = false;

        public MainWindow()
        {
            InitializeComponent();

            bot = new ChatBot("CyberBot");
            user = null;
            audioPlayer = new AudioPlayer();

            bot.MessageSpoken += Bot_MessageSpoken;

            string soundPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "chatBot.wav");

            audioPlayer.PlaySound(soundPath);

            bot.Speak(
                "Welcome to the Cybersecurity Chatbot! What is your name?");

            MessageInput.Focus();
        }

        private void Bot_MessageSpoken(string message)
        {
            AddMessage(message, false);
        }

        private void SendButton_Click(
            object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void MessageInput_KeyDown(
            object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
                e.Handled = true;
            }
        }

        private void SendMessage()
        {
            if (conversationEnded)
                return;

            string message = MessageInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(message))
                return;

            AddMessage(
                (user == null ? "You" : user.Name) + ": " + message,
                true);

            MessageInput.Clear();

            if (waitingForName)
            {
                if (message.Length > 50)
                {
                    bot.Speak(
                        "Please enter a name with 50 characters or fewer.");
                    return;
                }

                user = new User(message);
                waitingForName = false;

                bot.Speak(
                    $"Hello, {user.Name}! Nice to meet you.");

                bot.Speak(
                    "I can provide cybersecurity tips. Type Help to see " +
                    "the topics I support.");

                MessageInput.Focus();
                return;
            }

            if (message.Equals(
                    "exit", StringComparison.OrdinalIgnoreCase) ||
                message.Equals(
                    "quit", StringComparison.OrdinalIgnoreCase))
            {
                bot.Speak(
                    $"Goodbye, {user.Name}! Stay safe online.");

                conversationEnded = true;
                MessageInput.IsEnabled = false;
                SendButton.IsEnabled = false;
                return;
            }

            bot.RespondTo(message);

            ChatScroll.ScrollToEnd();
            MessageInput.Focus();
        }

        private void AddMessage(string message, bool isUser)
        {
            Border bubble = new Border
            {
                Background = new SolidColorBrush(
                    (Color)ColorConverter.ConvertFromString(
                        isUser ? "#073D52" : "#292F49")),

                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(12),

                Margin = new Thickness(
                    isUser ? 70 : 0,
                    0,
                    isUser ? 0 : 70,
                    10),

                HorizontalAlignment = isUser
                    ? HorizontalAlignment.Right
                    : HorizontalAlignment.Left,

                MaxWidth = 650
            };

            TextBlock text = new TextBlock
            {
                Text = message,
                Foreground = Brushes.White,
                FontSize = 14,
                TextWrapping = TextWrapping.Wrap
            };

            bubble.Child = text;
            ChatMessages.Children.Add(bubble);

            ChatScroll.ScrollToEnd();
        }

        protected override void OnClosed(EventArgs e)
        {
            audioPlayer?.StopSound();

            if (bot != null)
                bot.MessageSpoken -= Bot_MessageSpoken;

            base.OnClosed(e);
        }
    }
}
