using Model.Data.State;
using Model.Game;
using UnityEngine;
using UnityView.Base;

namespace UnityView.GUI {
    public class GuiController : MonoBase {
        public GameObject bgAudio;
        public GameScreen gameScreen;
        public GameOverScreen gameOverScreen;

        private void Start() {
            GameSystemState gameSystemState = States.Get<GameSystemState>();

            gameSystemState.Status.Changed += OnGameStatusChanged;
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