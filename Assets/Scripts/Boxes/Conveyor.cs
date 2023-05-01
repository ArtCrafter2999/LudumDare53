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

        private float Speed
        {
            get => -_surfaceEffector2D.speed;
            set => _surfaceEffector2D.speed = -value;
        }
        private float _period;
        private float _coloredBoxChance;

        private void Start()
        {
            _surfaceEffector2D = GetComponent<SurfaceEffector2D>();
            DifficultyManager.DifficultyChanged.AddListener(OnDifficultyChanged);
            OnDifficultyChanged();
            StartCoroutine(BoxGeneration());
        }

        private void OnDifficultyChanged()
        {
            switch (DifficultyManager.Difficulty) //TODO: Прописати значення для різних рівнів складності
            {
                default:
                case 0:
                    _coloredBoxChance = 0f;
                    Speed = 2;
                    _period = 6;
                    break;
                case 1:
                    _coloredBoxChance = 0.1f;
                    Speed = 2;
                    _period = 10;
                    break;
                case 2:
                    _coloredBoxChance = 0.1f;
                    Speed = 2;
                    _period = 10;
                    break;
                case 3:
                    _coloredBoxChance = 0.1f;
                    Speed = 2;
                    _period = 10;
                    break;
                case 4:
                    _coloredBoxChance = 0.1f;
                    Speed = 2;
                    _period = 10;
                    break;
                case 5: 
                    _coloredBoxChance = 0.1f;
                    Speed = 2;
                    _period = 10;
                    break;
            }
        }

        private IEnumerator BoxGeneration()
        {
            var seconds = _period;
            while (gameObject.activeSelf)
            {
                yield return new WaitForFixedUpdate();
                if (Physics2D.OverlapPointAll(generatePoint.position).Length > 0) continue;
                if (seconds > 0)
                {
                    if(!this.IsOnPause()) seconds -= Time.deltaTime;
                    continue;
                }
                seconds = _period;
                
                if (ordinaryBoxes.Count == 0 && coloredBoxes.Count == 0) continue;
                Instantiate(
                    RandomizeBox(),
                    generatePoint.position,
                    Quaternion.identity
                );
            }
        }

        private GameObject RandomizeBox()
        {
            if(ordinaryBoxes.Count == 0) return coloredBoxes[Random.Range(0, coloredBoxes.Count)];
            if(coloredBoxes.Count == 0) return ordinaryBoxes[Random.Range(0, ordinaryBoxes.Count)];
            return Random.value > _coloredBoxChance ?
                ordinaryBoxes[Random.Range(0, ordinaryBoxes.Count)] :
                coloredBoxes[Random.Range(0, coloredBoxes.Count)];
        }
    }
}