using Core.Systems.Base;
using Core.Systems.Interface;
using JetBrains.Annotations;
using Model.Adapters;
using Model.Data.EntityPool;
using Model.Data.State;
using Model.Data.Unity.Config;
using Model.Entity;
using Model.Entity.Base;
using UnityEngine;

namespace Core.Systems {
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
            Vector3 newPos = entity.Position;
            if (worldBorders.Contains(newPos)) return;

            if (newPos.x < worldBorders.x) newPos.x = worldBorders.xMax;
            else if (newPos.y < worldBorders.y) newPos.y = worldBorders.yMax;
            else if (newPos.x > worldBorders.xMax) newPos.x = worldBorders.x;
            else if (newPos.y > worldBorders.yMax) newPos.y = worldBorders.y;

            entity.Position = newPos;
        }


    }
}