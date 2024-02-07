using Control;
using Model.Core.State;
using Model.Core.Unity;
using UnityEngine;

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

            // Fill objects state
            states.objects.camera = sceneData.mainCamera;

            sceneData.SetGameData(states);
            // Bootstrapper
            bootstrapper = new Bootstrapper(states, sceneData.configCollector, sceneData.prefabCollector);
        }

        private void Update() {
            bootstrapper.Upd(Time.deltaTime);
        }

    }
}