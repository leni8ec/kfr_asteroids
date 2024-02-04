using Core.Game;
using UnityEngine;

namespace Presentation.GUI {
    public class GuiController : GuiBase {
        public GameObject bgAudio;
        public GameScreen gameScreen;
        public GameOverScreen gameOverScreen;

        private void Start() {
            State.game.Status.Changed += OnGameStatusChanged;
            OnGameStatusChanged(GameStatus.Playing); // hack
        }

        private void OnGameStatusChanged(GameStatus gameStatus) {
            if (gameStatus == GameStatus.Playing) {
                gameScreen.gameObject.SetActive(true);
                gameOverScreen.gameObject.SetActive(false);
                bgAudio.SetActive(true);
            } else {
                gameScreen.gameObject.SetActive(false);
                gameOverScreen.gameObject.SetActive(true);
                bgAudio.SetActive(false);
            }
        }
    }
}