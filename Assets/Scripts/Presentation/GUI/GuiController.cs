using Core.Game.States;
using UnityEngine;

namespace Presentation.GUI {
    public class GuiController : GuiBase {
        public GameObject bgAudio;
        public GameScreen gameScreen;
        public GameOverScreen gameOverScreen;

        private void Start() {
            Data.GameState.GameState.Changed += OnGameStateChanged;
            OnGameStateChanged(GameState.Playing); // hack
        }

        private void OnGameStateChanged(GameState gameState) {
            if (gameState == GameState.Playing) {
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