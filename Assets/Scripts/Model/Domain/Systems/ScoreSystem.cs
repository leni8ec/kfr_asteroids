using Model.Core.Adapters;
using Model.Core.Data;
using Model.Core.Data.State;
using Model.Core.Interface.Entity;
using Model.Domain.Systems.Base;

namespace Model.Domain.Systems {
    public class ScoreSystem : SystemBase {
        private ScoreState State { get; }

        public ScoreSystem(DataCollector data, AdaptersCollector adapters) {
            State = data.States.score;

            GameStateSystem.NewGameEvent += Reset;
            CollisionSystem.EnemyHitEvent += EnemyHitHandler;
        }

        private void Reset() {
            State.Points.Value = 0;
        }

        private void EnemyHitHandler(ICollider enemy, ICollider ammo) {
            State.Points.Value++;
        }

    }
}