using System.Collections;
using System.Collections.Generic;
using LudumDare53.Leveling;
using UnityEngine;

namespace LudumDare53.Boxes
{
    [RequireComponent(typeof(SurfaceEffector2D))]
    public class Conveyor : MonoBehaviour
    {
        private SurfaceEffector2D _surfaceEffector2D;
        
        [Header("BoxesGeneration")]
        [SerializeField] private Transform generatePoint;
        [SerializeField] private List<GameObject> ordinaryBoxes;
        [SerializeField] private List<GameObject> coloredBoxes;

        private float speed
        {
            get => -_surfaceEffector2D.speed;
            set => _surfaceEffector2D.speed = -value;
        }
        private float period;
        private float coloredBoxChance;

        private void Start()
        {
            _surfaceEffector2D = GetComponent<SurfaceEffector2D>();
            switch (DifficultyManager.Difficulty) //TODO: Прописати значення для різних рівнів складності
            {
                default:
                case 0:
                    coloredBoxChance = 0f;
                    speed = 2;
                    period = 10;
                    break;
                case 1:
                    coloredBoxChance = 0.1f;
                    speed = 2;
                    period = 10;
                    break;
                case 2:
                    coloredBoxChance = 0.1f;
                    speed = 2;
                    period = 10;
                    break;
                case 3:
                    coloredBoxChance = 0.1f;
                    speed = 2;
                    period = 10;
                    break;
                case 4:
                    coloredBoxChance = 0.1f;
                    speed = 2;
                    period = 10;
                    break;
                case 5: 
                    coloredBoxChance = 0.1f;
                    speed = 2;
                    period = 10;
                    break;
            }
            StartCoroutine(BoxGeneration());
        }

        private IEnumerator BoxGeneration()
        {
            var seconds = period;
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
                    generatePoint.position,
                    Quaternion.identity
                );
            }
        }

        private GameObject RandomizeBox()
        {
            return Random.value > coloredBoxChance ?
                ordinaryBoxes[Random.Range(0, ordinaryBoxes.Count)] :
                coloredBoxes[Random.Range(0, coloredBoxes.Count)];
        }
    }
}