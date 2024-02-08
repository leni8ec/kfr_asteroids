using Control;
using Model.Core.Adapters;
using Model.Core.Data;
using Model.Core.Data.State.Base;
using UnityEngine;
using UnityView.Data;

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
            DataCollector data = new(states, sceneData.configsCollector, sceneData.prefabsCollector);
            AdaptersCollector adaptersCollector = sceneData.adaptersCollectorUnity.CreateCollector();

            // Fill objects state
            sceneData.SetGameData(states);

            // Bootstrapper
            bootstrapper = new Bootstrapper(data, adaptersCollector);
        }

        private void Update() {
            bootstrapper.Upd(Time.deltaTime);
        }

    }
}