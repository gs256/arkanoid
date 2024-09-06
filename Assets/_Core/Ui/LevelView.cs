using TMPro;
using UnityEngine;

namespace Arkanoid.Ui
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _label;

        private string _labelFormat;

        private void Awake()
        {
            _labelFormat = _label.text;
        }

        public void ShowLevel(int levelNumber)
        {
            Debug.Assert(levelNumber > 0);
            _label.text = string.Format(_labelFormat, levelNumber);
        }
    }
}
