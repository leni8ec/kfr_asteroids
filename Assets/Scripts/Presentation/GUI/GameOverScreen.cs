using TMPro;

namespace Presentation.GUI {
    public class GameOverScreen : GuiBase {
        public TextMeshProUGUI score;

        private void OnEnable() {
            if (Data == null) return;
            score.SetText($"Score\n{Data.Score.Points.Value}");
        }
    }
}