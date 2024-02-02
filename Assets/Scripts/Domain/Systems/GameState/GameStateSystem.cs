using Core.Interface.Objects;
using Domain.Systems.Collision;
using UnityEngine;

namespace Domain.Systems.GameState {
    public class GameStateSystem {
        public GameStateSystem() {
            CollisionSystem.PlayerHit += PlayerHitHandler;
        }

        private void PlayerHitHandler(ICollider enemy) {
            GameOver();
        }

        private void GameOver() {
            // todo
            // Debug.LogWarning("Game Over!");
        }
    }
}