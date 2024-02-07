using System;
using System.Collections.Generic;
using Core.State;
using Core.Unity;
using Domain.Base;
using Domain.Systems;

namespace Domain.Processors {
    public class SystemsProcessor {
        // ReSharper disable once CollectionNeverQueried.Local
        private readonly Dictionary<Type, ISystem> systems; // todo: DI container
        private readonly List<IUpdateSystem> updateSystems;

        public SystemsProcessor(StateCollector state, ConfigCollector config, PrefabCollector prefab) {
            systems = new Dictionary<Type, ISystem>();
            updateSystems = new List<IUpdateSystem>();

            // Systems and processors                     //        Order of initialization:
            Add(new PlayerSystem(state, config, prefab)); //        1. Player (player control)
            Add(new WeaponSystem(state, config, prefab)); //        2. Weapon (spawn ammo)
            Add(new WorldSystem(state, config, prefab)); //         3. World (spawn enemies)
            Add(new ObjectsUpdateSystem(state, config, prefab)); // 4. Objects update
            Add(new CollisionSystem(state, config, prefab)); //     6. Collision
            Add(new ScoreSystem(state, config, prefab));
            Add(new AudioSystem(state, config, prefab));
            Add(new GameStateSystem(state, config, prefab)); //     [Last] NewGame event
        }

        private void Add<T>(T system) where T : ISystem {
            // Fill systems Hashmap
            systems.Add(typeof(T), system);

            // Fill update systems
            if (system is IUpdateSystem updateSystem) updateSystems.Add(updateSystem);
        }

        public void Upd(float deltaTime) {
            foreach (IUpdateSystem updateSystem in updateSystems) {
                updateSystem.Upd(deltaTime);
            }
        }
    }
}