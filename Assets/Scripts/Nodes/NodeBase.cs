using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace LudumDare53.Nodes
{
    public abstract class NodeBase : ScriptableObject, INode
    {
        public NodePlayer NodePlayer { get; set; }

        public virtual void Break() { }
        public virtual void Stop() { }
        public virtual void Pause() { }
        public virtual bool Skip() => false;
        public virtual void Init() { }
        public abstract bool Invoke();
    }
}

