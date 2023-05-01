using UnityEngine;

namespace LudumDare53.Nodes
{
    [CreateAssetMenu(menuName = "Nodes/TriggerNode")]
    public class TriggerNode : NodeBase
    {
        private bool isTriggered = false;
        public override bool Invoke()
        {
            return isTriggered;
        }

        public void Trigger()
        {
            isTriggered = true;
        }
    }
}