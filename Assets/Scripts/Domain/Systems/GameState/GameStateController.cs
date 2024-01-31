using Domain.Systems.Collision;
using Framework.Objects;
using UnityEngine;

namespace Domain.Systems.GameState {
    public class GameStateController {
        public GameStateController() {
            CollisionSystem.playerHit += PlayerHitHandler;
        }

        private void PlayerHitHandler(ICollider enemy) {
            GameOver();
        }

        private void GameOver() {
            Debug.LogWarning("Game Over!");
        }
    }
}