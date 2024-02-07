using TMPro;

namespace Presentation.GUI {
    public class GameOverScreen : GuiBase {
        public TextMeshProUGUI score;

        private void OnEnable() {
            if (States == null) return;
            score.SetText($"Score\n{States.score.Points.Value}");
        }
    }
}