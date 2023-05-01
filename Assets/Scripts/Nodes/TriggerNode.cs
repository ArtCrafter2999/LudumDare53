using UnityEngine;

namespace LudumDare53.Nodes
{
    [CreateAssetMenu(menuName = "Nodes/TriggerNode")]
    public class TriggerNode : NodeBase
    {
        private bool isTriggered;

        public override void Init()
        {
            isTriggered = true;//TODO: Замінити на False коли з'являться евенти 
        }
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