using TMPro;
using UnityEngine;

namespace Arkanoid.Ui
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _label;

        private string _labelFormat;

        private void Awake()
        {
            _labelFormat = _label.text;
        }

        private void Start()
        {
            ShowScore(0);
        }

        public void ShowScore(int score)
        {
            Debug.Assert(score >= 0);
            _label.text = string.Format(_labelFormat, score);
        }
    }
}
