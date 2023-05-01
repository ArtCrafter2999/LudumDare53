using UnityEngine;

namespace LudumDare53.Tutorial
{
    public class Outline : MonoBehaviour
    {
        [SerializeField] private Sprite _outlineSprite;
        [SerializeField] private SpriteRenderer _outlineSpriteRenderer;

        private Sprite _startSprite;

        public void Activate()
        {
            _outlineSpriteRenderer.sprite = _outlineSprite;
        }

        public void Deactivate()
        {
            _outlineSpriteRenderer.sprite = _startSprite;
        }

        protected void Start ()
        {
            _startSprite = _outlineSpriteRenderer.sprite;
        }
    }
}
