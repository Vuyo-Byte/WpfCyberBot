
using System;
using System.IO;
using System.Media;

namespace CyberChatBot
{
    public class AudioPlayer
    {
        private SoundPlayer player;

        public void PlaySound(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine(
                        $"[AudioPlayer] Sound file not found: {filePath}");
                    return;
                }

                player?.Stop();
                player?.Dispose();

                player = new SoundPlayer(filePath);
                player.Load();
                player.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"[AudioPlayer] Could not play sound: {ex.Message}");
            }
        }

        public void StopSound()
        {
            player?.Stop();
        }
    }
}
