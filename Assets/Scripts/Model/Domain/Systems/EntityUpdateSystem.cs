using System.Collections.Generic;
using JetBrains.Annotations;
using Model.Core.Data.State;
using Model.Core.Entity;
using Model.Core.Interface.Entity;
using Model.Core.Pool;
using Model.Domain.Systems.Base;
using Model.Domain.Systems.Interface;

namespace Model.Domain.Systems {
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
            foreach (EntitiesList<Asteroid> asteroids in State.asteroidsDict.Values)
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