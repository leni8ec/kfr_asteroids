using Core.Unity;
using Domain.Processors;
using UnityEngine;

namespace Domain {
    public class MainBootstrapper : UnityBootstrapperBase {
        private SystemsProcessor systemsProcessor;

        protected override void Awake() {
            base.Awake();
            systemsProcessor = new SystemsProcessor(states, sceneData.configCollector, sceneData.prefabCollector);
        }

        private void Update() {
            systemsProcessor.Upd(Time.deltaTime);
        }

    }
}