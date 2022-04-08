using _Project.CodeBase.Logic.Player;
using TMPro;
using UnityEngine;

namespace _Project.CodeBase.UI.Elements
{
    public class HeroLevelUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelText;

        public void Construct(HeroStickmanBehaviour stickmanBehaviour)
        {
            stickmanBehaviour.OnLevelChanged += OnLevelChanged;
            OnLevelChanged(stickmanBehaviour.Level);
        }

        private void OnLevelChanged(int level) => 
            _levelText.text = level.ToString();
    }
}