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
    public class NodePlayer : MonoBehaviour
    { 
        private int _currentIndex;

        private List<NodeBase> nodes;
        public AudioSource source;

        private bool _isSkipped { get; set; }

        public bool playOnAwake;
        public bool IsPlaying { get; private set; }
        private bool IsPaused => PauseManager.IsPaused && PauseManager.Cause != PauseManager.PauseCause.Tutorial;

        private bool AnySkipCause => _isSkipped || IsPaused || !IsPlaying;

        private void Start()
        {
            PauseManager.Resume.AddListener(() => {if (PauseManager.Cause == PauseManager.PauseCause.Tutorial) PauseManager.Cause = PauseManager.PauseCause.Tutorial;});
            nodes?.ForEach(n => n.NodePlayer = this);
            if (playOnAwake) StartSequence();
        }

        public void SetNodes(List<NodeBase> nodeBases)
        {
            nodes = nodeBases;
            nodes.ForEach(n => n.NodePlayer = this);
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
            //Debug.Log("SkipNode, index: " + _currentIndex);
            if(_currentIndex>=0 && _currentIndex<nodes.Count) _isSkipped = nodes[_currentIndex].Skip();
        }

        private IEnumerator StartSequenceWork()
        {
            if (PauseManager.IsPaused) PauseManager.Cause = PauseManager.PauseCause.Tutorial;
            IsPlaying = true;
            
            for (_currentIndex = 0; _currentIndex < nodes.Count; _currentIndex++)
            {
                var node = nodes[_currentIndex];
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
                    _currentIndex--;
                    yield return new WaitWhile(() => IsPaused);
                }

                if (AnySkipCause) node.Break();
                _isSkipped = false;
            }
            StopSequence();
        }
    }
}