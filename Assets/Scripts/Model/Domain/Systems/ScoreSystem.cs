using Model.Core.Interface.Objects;
using Model.Core.State;
using Model.Core.Unity;
using Model.Domain.Systems.Base;

namespace Model.Domain.Systems {
    public class ScoreSystem : SystemBase {
        private ScoreState State { get; }

        public ScoreSystem(StateCollector state, ConfigCollector config, PrefabCollector prefab) {
            State = state.score;

            CollisionSystem.EnemyHit += EnemyHitHandler;
            GameStateSystem.NewGameEvent += ResetHandler;
        }

        private void ResetHandler() {
            State.Points.Value = 0;
        }

        private void EnemyHitHandler(ICollider enemy, ICollider ammo) {
            State.Points.Value++;
        }

    }
}