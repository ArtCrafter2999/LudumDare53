using System.Collections;
using System.Collections.Generic;
using DanPie.Framework.Randomnicity;
using LudumDare53.Leveling;
using UnityEngine;

namespace LudumDare53.Boxes
{
    [RequireComponent(typeof(SurfaceEffector2D))]
    public class Conveyor : MonoBehaviour
    {
        private SurfaceEffector2D _surfaceEffector2D;

        [SerializeField] private Transform generatePoint;
        [SerializeField] private List<ConveyorDifficultyPresset> _difficultyPressets;

        private ConveyorDifficultyPresset _currentDifficultyPresset;
        private RandomItemSelector<SpawnableObject> _randomItemSelector;
        private Coroutine _generationCoroutine;
        private float _seconds;

        private float Speed
        {
            get => -_surfaceEffector2D.speed;
            set => _surfaceEffector2D.speed = -value;
        }

        private void OnEnable()
        {
            _surfaceEffector2D = GetComponent<SurfaceEffector2D>();
            OnDifficultyChanged();
            DifficultyManager.DifficultyChanged.AddListener(OnDifficultyChanged);
            _generationCoroutine = StartCoroutine(BoxGeneration());
        }

        private void OnDisable()
        {
            DifficultyManager.DifficultyChanged.RemoveListener(OnDifficultyChanged);
            StopCoroutine(_generationCoroutine);
        }

        private void OnDifficultyChanged()
        {
            _currentDifficultyPresset = GetConveyorDifficultyPresset();
            _randomItemSelector = new RandomItemSelector<SpawnableObject>(_currentDifficultyPresset.SpawnableObjects);
            Speed = _currentDifficultyPresset.Speed;
            _seconds = _currentDifficultyPresset.FirstBoxSpawnDelay;
        }

        private ConveyorDifficultyPresset GetConveyorDifficultyPresset()
        {
            return _difficultyPressets[Mathf.Clamp(DifficultyManager.Difficulty, 0, _difficultyPressets.Count - 1)];
        }

        private IEnumerator BoxGeneration()
        {
            while (gameObject.activeSelf)
            {
                yield return new WaitForFixedUpdate();

                if (_seconds > 0)
                {
                    if(!this.IsOnPause()) _seconds -= Time.deltaTime;
                    continue;
                }
                _seconds = _currentDifficultyPresset.Period;
                

                Instantiate(
                    _randomItemSelector.GetRandomItem().Prefab,
                    generatePoint.position,
                    Quaternion.identity
                );
            }
        }

    }
}