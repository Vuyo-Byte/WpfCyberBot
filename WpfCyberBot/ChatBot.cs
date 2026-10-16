
using System;

namespace CyberChatBot
{
    public class ChatBot : ChatParticipant
    {
        private readonly Random _random = new Random();

        private readonly string[,] _tipsDatabase =
        {
            {
                "Tip 1: Use strong, unique passwords for every account.",
                "Tip 2: Enable two-factor authentication whenever possible.",
                "Tip 3: Watch out for phishing emails and suspicious links."
            },
            {
                "Tip 4: Keep your software and operating system updated.",
                "Tip 5: Use reputable antivirus software and scan regularly.",
                "Tip 6: Avoid sensitive transactions on public Wi-Fi."
            },
            {
                "Tip 7: Back up important data to an external drive or cloud.",
                "Tip 8: Only grant apps the permissions they actually need.",
                "Tip 9: Learn about cybersecurity threats and best practices."
            }
        };

        public ChatBot(string name) : base(name)
        {
        }

        private int GetCategoryRow(string input)
        {
            input = input.ToUpperInvariant();

            if (input.Contains("PASSWORD") ||
                input.Contains("PHISHING"))
                return 0;

            if (input.Contains("MALWARE") ||
                input.Contains("ANTIVIRUS") ||
                input.Contains("UPDATE") ||
                input.Contains("WI-FI") ||
                input.Contains("WIFI"))
                return 1;

            if (input.Contains("BACKUP") ||
                input.Contains("PERMISSION") ||
                input.Contains("EDUCATION"))
                return 2;

            return -1;
        }

        // Handles greetings, conversation and cybersecurity tips.
        public void RespondTo(string input)
        {
            string message = input.Trim();
            string lower = message.ToLowerInvariant();

            if (lower == "exit" || lower == "quit")
            {
                Speak("Goodbye! Stay safe online.");
                return;
            }

            if (lower.Contains("your name"))
            {
                Speak("I'm CyberBot, your cybersecurity assistant.");
                return;
            }

            if (lower == "hi" || lower == "hello" ||
                lower.StartsWith("hey"))
            {
                Speak("Hello! How can I help you stay safe online?");
                return;
            }

            if (lower.Contains("how are you"))
            {
                Speak("I'm running smoothly! What would you like to learn today?");
                return;
            }

            if (lower.Contains("thank"))
            {
                Speak("You're welcome! Staying safe online starts with good habits.");
                return;
            }

            if (lower.Contains("help") ||
                lower.Contains("what can you do"))
            {
                Speak("I can discuss Passwords, Phishing, Malware, Updates, " +
                      "Antivirus, Wi-Fi, Backup, Permissions and Education. " +
                      "You can also say hello or ask me how I am.");
                return;
            }

            if (lower.Contains("password") ||
                lower.Contains("phishing") ||
                lower.Contains("malware") ||
                lower.Contains("antivirus") ||
                lower.Contains("update") ||
                lower.Contains("wi-fi") ||
                lower.Contains("wifi") ||
                lower.Contains("backup") ||
                lower.Contains("permission") ||
                lower.Contains("education"))
            {
                GiveTip(message);
                return;
            }

            if (lower.Contains("worried") ||
                lower.Contains("scared") ||
                lower.Contains("hacked"))
            {
                Speak("If you think an account has been compromised, " +
                      "change its password from a trusted device, " +
                      "enable two-factor authentication, and check its " +
                      "official account recovery guidance.");
                return;
            }

            Speak("I'm not sure I understood that. You can ask me for help " +
                  "or choose a cybersecurity topic such as Passwords, " +
                  "Phishing, Malware, Wi-Fi or Backup.");
        }

        public void GiveTip(string category)
        {
            int row = GetCategoryRow(category);

            if (row == -1)
            {
                Speak("I didn't recognise that topic. Try Password, Phishing, " +
                      "Malware, Updates, Antivirus, Wi-Fi, Backup, " +
                      "Permissions or Education.");
                return;
            }

            int tipIndex = _random.Next(0, 3);

            Speak(_tipsDatabase[row, tipIndex]);
        }
    }
}
