using System;
using Model.Core.Adapters;
using Model.Core.Container.Ioc;
using Model.Core.Container.Object;
using Model.Core.Data;

namespace Control {
    public class DependencyRegistrar {
        public DependencyContainer Container { get; }

        public DependencyRegistrar(DependencyContainer container) {
            Container = container;
        }

        public void RegisterDependencies(GameDataCollector gameData, AdaptersCollector adapters) {
            RegisterCollector(gameData.Configs);
            RegisterCollector(gameData.States);
            RegisterCollector(adapters);
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