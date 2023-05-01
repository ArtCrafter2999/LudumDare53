using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Collections;
using System.Collections;
using LudumDare53.Leveling;
using Unity.VisualScripting;
using UnityEngine.Serialization;

namespace LudumDare53.Nodes
{
    public class NodeSequence : MonoBehaviour
    {
        [ReadOnly] public int _currentIndex;

        public List<NodeBase> nodes;
        public AudioSource source;

        private bool _isSkipped = false;

        public bool playOnAwake;
        public bool IsPlaying { get; private set; }
        private bool IsPaused => PauseManager.IsPaused && PauseManager.Cause != PauseManager.PauseCause.Tutorial;

        private bool AnySkipCause => _isSkipped  || IsPaused || !IsPlaying;

        private void Start()
        {
            PauseManager.Resume.AddListener(() => {if (PauseManager.Cause == PauseManager.PauseCause.Tutorial) PauseManager.SetPause(PauseManager.PauseCause.Tutorial);});
            nodes.ForEach(n => n.NodeSequence = this);
            if (playOnAwake) StartSequence();
        }

        public void StartSequence()
        {
            StartCoroutine(StartSequenceWork());
        }

        public void StopSequence()
        {
            IsPlaying = false;
        }

        public void SkipNode()
        {
            _isSkipped = nodes[_currentIndex].Skip();
        }

        private IEnumerator StartSequenceWork()
        {
            IsPlaying = true;
            for (var i = 0; i < nodes.Count; i++)
            {
                _currentIndex = i;
                var node = nodes[i];
                node.Init();
                yield return new WaitUntil(() => node.Invoke() || AnySkipCause);
                
                if (!IsPlaying)
                {
                    node.Stop();
                    break;
                }
                if (IsPaused)
                {
                    node.Pause();
                    i--;
                    yield return new WaitWhile(() => IsPaused);
                }

                if (AnySkipCause) nodes[i].Break();
                _isSkipped = false;
            }
            StopSequence();
        }
    }
}