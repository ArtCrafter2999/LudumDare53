using LudumDare53.Boxes;
using LudumDare53.Truck;
using UnityEngine;

namespace LudumDare53.Tutorial
{
    public class TutorialEventsExecutor : MonoBehaviour
    {
        [SerializeField] private SurfaceEffector2D _conveyor;
        [SerializeField] private Shredder _shredder;
        [SerializeField] private TruckController _truckController;
        [SerializeField] private PrefabSpawner _outlinedTruckSpawner;
        [SerializeField] private PrefabSpawner _outlinedBoxSpawner;
        [SerializeField] private PrefabSpawner _outlinedTrashSpawner;
        [SerializeField] private SpriteRenderer _conveyorOutline;
        [SerializeField] private SpriteRenderer _shredderOutline;

        public GameObject SpawnOutlinedBox()
            => _outlinedBoxSpawner.Spawn();

        public GameObject SpawnOutlinedTrash()
            => _outlinedTrashSpawner.Spawn();

        public GameObject SpawnOutlinedTruck()
            => _outlinedTruckSpawner.Spawn();

        public void SetConveyorOutlineState(bool enabled)
            => _conveyorOutline.enabled = enabled;

        public void SetShredderOutlineState(bool enabled)
            => _conveyorOutline.enabled = enabled;

        public void SetConveyorActiveState(bool enabled)
            => _conveyor.enabled = enabled;

        public void SetShredderActiveState(bool enabled)
            => _shredder.enabled = enabled;

        public void SetTruckControllerActiveState(bool enabled)
            => _truckController.enabled = enabled;
    }
}
