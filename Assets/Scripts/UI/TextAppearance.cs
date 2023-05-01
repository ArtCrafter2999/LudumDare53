using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace LudumDare53.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextAppearance : MonoBehaviour
    {
        [Header("Text")]
        [Tooltip("Could be empty for leaving existing text")]
        public string text;
        private TextMeshProUGUI _mesh;
        [Header("Time")]
        [SerializeField] private float period = 0.1f;
        [SerializeField] private float delay;
        [Header("Other")]
        [SerializeField] private bool activateOnEnabled;

        private bool _forceEnd = false; 

        public void Start()
        {
            if(_mesh == null)
                _mesh = GetComponent<TextMeshProUGUI>();
        }

        public void OnEnable()
        {
            if(_mesh == null)
                _mesh = GetComponent<TextMeshProUGUI>();
            if(activateOnEnabled)Activate();
        }

        public bool isEnd = true;
        private Coroutine _animationWork;
        public void Activate()
        {
            if (_animationWork != null) StopCoroutine(_animationWork);
            if (text == "") text = _mesh.text;
            _mesh.text = "";
            isEnd = false;
            _forceEnd = false;
            _animationWork = StartCoroutine(Work());
        }

        private IEnumerator Work()
        {
            yield return new WaitForSeconds(delay);
            _forceEnd = false;
            foreach (var t in text)
            {
                _mesh.text += t;
                yield return new WaitForSeconds(period);
                if (!_forceEnd) continue;
                _forceEnd = false;
                _mesh.text = text;
                break;
            }
            isEnd = true;
        }

        public void ForceEnd()
        {
            _forceEnd = true;
            _mesh.text = text;
            isEnd = true;
        }
    }
}