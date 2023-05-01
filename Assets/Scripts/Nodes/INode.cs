using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace LudumDare53.Nodes
{
    public interface INode
    {
        NodePlayer NodePlayer { get; set; }


        void Init();
        bool Invoke();
        void Break();
        void Stop();
        void Pause();
        bool Skip();
    }
}
