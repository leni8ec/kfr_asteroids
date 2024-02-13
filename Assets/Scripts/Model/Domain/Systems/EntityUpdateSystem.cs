using JetBrains.Annotations;
using Model.Core.Data.State;
using Model.Core.Interface.Entity;
using Model.Core.Pools;
using Model.Domain.Systems.Base;

namespace Model.Domain.Systems {
    [UsedImplicitly]
    public class EntityUpdateSystem : SystemBase, IUpdateSystem {
        private EntitiesState State { get; }

        public EntityUpdateSystem(EntitiesState entities) {
            State = entities;

            // Game state events
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