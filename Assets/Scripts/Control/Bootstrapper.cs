using Model.Core.Adapters;
using Model.Core.Container.Ioc;
using Model.Core.Data;
using Model.Domain.Processors;

namespace Control {
    public class Bootstrapper {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly DependencyContainer dependencyContainer;
        private readonly SystemsProcessor systemsProcessor;

        public Bootstrapper(GameDataCollector gameData, AdaptersCollector adapters) {
            dependencyContainer = new DependencyContainer();
            DependencyRegistrar dependencyRegistrar = new(dependencyContainer);
            dependencyRegistrar.RegisterDependencies(gameData, adapters);

            systemsProcessor = new SystemsProcessor(dependencyContainer);
        }

        public void Upd(float deltaTime) {
            systemsProcessor.Upd(deltaTime);
        }

    }
}