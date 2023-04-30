using System;
using System.Collections;
using System.Collections.Generic;
using LudumDare53.Leveling;
using UnityEngine;

namespace LudumDare53.Boxes
{
    public class BoxOnConveyor : MonoBehaviour
    {
        public void OnDragStarted()
        {
            gameObject.layer = 6;
        }

        public void OnDragEnded()
        {
            gameObject.layer = 0;
        }
    }
}