using System;
using System.Collections.Generic;
using Model.Core.Container.Ioc;
using Model.Domain.Systems.Base;
using Model.Domain.Systems.Interface;

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

            // Resolve Systems              Order of initialization:
            Add<IAudioSystem>(); //            -1. Audio System (temp hack for disable sounds on GameOver)
            Add<IEntitiesSystem>(); //          0. Entities managers
            Add<IPlayerSystem>(); //            1. Player (player control)
            Add<IWeaponSystem>(); //            2. Weapon (spawn ammo)
            Add<IEnemiesSystem>(); //           3. Enemies (spawn enemies)
            Add<IInfinityScreenSystem>(); //    4. Infinity screen
            Add<IEntityUpdateSystem>(); //      5. Entities update
            Add<ICollisionSystem>(); //         6. Collision
            Add<IScoreSystem>();
            Add<IGameStateSystem>(); //         [Last] NewGame event

            // Called after all systems constructors is called
            Initialization();
        }

        private void Initialization() {
            // Init systems
            foreach (ISystem system in systems.Values) system.Initialize();
        }

        private void Add<T>() where T : ISystem {
            ISystem system = container.Resolve<T>();

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