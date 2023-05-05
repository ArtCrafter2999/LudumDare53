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
        [SerializeField] private float cooldown;
        [SerializeField] float _cooldown;
        private AudioSourcesManager _manager;

        private void Start()
        {
            _manager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSourcesManager>();
            points.pointsChanged.AddListener(OnChanged);
            points.pointsNaturallyChanged.AddListener(_ => OnNaturallyChanged());
        }

        private void Update()
        {
            if (_cooldown > 0) _cooldown -= Time.deltaTime;
        }

        private void OnNaturallyChanged()
        {
            if (_cooldown > 0)return;
            animator.SetTrigger("Question");
            _manager.GetAudioSourceController().Play(negativeSound.GetClipData());
        }

        private void OnChanged(float value)
        {
            if (_cooldown > 0)return;
            switch (value)
            {
                case < 0:
                    animator.SetTrigger("Bad");
                    _manager.GetAudioSourceController().Play(negativeSound.GetClipData());
                    break;
                case >= 1:
                    animator.SetTrigger("Good");
                    _manager.GetAudioSourceController().Play(positiveSound.GetClipData());
                    break;
            }
            _cooldown = cooldown;
        }
    }
}