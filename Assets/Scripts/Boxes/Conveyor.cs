using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DanPie.Framework.Randomnicity;
using LudumDare53.Leveling;
using LudumDare53.Truck;
using UnityEngine;

namespace LudumDare53.Boxes
{
    [RequireComponent(typeof(SurfaceEffector2D))]
    public class Conveyor : MonoBehaviour
    {
        private SurfaceEffector2D _surfaceEffector2D;

        [SerializeField] private TruckController _truckController;
        [SerializeField] private Transform _generatePoint;
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
            _truckController.TruckCountChanged.AddListener(UpdateSelector);
            OnDifficultyChanged();
            DifficultyManager.DifficultyChanged.AddListener(OnDifficultyChanged);
            _generationCoroutine = StartCoroutine(BoxGeneration());
        }

        private void OnDisable()
        {
            _truckController.TruckCountChanged.RemoveListener(UpdateSelector);
            DifficultyManager.DifficultyChanged.RemoveListener(OnDifficultyChanged);
            StopCoroutine(_generationCoroutine);
        }

        private void OnDifficultyChanged()
        {
            _currentDifficultyPresset = GetConveyorDifficultyPresset();
            _truckController.RecreateTrucks();       
            Speed = _currentDifficultyPresset.Speed;
            _seconds = _currentDifficultyPresset.FirstBoxSpawnDelay;
        }

        private void UpdateSelector(int a, int b)
        {
            _randomItemSelector
                = new RandomItemSelector<SpawnableObject>(_currentDifficultyPresset.SpawnableObjects
                .Where(x => _truckController.ActiveTrucks
                .Any(b =>
                {
                    bool gg = x.Prefab.TryGetComponent<BoxMarker>(out var mark);
                    if (!gg || mark.type == "trash") return true;
                    return mark.type.Equals(b.Marker);
                })).ToList());
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


                try
                {
                    Instantiate(
                                _randomItemSelector.GetRandomItem().Prefab,
                                _generatePoint.position,
                                Quaternion.identity
                            );
                }
                catch 
                {

                    throw;
                }
            }
        }

    }
}