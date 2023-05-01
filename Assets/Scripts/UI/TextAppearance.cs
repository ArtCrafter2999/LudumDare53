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
            _mesh = GetComponent<TextMeshProUGUI>();
        }

        public void OnEnable()
        {
            if(activateOnEnabled)Activate();
        }

        public void Activate()
        {
            if (text == "") text = _mesh.text;
            _mesh.text = "";
            StartCoroutine(Work());
        }

        private IEnumerator Work()
        {
            yield return new WaitForSeconds(delay);
            foreach (var t in text)
            {
                _mesh.text += t;
                yield return new WaitForSeconds(period);
                if (!_forceEnd) continue;
                _mesh.text = text;
                break;
            }
        }

        public void ForceEnd()
        {
            _forceEnd = true;
        }
    }
}