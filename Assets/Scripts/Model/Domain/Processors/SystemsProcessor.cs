using System;
using System.Collections.Generic;
using Model.Core.Adapters;
using Model.Core.Data;
using Model.Domain.Systems;
using Model.Domain.Systems.Base;

namespace Model.Domain.Processors {
    public class SystemsProcessor {
        // ReSharper disable once CollectionNeverQueried.Local
        private readonly Dictionary<Type, ISystem> systems; // todo: DI container
        private readonly List<IUpdateSystem> updateSystems;

        public SystemsProcessor(DataCollector data, AdaptersCollector adapters) {
            systems = new Dictionary<Type, ISystem>();
            updateSystems = new List<IUpdateSystem>();

            // Systems and processors                     //        Order of initialization:
            Add(new PlayerSystem(data, adapters)); //        1. Player (player control)
            Add(new WeaponSystem(data, adapters)); //        2. Weapon (spawn ammo)
            Add(new WorldSystem(data, adapters)); //         3. World (spawn enemies)
            Add(new EntityUpdateSystem(data, adapters)); //  4. Entities update
            Add(new CollisionSystem(data, adapters)); //     6. Collision
            Add(new ScoreSystem(data, adapters));
            Add(new AudioSystem(data, adapters));
            Add(new GameStateSystem(data, adapters)); //     [Last] NewGame event
        }

        private void Add<T>(T system) where T : ISystem {
            // Fill systems Hashmap
            systems.Add(typeof(T), system);

            // Fill update systems
            if (system is IUpdateSystem updateSystem) updateSystems.Add(updateSystem);
        }

        public void Upd(float deltaTime) {
            foreach (IUpdateSystem updateSystem in updateSystems) {
                if (updateSystem.Active) updateSystem.Upd(deltaTime);
            }
        }
    }
}