using Core.Processors;
using Presenter.Collectors;
using Presenter.Services.DI.Ioc;

namespace Presenter.Starter {
    public class Bootstrapper {
        private readonly ISystemsProcessor systemsProcessor;

        public Bootstrapper(GameDataContainer gameData, AdaptersCollector adapters) {
            IDependencyContainer dependencyContainer = new DependencyContainer();

            new DependencyRegistry(dependencyContainer)
                .RegisterDependencies(gameData, adapters);

            new SystemsRegistry(dependencyContainer)
                .ObtainSystems(systemsProcessor = new SystemsProcessor());

            Init();
        }

        private void Init() {
            systemsProcessor.Initialization();
        }

        public void Upd(float deltaTime) {
            systemsProcessor.Upd(deltaTime);
        }

    }
}