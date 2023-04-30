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
        [SerializeField] private float width;
        [SerializeField] private Transform endPoint;
        private Vector2 StartPos => endPoint.position + Vector3.right * width;
        [SerializeField] private float speed;
        [SerializeField] private float period;

        private void Start()
        {
            StartCoroutine(BoxGeneration());
        }

        private IEnumerator BoxGeneration()
        {
            float seconds = 0;
            while (gameObject.activeSelf)
            {
                yield return new WaitForFixedUpdate();
                if (seconds > 0)
                {
                    if(!PauseManager.IsPaused) seconds -= Time.deltaTime;
                    continue;
                }
                seconds = period;
                
                if (boxesPool.Count == 0) continue;
                var randomIndex = Random.Range(0, boxesPool.Count);
                var obj = Instantiate(
                    boxesPool[randomIndex],
                    StartPos,
                    Quaternion.identity
                );
                obj.GetComponent<BoxOnConveyor>().Init(StartPos, endPoint.position, speed);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(StartPos, endPoint.position);
        }
    }
}