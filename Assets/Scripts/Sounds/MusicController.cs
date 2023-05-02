using System.Collections.Generic;
using DanPie.Framework.AudioManagement;
using LudumDare53.Leveling;
using UnityEngine;

namespace LudumDare53.Sounds
{

    public class MusicController : MonoBehaviour
    {
        [SerializeField] private bool _isPausable = true;
        [SerializeField] private AudioSourcesManager _sourcesManager;
        [SerializeField] private List<AudioClipDataProvider> _musicProviders;

        private AudioSourceController _musicSourceController;

        private AudioSourceController MusicSource
            => _musicSourceController = _musicSourceController ?? _sourcesManager.GetAudioSourceController();

        public AudioClipData GetMusicData()
        {
            return _musicProviders[Mathf.Clamp(DifficultyManager.Difficulty, 0, _musicProviders.Count - 1)].GetClipData();
        }

        public void PlayMusic(AudioClipData musicData)
        {

            if (MusicSource.IsBusy)
            {
                StopMusic();
            }

            MusicSource.Play(musicData, true, false, _isPausable);
        }

        public void StopMusic()
        {
            MusicSource?.Stop();
        }


        private void OnDifficultyChanged()
        {
            PlayMusic(GetMusicData());
        }

        protected void OnEnable()
        {
            OnDifficultyChanged();
            DifficultyManager.DifficultyChanged.AddListener(OnDifficultyChanged);
        }

        protected void OnDisable()
        {
            StopMusic();
            DifficultyManager.DifficultyChanged.RemoveListener(OnDifficultyChanged);
        }
    }
}
