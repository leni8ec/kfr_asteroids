using Model.Core.Adapters;
using Model.Core.Data;
using Model.Domain.Processors;

namespace Control {
    public class Bootstrapper {
        private readonly SystemsProcessor systemsProcessor;

        public Bootstrapper(DataCollector data, AdaptersCollector adapters) {
            systemsProcessor = new SystemsProcessor(data, adapters);
        }

        public void Upd(float deltaTime) {
            systemsProcessor.Upd(deltaTime);
        }

    }
}