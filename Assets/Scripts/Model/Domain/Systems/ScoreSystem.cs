﻿using JetBrains.Annotations;
using Model.Core.Data.State;
using Model.Core.Interface.Entity;
using Model.Domain.Systems.Base;

namespace Model.Domain.Systems {
    [UsedImplicitly]
    public class ScoreSystem : SystemBase {
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