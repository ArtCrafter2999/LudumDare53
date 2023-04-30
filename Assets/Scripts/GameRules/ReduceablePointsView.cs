using UnityEngine;
using UnityEngine.UI;


namespace LudumDare53.GameRules
{
    public class ReduceablePointsView : MonoBehaviour
    {
        [SerializeField] private Slider _progressBar;
        [SerializeField] private ReduceablePoints _reduceablePoints;

        protected void OnEnable()
        {
            _progressBar.value = _reduceablePoints.CurrentPoints;
            _reduceablePoints.PointsChanged.AddListener(OnPointsChanged);
        }

        protected void OnDisable()
        {
            _reduceablePoints.PointsChanged.RemoveListener(OnPointsChanged);
        }

        private void OnPointsChanged(float newValue)
        {
            _progressBar.value = newValue;
        }
    }

}