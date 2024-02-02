using Core.Data;
using Core.Interface.Objects;
using Domain.Systems.Collision;
using Domain.Systems.GameState;

namespace Domain.Systems.Gameplay {
    public class ScoreSystem {
        public ScoreData Data { get; }

        public ScoreSystem(ScoreData data) {
            Data = data;

            CollisionSystem.EnemyHit += OnEnemyHit;
            GameStateSystem.NewGameEvent += Reset;
        }

        private void Reset() {
            Data.points = 0;
        }

        private void OnEnemyHit(ICollider enemy, ICollider ammo) {
            Data.points++;
        }

    }
}