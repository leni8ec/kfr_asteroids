using System;
using Control.Collectors;
using Control.Collectors.Base;
using Control.Services.DI.Ioc;
using Model.Domain.Systems;
using Model.Domain.Systems.Interface;

namespace Control.Starter {
    public class DependencyRegistry {
        private IDependencyContainer Container { get; }

        public DependencyRegistry(IDependencyContainer container) {
            Container = container;
        }

        public void RegisterDependencies(GameDataContainer gameData, AdaptersCollector adapters) {
            RegisterCollector(gameData.Configs);
            RegisterCollector(gameData.States);
            RegisterCollector(adapters);
            RegisterSystems();
        }

        private void RegisterSystems() {
            // Resolve Systems            Order of initialization:
            Container.Register<IEntitiesSystem, EntitiesSystem>();
            Container.Register<IPlayerSystem, PlayerSystem>();
            Container.Register<IWeaponSystem, WeaponSystem>();
            Container.Register<IEnemiesSystem, EnemiesSystem>();
            Container.Register<IInfinityScreenSystem, InfinityScreenSystem>();
            Container.Register<IEntityUpdateSystem, EntityUpdateSystem>();
            Container.Register<ICollisionSystem, CollisionSystem>();
            Container.Register<IScoreSystem, ScoreSystem>();
            Container.Register<IAudioSystem, AudioSystem>();
            Container.Register<IGameStateSystem, GameStateSystem>();
        }

        private void RegisterCollector<T>(CollectorBase<T> collector) {
            foreach ((Type type, T value) in collector.Objects)
                Container.Register(type, value);

            // Add ObjectPointers
            if (!collector.IsPointersExists) return;
            foreach ((object type, object value) in collector.Pointers) {
                Container.Register((Type) type, value);
            }
        }

    }
}