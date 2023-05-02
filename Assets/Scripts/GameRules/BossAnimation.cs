using System;
using System.Collections.Generic;
using DanPie.Framework.AudioManagement;
using LudumDare53.Leveling;
using UnityEngine;

namespace LudumDare53.GameRules
{
    public class BossAnimation : MonoBehaviour
    {
        [SerializeField] private AudioClipDataProvider positiveSound;
        [SerializeField] private AudioClipDataProvider negativeSound;
        [SerializeField] private Animator animator;
        [SerializeField] private ReduceablePoints points;
        private AudioSourcesManager _manager;

        private void Start()
        {
            _manager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSourcesManager>();
            points.PointsChanged.AddListener(OnChanged);
        }


        private void OnChanged(float curPoints, float value)
        {
            if (value < -points.decreaseRate * 2)
            {
                animator.SetTrigger("Bad");
                _manager.GetAudioSourceController().Play(negativeSound.GetClipData());
            }
            else if (value >= points.decreaseRate)
            {
                animator.SetTrigger("Good");
                _manager.GetAudioSourceController().Play(positiveSound.GetClipData());
            }
            else if (value < 0)
            {
                animator.SetTrigger("Question");
                _manager.GetAudioSourceController().Play(negativeSound.GetClipData());
            }
        }
    }
}