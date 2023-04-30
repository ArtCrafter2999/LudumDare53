using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace LudumDare53.UI
{
    public class TextAppearance : MonoBehaviour
    {
        [Header("Text")]
        [Tooltip("Could be empty for leaving existing text")]
        [SerializeField] private string text;
        [SerializeField] private TextMeshProUGUI mesh;
        [Header("Time")]
        [SerializeField] private float period;
        [SerializeField] private float delay;
        [Header("Other")]
        [SerializeField] private bool activateOnEnabled;

        public void OnEnable()
        {
            if(activateOnEnabled)Activate();
        }

        public void Activate()
        {
            if (text == "") text = mesh.text;
            mesh.text = "";
            StartCoroutine(Work());
        }

        private IEnumerator Work()
        {
            yield return new WaitForSeconds(delay);
            foreach (var t in text)
            {
                mesh.text += t;
                yield return new WaitForSeconds(period);
            }
        }
    }
}