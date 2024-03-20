using Core.Framework.Systems;
using Core.Systems.Interface;
using JetBrains.Annotations;
using Model.Data.State;
using Model.Entity.Interface;

namespace Core.Systems {
    [UsedImplicitly]
    public class ScoreSystem : SystemBase, IScoreSystem {
        private ScoreSystemState State { get; }

        public ScoreSystem(ScoreSystemState state) {
            State = state;

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