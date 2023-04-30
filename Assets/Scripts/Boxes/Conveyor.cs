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
        [Header("Form")]
        [SerializeField] private Transform endPoint;
        [SerializeField] private float width;
        [Header("Boxes")]
        [SerializeField] private List<GameObject> ordinaryBoxes;
        [SerializeField] private List<GameObject> coloredBoxes;

        private Vector2 StartPos => endPoint.position + Vector3.right * width;
        private float speed;
        private float period;
        private float coloredBoxChance;

        private void Start()
        {
            switch (DifficultyManager.Difficulty) //TODO: Прописати значення для різних рівнів складності
            {
                default:
                case 0:
                    coloredBoxChance = 0.1f;
                    speed = 1;
                    period = 10;
                    break;
                case 1:
                    coloredBoxChance = 0.1f;
                    speed = 1;
                    period = 10;
                    break;
                case 2:
                    coloredBoxChance = 0.1f;
                    speed = 1;
                    period = 10;
                    break;
                case 3:
                    coloredBoxChance = 0.1f;
                    speed = 1;
                    period = 10;
                    break;
                case 4:
                    coloredBoxChance = 0.1f;
                    speed = 1;
                    period = 10;
                    break;
                case 5: 
                    coloredBoxChance = 0.1f;
                    speed = 1;
                    period = 10;
                    break;
            }
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
                
                if (ordinaryBoxes.Count == 0 && coloredBoxes.Count == 0) continue;
                var obj = Instantiate(
                    RandomizeBox(),
                    StartPos,
                    Quaternion.identity
                );
                obj.GetComponent<BoxOnConveyor>().Init(StartPos, endPoint.position, speed);
            }
        }

        private GameObject RandomizeBox()
        {
            return Random.value > coloredBoxChance ?
                ordinaryBoxes[Random.Range(0, ordinaryBoxes.Count)] :
                coloredBoxes[Random.Range(0, coloredBoxes.Count)];
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(StartPos, endPoint.position);
        }
    }
}