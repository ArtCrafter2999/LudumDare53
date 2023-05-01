using UnityEngine;

namespace LudumDare53.Nodes
{
    public class MessageNode : NodeBase
    {
        [SerializeField] private GameObject messageObject;
        public override bool Invoke()
        {
            messageObject.SetActive(true);
            return false;
        }

        public override bool Skip()
        {
            return true;
        }

        public override void Break()
        {
            messageObject.SetActive(false);
        }
    }
}