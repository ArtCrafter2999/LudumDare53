using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace LudumDare53.Nodes
{
    [CreateAssetMenu(menuName = "Nodes/EventNode")]
    public class EventNode : NodeBase
    {
        public UnityEvent Event;
        public override bool Invoke()
        {
            Event.Invoke();
            return true;
        }
    }
}



