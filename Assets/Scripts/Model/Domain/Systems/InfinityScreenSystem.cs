using JetBrains.Annotations;
using Model.Core.Adapters;
using Model.Core.Data.EntityPool;
using Model.Core.Data.State;
using Model.Core.Data.Unity.Config;
using Model.Core.Entity;
using Model.Core.Entity.Base;
using Model.Domain.Systems.Base;
using Model.Domain.Systems.Interface;
using UnityEngine;

namespace Model.Domain.Systems {
    [UsedImplicitly]
    public class InfinityScreenSystem : SystemBase, IInfinityScreenSystem, IUpdateSystem {
        private WorldConfig WorldConfig { get; }
        private ActiveEntitiesState Entities { get; }
        private ICameraAdapter Camera { get; }

        public InfinityScreenSystem(WorldConfig worldConfig, ActiveEntitiesState activeEntities, ICameraAdapter cameraAdapter) {
            WorldConfig = worldConfig;

            Entities = activeEntities;
            Camera = cameraAdapter;
        }

        public void Upd(float deltaTime) {
            ProcessInfinityScreen();
        }

        private void ProcessInfinityScreen() {
            Rect worldBorders = Camera.GetWorldLimits(WorldConfig.screenInfinityOutsideOffset);

            // Player
            ProcessEntityOutOfScreen(worldBorders, Entities.player);

            // Enemies
            Entities.ufos.ForEachSave(ProcessEntity);
            foreach (IEntitiesList<Asteroid> activeAsteroids in Entities.asteroidsDict.Values)
                activeAsteroids.ForEachSave(ProcessEntity);

            // Bullets
            Entities.ammo1.ForEachSave(ProcessEntity);

            return;
            void ProcessEntity(EntityBase entity) => ProcessEntityOutOfScreen(worldBorders, entity);
        }

        private void ProcessEntityOutOfScreen(Rect worldBorders, EntityBase entity) {
            Vector3 pos = entity.Transform.position;
            if (worldBorders.Contains(pos)) return;

            if (pos.x < worldBorders.x) pos.x = worldBorders.xMax;
            else if (pos.y < worldBorders.y) pos.y = worldBorders.yMax;
            else if (pos.x > worldBorders.xMax) pos.x = worldBorders.x;
            else if (pos.y > worldBorders.yMax) pos.y = worldBorders.y;

            entity.Transform.position = pos;
        }


    }
}