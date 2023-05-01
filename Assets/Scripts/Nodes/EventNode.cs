using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace LudumDare53.Nodes
{
    [CreateAssetMenu(menuName = "Nodes/EventNode")]
    public class EventNode : NodeBase
    {
        public bool waitForSkip;
        public bool canSkip;
        public UnityEvent invoked;
        public UnityEvent skipped;
        public UnityEvent broken;
        public UnityEvent stopped;
        public UnityEvent paused;
        public UnityEvent initialized;

        public override void Stop()
        {
            stopped.Invoke();
        }

        public override void Pause()
        {
            paused.Invoke();
        }

        public override void Init()
        {
            initialized.Invoke();
        }

        public override bool Invoke()
        {
            invoked.Invoke();
            return !waitForSkip;
        }

        public override bool Skip()
        {
            var doskip = canSkip && waitForSkip;
            skipped.Invoke();
            return doskip;
        }

        public override void Break()
        {
            broken.Invoke();
        }
    }
}



