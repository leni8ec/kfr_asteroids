﻿using Model.Core.Adapters;
using Model.Core.Data;
using Model.Core.Data.State;
using Model.Core.Interface.Entity;
using Model.Core.Pools;
using Model.Domain.Systems.Base;

namespace Model.Domain.Systems {
    public class EntityUpdateSystem : SystemBase, IUpdateSystem {
        private EntitiesState State { get; }

        private bool active;

        public EntityUpdateSystem(DataCollector data, AdaptersCollector adapters) {
            State = data.States.entity;

            // Game state listeners
            GameStateSystem.NewGameEvent += Play;
            GameStateSystem.GameOverEvent += Reset;
        }

        private void Play() {
            active = true;
        }

        private void Reset() {
            active = false;
        }


        public void Upd(float deltaTime) {
            if (!active) return;

            // Update Player
            State.player.Upd(deltaTime);

            // Update Enemies
            State.ufosPool.ForEachActive(UpdEntity);
            foreach (AsteroidPool asteroidPool in State.asteroidPools.Values)
                asteroidPool.ForEachActive(UpdEntity);

            // Update Ammo
            State.ammo1Pool.ForEachActive(UpdEntity);
            State.ammo2Pool.ForEachActive(UpdEntity);

            return;
            void UpdEntity(IEntity entity) => entity.Upd(deltaTime);
        }

    }
}