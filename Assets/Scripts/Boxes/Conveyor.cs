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
                var wrap = new GameObject { transform = { position = startPoint.position } };
                var randomIndex = Random.Range(0, boxesPool.Count);
                var obj = Instantiate(
                    boxesPool[randomIndex],
                    wrap.transform.position,
                    Quaternion.identity,
                    wrap.transform
                );
                var halfOfHeight = obj
                    .GetComponent<Collider2D>()
                    .bounds.size.y / 2;
                obj.transform.localPosition = Vector3.up * halfOfHeight;
                var boxOnConveyor = wrap.AddComponent<BoxOnConveyor>();
                boxOnConveyor.Init(wrap.transform, endPoint, speed);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(startPoint.position, endPoint.position);
        }
    }
}