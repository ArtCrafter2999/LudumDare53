using System;
using System.Collections;
using System.Collections.Generic;
using LudumDare53.Leveling;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace LudumDare53.Boxes
{
    public class Conveyor : MonoBehaviour
    {
        [SerializeField] private List<GameObject> boxesPool; 
        [SerializeField] private Transform startPoint; 
        [SerializeField] private Transform endPoint;
        [SerializeField] private float speed;
        [SerializeField] private float period;
        private void Start()
        {
            StartCoroutine(BoxGeneration());
        }
        private IEnumerator BoxGeneration()
        {
            while (gameObject.activeSelf)
            {
                yield return new WaitForSeconds(period);
                if (PauseManager.IsPaused) yield return new WaitWhile(() => PauseManager.IsPaused);
                if(boxesPool.Count == 0) continue;
                var obj = Instantiate(boxesPool[Random.Range(0, boxesPool.Count)], startPoint.position, Quaternion.identity);
                var boxOnConveyor = obj.AddComponent<BoxOnConveyor>();
                boxOnConveyor.Init(endPoint, speed);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(startPoint.position, endPoint.position);
        }
    }
}