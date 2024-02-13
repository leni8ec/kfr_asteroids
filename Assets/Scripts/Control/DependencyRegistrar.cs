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
            foreach (T collectorValue in collector.Values) {
                Container.RegisterByInstanceType(collectorValue);
            }
            // Add pointer objects
            if (collector.Pointers.Count == 0) return;
            foreach (IObjectPointers pointerObjectsValue in collector.Pointers) {
                Container.RegisterByInstanceType(pointerObjectsValue);
            }
        }

    }
}