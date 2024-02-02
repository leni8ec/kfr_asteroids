using System;
using Core.Game;
using Core.Input;
using Core.Interface.Objects;
using Core.State;
using Domain.Systems.Collision;
using UnityEngine;

namespace Domain.Systems.Game {
    public class GameStateSystem {
        private GameState Data { get; }

        public static event Action NewGameEvent;
        public static event Action GameOverEvent;

        public GameStateSystem(GameState data) {
            Data = data;

            CollisionSystem.PlayerHit += PlayerHitHandler;
            InputController.Continue += NewGame;

            NewGame();
        }

        private void PlayerHitHandler(ICollider enemy) {
            GameOver();
        }

        private void NewGame() {
            if (Data.Status.Value == GameStatus.Playing) return;
            Data.Status.Value = GameStatus.Playing;
            NewGameEvent?.Invoke();
            Debug.Log("NewGame");
        }

        private void GameOver() {
            if (Data.Status.Value == GameStatus.End) return;
            Data.Status.Value = GameStatus.End;
            GameOverEvent?.Invoke();
            Debug.Log("GameOver");
        }
    }
}