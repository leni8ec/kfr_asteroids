using System;
using Model.Core.Adapters;
using Model.Core.Container.Ioc;
using Model.Core.Container.Object;
using Model.Core.Data;
using Model.Domain.Systems;
using Model.Domain.Systems.Interface;

namespace Control {
    public class DependencyRegistrar {
        private DependencyContainer Container { get; }

        public DependencyRegistrar(DependencyContainer container) {
            Container = container;
        }

        public void RegisterDependencies(GameDataCollector gameData, AdaptersCollector adapters) {
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
            Container.Register<IWorldSystem, WorldSystem>();
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