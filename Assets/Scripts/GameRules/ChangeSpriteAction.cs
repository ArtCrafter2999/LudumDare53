using System;
using UnityEngine;

namespace LudumDare53.GameRules
{
    [Serializable]
    public class ChangeSpriteAction : IBossAction
    {
        [SerializeField] private Sprite _sprite;

        public void MakeBossAction(Boss boss)
        {
            boss.BossRenderer.sprite = _sprite;
        }
    }
}
