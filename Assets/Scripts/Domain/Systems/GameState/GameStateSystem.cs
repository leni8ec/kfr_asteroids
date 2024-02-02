using System;
using Core.Interface.Objects;
using Domain.Systems.Collision;

namespace Domain.Systems.GameState {
    public class GameStateSystem {
        public static event Action NewGameEvent;
        public static event Action GameOverEvent;

        public GameStateSystem() {
            CollisionSystem.PlayerHit += PlayerHitHandler;
        }

        private void PlayerHitHandler(ICollider enemy) {
            GameOver();
        }

        private void GameOver() {
            GameOverEvent?.Invoke();
            // todo
            // Debug.LogWarning("Game Over!");
        }
    }
}