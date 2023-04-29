using System;
using UnityEngine;
using UnityEngine.Audio;

namespace DanPie.Framework.AudioManagement
{
    [Serializable]
    public class AudioClipData
    {
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private AudioMixerGroup _mixerGroup;
        [Range(0, 1)]
        [SerializeField] private float _volume;

        public AudioClipData(AudioClip audioClip, AudioMixerGroup mixerGroup, float volume)
        {
            _audioClip = audioClip;
            _mixerGroup = mixerGroup;
            _volume = volume;
        }

        public AudioClip AudioClip { get => _audioClip; }
        public AudioMixerGroup MixerGroup { get => _mixerGroup; }
        public float Volume { get => _volume; }
    }
}
