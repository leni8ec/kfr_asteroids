using Core.Framework.Systems;
using Core.Systems.Interface;
using JetBrains.Annotations;
using Model.Data.EntityPool;
using Model.Data.State;
using Model.Entity;
using Model.Entity.Interface;

namespace Core.Systems {
    [UsedImplicitly]
    public class EntityUpdateSystem : SystemBase, IEntityUpdateSystem, IUpdateSystem {
        private ActiveEntitiesState State { get; }

        public EntityUpdateSystem(ActiveEntitiesState entities) {
            State = entities;

            // Game state events
            GameStateSystem.NewGameEvent += Enable;
            GameStateSystem.GameOverEvent += Disable;
        }

        public void Upd(float deltaTime) {
            // Update Player
            State.player.Upd(deltaTime);

            // Update Enemies
            State.ufos.ForEachSave(UpdEntity);
            foreach (EntityList<Asteroid> asteroids in State.asteroidsDict.Values)
                asteroids.ForEachSave(UpdEntity);

            // Update Ammo
            State.ammo1.ForEachSave(UpdEntity);
            State.ammo1.ForEachSave(UpdEntity);
            State.ammo2.ForEachSave(UpdEntity);

            return;
            void UpdEntity(IEntity entity) => entity.Upd(deltaTime);
        }

    }
}