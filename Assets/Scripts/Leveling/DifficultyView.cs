using TMPro;
using UnityEngine;

namespace LudumDare53.Leveling
{
    public class DifficultyView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private int _difficultyValueOffset = 1;
        [SerializeField] private string _formatingText = "Day: {0}";

        protected void OnEnable()
        {
            _text.text = string.Format(_formatingText, DifficultyManager.Difficulty + _difficultyValueOffset);
        }
    }
}