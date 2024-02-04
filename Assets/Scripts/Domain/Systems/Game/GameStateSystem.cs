using System;
using Core.Game;
using Core.Interface.Objects;
using Core.State;
using Domain.Systems.Collision;
using UnityEngine;

namespace Domain.Systems.Game {
    public class GameStateSystem {
        private GameState State { get; }

        public static event Action NewGameEvent;
        public static event Action GameOverEvent;

        public GameStateSystem(GameState state) {
            State = state;

            CollisionSystem.PlayerHit += PlayerHitHandler;
            State.ContinueFlag.Changed += OnContinueOnChanged;

            NewGame();
        }

        private void OnContinueOnChanged(bool toContinue) {
            if (toContinue) NewGame();
            State.ContinueFlag.Value = false;
        }

        private void PlayerHitHandler(ICollider enemy) {
            GameOver();
        }

        private void NewGame() {
            if (State.Status.Value == GameStatus.Playing) return;
            State.Status.Value = GameStatus.Playing;
            NewGameEvent?.Invoke();
            Debug.Log("NewGame");
        }

        private void GameOver() {
            if (State.Status.Value == GameStatus.End) return;
            State.Status.Value = GameStatus.End;
            GameOverEvent?.Invoke();
            Debug.Log("GameOver");
        }
    }
}