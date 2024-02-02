using System;
using Core.Data;
using Core.Input;
using Core.Interface.Objects;
using Domain.Systems.Collision;
using UnityEngine;

namespace Domain.Systems.GameState {
    public class GameStateSystem {
        private GameStateData Data { get; }

        public static event Action NewGameEvent;
        public static event Action GameOverEvent;

        public GameStateSystem(GameStateData data) {
            Data = data;

            CollisionSystem.PlayerHit += PlayerHitHandler;
            InputController.Continue += NewGame;

            NewGame();
        }

        private void PlayerHitHandler(ICollider enemy) {
            GameOver();
        }

        private void NewGame() {
            if (Data.GameState.Value == Core.Game.States.GameState.Playing) return;
            Data.GameState.Value = Core.Game.States.GameState.Playing;
            NewGameEvent?.Invoke();
            Debug.Log("NewGame");
        }

        private void GameOver() {
            if (Data.GameState.Value == Core.Game.States.GameState.End) return;
            Data.GameState.Value = Core.Game.States.GameState.End;
            GameOverEvent?.Invoke();
            Debug.Log("GameOver");
        }
    }
}