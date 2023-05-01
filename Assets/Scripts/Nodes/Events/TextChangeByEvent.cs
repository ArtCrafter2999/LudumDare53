using LudumDare53.UI;
using UnityEngine;

namespace LudumDare53.Nodes.Events
{
    public class TextChangeByEvent : MonoBehaviour
    {
        [SerializeField] private EventNode node;
        [SerializeField] private TextAppearance textAppearance;
        [SerializeField] private string text;

        public void Start()
        {
            node.invoked.AddListener(Invoke);
            node.broken.AddListener(Break);
        }

        private void Invoke()
        {
            textAppearance.text = text;
            textAppearance.Activate();
        }
        private void Break()
        {
            textAppearance.ForceEnd();
        }
    }
}