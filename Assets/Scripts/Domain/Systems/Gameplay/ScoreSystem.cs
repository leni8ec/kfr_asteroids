using Core.Interface.Objects;
using Core.State;
using Domain.Systems.Collision;
using Domain.Systems.Game;

namespace Domain.Systems.Gameplay {
    public class ScoreSystem {
        public ScoreState State { get; }

        public ScoreSystem(ScoreState state) {
            State = state;

            CollisionSystem.EnemyHit += OnEnemyHit;
            GameStateSystem.NewGameEvent += Reset;
        }

        private void Reset() {
            State.Points.Value = 0;
        }

        private void OnEnemyHit(ICollider enemy, ICollider ammo) {
            State.Points.Value++;
        }

    }
}