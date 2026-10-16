
using System;

namespace CyberChatBot
{
    public class ChatParticipant
    {
        public string Name { get; set; }

        // Allows the WPF window to receive messages from the bot.
        public event Action<string> MessageSpoken;

        public ChatParticipant(string name)
        {
            Name = name;
        }

        public virtual void Speak(string message)
        {
            string fullMessage = $"{Name}: {message}";

            // Send the message to the WPF interface.
            MessageSpoken?.Invoke(fullMessage);

            // Retain console output for compatibility with Part 1.
            Console.WriteLine(fullMessage);
        }
    }
}
