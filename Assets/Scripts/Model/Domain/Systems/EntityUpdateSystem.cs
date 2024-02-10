using Model.Core.Adapters;
using Model.Core.Data;
using Model.Core.Data.State;
using Model.Core.Interface.Entity;
using Model.Core.Pools;
using Model.Domain.Systems.Base;

namespace Model.Domain.Systems {
    public class EntityUpdateSystem : SystemBase, IUpdateSystem {
        private EntitiesState State { get; }


        public EntityUpdateSystem(DataCollector data, AdaptersCollector adapters) {
            State = data.States.entity;

            // Game state
            GameStateSystem.NewGameEvent += Enable;
            GameStateSystem.GameOverEvent += Disable;
        }


        public void Upd(float deltaTime) {
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