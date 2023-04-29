using System;
using System.Collections.Generic;
using System.Linq;
using DanPie.Framework.Coroutines;
using DanPie.Framework.Pooling;
using UnityEngine;
using UnityEngine.Audio;

namespace DanPie.Framework.AudioManagement
{
    public class AudioSourcesManager : CoroutineExecutor
    {
        [SerializeField] private int _sourcesInitialCount = 5;
        [SerializeField] private GameObject _sourcesContainer;

        private PoolOfType<AudioSourceController> _audioSourceControllerPool;
        private List<AudioSourceController> _activeSources = new List<AudioSourceController>();

        public List<AudioSourceController> GetActiveSourceControllersBy(AudioMixerGroup audioMixerGroup)
        {
            return _activeSources.Where((x) => x.PlayingAudioClipData.MixerGroup == audioMixerGroup).ToList();
        }

        public AudioSourceController GetAudioSourceController()
        {
            var instance = _audioSourceControllerPool.TakeInstance();
            _activeSources.Add(instance);
            return instance;
        }

        protected void Awake()
        {
            if (_sourcesInitialCount < 0)
            {
                throw new ArgumentException($"The {nameof(_sourcesInitialCount)} can't be less then zero!");
            }

            var poolBehaviour = new PoolObjectInitializingBehaviour<AudioSourceController>()
            {
                OnCreatedAction = (x) => x.AudioSource.enabled = false,

                OnDisposedAction = (x) => {
                    MonoBehaviour.Destroy(x.AudioSource);
                },

                OnReturnedToPoolAction = (x) => _activeSources.Remove(x)
            };

            _audioSourceControllerPool
                = new PoolOfType<AudioSourceController>("AudioSourceUsers", NewAudioSourceUser, poolBehaviour);

            _audioSourceControllerPool.SetInstancesCount(_sourcesInitialCount);
        }

        private AudioSourceController NewAudioSourceUser()
        {
            AudioSourceController sourceController = new AudioSourceController(
                _sourcesContainer.AddComponent<AudioSource>(),
                this,
                (x) => _audioSourceControllerPool.ReturnInstance(x));

            return sourceController;
        }
    }
}
