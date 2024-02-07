﻿using Model.Core.Game;
using UnityEngine;

namespace UnityView.GUI {
    public class GuiController : GuiBase {
        public GameObject bgAudio;
        public GameScreen gameScreen;
        public GameOverScreen gameOverScreen;

        private void Start() {
            States.game.Status.Changed += OnGameStatusChanged;
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