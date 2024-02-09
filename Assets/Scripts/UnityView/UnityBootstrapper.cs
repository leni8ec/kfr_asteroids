using Control;
using Model.Core.Adapters;
using Model.Core.Data;
using Model.Core.Data.State.Base;
using UnityEngine;
using UnityView.Data;
using UnityView.Handlers;

namespace UnityView {

    /// <summary>
    /// Base class - don't place it to unity scene!
    /// </summary>
    public class UnityBootstrapper : MonoBehaviour {
        [Space]
        [SerializeField] private SceneData sceneData;

        private Bootstrapper bootstrapper;

        private void Awake() {
            // Data - init first
            StatesCollector states = new();
            DataCollector data = new(states, sceneData.configsCollector);
            AdaptersCollector adaptersCollector = sceneData.adaptersCollectorUnity.CreateCollector();

            // Fill objects state
            sceneData.SetGameData(states);

            // Entities
            UnityEntitiesCreateHandler unityEntitiesCreateHandler = new(sceneData.prefabsCollector);

            // Bootstrapper
            bootstrapper = new Bootstrapper(data, adaptersCollector);
        }

        private void Update() {
            bootstrapper.Upd(Time.deltaTime);
        }

    }
}