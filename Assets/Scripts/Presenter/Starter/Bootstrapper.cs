using Core.Framework.Systems.Cluster;
using Presenter.Collectors;
using Presenter.Services.DI;

namespace Presenter.Starter {
    public class Bootstrapper {
        private readonly ISystemsCluster mainSystemsCluster;

        public Bootstrapper(GameDataContainer gameData, AdaptersCollector adapters) {
            IDependencyContainer dependencyContainer = new DependencyContainer();

            new DependencyRegistry(dependencyContainer)
                .RegisterDependencies(gameData, adapters);

            new SystemsRegistry(dependencyContainer)
                .ObtainSystems(mainSystemsCluster = new SystemsCluster());

            Init();
        }

        private void Init() {
            mainSystemsCluster.Initialization();
        }

        public void Upd(float deltaTime) {
            mainSystemsCluster.Upd(deltaTime);
        }

    }
}