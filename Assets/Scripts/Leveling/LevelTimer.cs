using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LudumDare53.Leveling
{
    public class LevelTimer : MonoBehaviour
    {
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
        }

        public void Reload()
        {
            _timer = maxTime;
        }
    }
}

