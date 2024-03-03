﻿using Control.Collectors;
using Control.Services.DI.Ioc;
using Model.Domain.Processors;

namespace Control.Starter {
    public class Bootstrapper {
        private readonly ISystemsProcessor systemsProcessor;

        public Bootstrapper(GameDataContainer gameData, AdaptersCollector adapters) {
            IDependencyContainer dependencyContainer = new DependencyContainer();
            DependencyRegistry dependencyRegistry = new(dependencyContainer);
            dependencyRegistry.RegisterDependencies(gameData, adapters);

            systemsProcessor = new SystemsProcessor();
            
            SystemsRegistry systemsRegistry = new(systemsProcessor, dependencyContainer);
            systemsRegistry.AddSystems();

            systemsProcessor.Initialization();
        }

        public void Upd(float deltaTime) {
            systemsProcessor.Upd(deltaTime);
        }

    }
}