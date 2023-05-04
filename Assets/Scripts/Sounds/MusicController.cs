using System;
using System.Collections;
using System.Collections.Generic;
using DanPie.Framework.AudioManagement;
using DanPie.Framework.Coroutines;
using LudumDare53.Leveling;
using UnityEngine;

namespace LudumDare53.Sounds
{

    public class MusicController : MonoBehaviour
    {
        [SerializeField] private bool _isPausable = true;
        [SerializeField] private AudioSourcesManager _sourcesManager;
        [SerializeField] private List<AudioClipDataProvider> _musicProviders;
        [SerializeField] private AudioClipDataProvider mainMenu;

        private AudioSourceController _musicSourceController;

        private AudioSourceController MusicSource
            => _musicSourceController ??= _sourcesManager.GetAudioSourceController();

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

        private void Start()
        {
            StartCoroutine(CoroutineUtilities.WaitForSeconds(0.2f, () => {PlayMusic(mainMenu.GetClipData());}));
        }

        protected void OnEnable()
        {
            DifficultyManager.DifficultyChanged.AddListener(OnDifficultyChanged);
        }

        protected void OnDisable()
        {
            StopMusic();
            DifficultyManager.DifficultyChanged.RemoveListener(OnDifficultyChanged);
        }
    }
}
