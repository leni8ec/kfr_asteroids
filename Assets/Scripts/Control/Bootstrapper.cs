using Model.Core.State;
using Model.Core.Unity;
using Model.Domain.Processors;

namespace Control {
    public class Bootstrapper {
        private readonly SystemsProcessor systemsProcessor;

        public Bootstrapper( StateCollector states, ConfigCollector configs, PrefabCollector prefabs) {
            systemsProcessor = new SystemsProcessor(states, configs, prefabs);
        }

        public void Upd(float deltaTime) {
            systemsProcessor.Upd(deltaTime);
        }

    }
}