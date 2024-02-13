using Control;
using Model.Core.Adapters;
using Model.Core.Data;
using Model.Core.Data.Collectors;
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
            StateCollector states = new();
            GameDataCollector data = new(sceneData.config.CreateCollector(), states);
            AdaptersCollector adaptersCollector = sceneData.adapter.CreateCollector();

            // Fill entities state
            sceneData.SetGameData(states);

            // Entities
            UnityEntitiesCreateHandler unityEntitiesCreateHandler = new(sceneData.prefab);

            // Bootstrapper
            bootstrapper = new Bootstrapper(data, adaptersCollector);
        }

        private void Update() {
            bootstrapper.Upd(Time.deltaTime);
        }

    }
}