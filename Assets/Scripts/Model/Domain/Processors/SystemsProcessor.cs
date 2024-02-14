using System;
using System.Collections.Generic;
using Model.Core.Container.Ioc;
using Model.Domain.Systems;
using Model.Domain.Systems.Base;

namespace Model.Domain.Processors {
    public class SystemsProcessor {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly DependencyContainer container;
        private readonly Dictionary<Type, ISystem> systems;
        private readonly List<IUpdateSystem> updateSystems;

        public SystemsProcessor(DependencyContainer container) {
            this.container = container;

            systems = new Dictionary<Type, ISystem>();
            updateSystems = new List<IUpdateSystem>();

            // Resolve Systems           Order of initialization:
            Add<EntityPoolSystem>(); //    0. Pools (for entities)
            Add<PlayerSystem>(); //        1. Player (player control)
            Add<WeaponSystem>(); //        2. Weapon (spawn ammo)
            Add<WorldSystem>(); //         3. World (spawn enemies)
            Add<EntityUpdateSystem>(); //  4. Entities update
            Add<CollisionSystem>(); //     5. Collision
            Add<ScoreSystem>();
            Add<AudioSystem>();
            Add<GameStateSystem>(); //     [Last] NewGame event

            // Called after all systems constructors is called
            Initialization();
        }

        private void Initialization() {
            // Init systems
            foreach (ISystem system in systems.Values) system.Initialize();
        }

        private void Add<T>() where T : ISystem {
            ISystem system = container.ResolveUnregistered<T>();

            // Fill systems
            systems.Add(typeof(T), system);
            // Fill update
            if (system is IUpdateSystem updateSystem) updateSystems.Add(updateSystem);
        }

        public void Upd(float deltaTime) {
            foreach (IUpdateSystem updateSystem in updateSystems) {
                if (updateSystem.Active) updateSystem.Upd(deltaTime);
            }
        }

    }
}