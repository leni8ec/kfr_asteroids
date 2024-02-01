using Domain.Systems.Collision;
using Framework.Objects;
using UnityEngine;

namespace Domain.Systems.GameState {
    public class GameStateController {
        public GameStateController() {
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