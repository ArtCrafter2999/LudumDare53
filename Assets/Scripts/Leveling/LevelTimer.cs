using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LudumDare53.Leveling
{
    public class LevelTimer : MonoBehaviour
    {
        [SerializeField]
        private Text _text;
        [SerializeField]
        private float maxTime;

        public UnityEvent timePassed;

        public float _timer;
        private void Start()
        {
            _timer = maxTime;
        }
        private void Update()
        {
            if(this.IsOnPause()) return;
            if(_timer <= 0) timePassed.Invoke();
            _timer -= Time.deltaTime;
            var span = TimeSpan.FromSeconds(_timer);
            var eblan = span.ToString("mm\\:ss");
            _text.text = $"It remains to work: {eblan}";
        }

        public void Reload()
        {
            _timer = maxTime;
        }
    }
}

