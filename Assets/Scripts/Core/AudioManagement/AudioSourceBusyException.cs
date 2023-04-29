using System;

namespace DanPie.Framework.AudioManagement
{
    public class AudioSourceBusyException : Exception
    {
        public AudioSourceBusyException() 
            : base($"This source is currently busy, in order to play sound from it, first wait for " +
                  $"it to stop or stop it manually using the {nameof(AudioSourceController.Stop)} method.")
        {
        }

        public AudioSourceBusyException(string message) 
            : base(message)
        {
        }
    }
}
