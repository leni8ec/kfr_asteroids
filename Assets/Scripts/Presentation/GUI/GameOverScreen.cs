using TMPro;

namespace Presentation.GUI {
    public class GameOverScreen : GuiBase {
        public TextMeshProUGUI score;

        private void OnEnable() {
            if (State == null) return;
            score.SetText($"Score\n{State.score.Points.Value}");
        }
    }
}