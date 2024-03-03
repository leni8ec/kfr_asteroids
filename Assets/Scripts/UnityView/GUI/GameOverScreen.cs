using Model.Data.State;
using TMPro;
using UnityView.Base;

namespace UnityView.GUI {
    public class GameOverScreen : MonoBase {
        public TextMeshProUGUI score;

        private ScoreSystemState scoreSystemState;

        private void Start() {
            scoreSystemState = States.Get<ScoreSystemState>();
        }

        private void OnEnable() {
            if (States == null) return;
            score.SetText($"Score\n{scoreSystemState.Points.Value}");
        }
    }
}