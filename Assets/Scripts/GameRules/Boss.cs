using LudumDare53.Core;
using SimpleHeirs;
using UnityEngine;
using UnityEngine.UI;

namespace LudumDare53.GameRules
{
    public class Boss : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _bossRenderer;
        [SerializeField] private ReduceablePoints _patience;
        [SerializeField] private ProgressObjectProvider<HeirsProvider<IBossAction>> _bossActions;

        private HeirsProvider<IBossAction> _previousBossAction;

        public SpriteRenderer BossRenderer { get => _bossRenderer; }

        protected void OnEnable()
        {
            _patience.PointsChanged.AddListener(OnPatiencePointsChanged);
        }
        
        protected void OnDisable()
        {
            _patience.PointsChanged.RemoveListener(OnPatiencePointsChanged);
        }

        private void OnPatiencePointsChanged(float newValue)
        {
            HeirsProvider<IBossAction> bossAction
                = _bossActions.GetObjectByProgress(newValue);

            if (_previousBossAction != bossAction)
            {
                bossAction.GetValue()?.MakeBossAction(this);
            }

            _previousBossAction = bossAction;
        }
    }
}
