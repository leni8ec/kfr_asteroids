using Core.State;
using UnityEngine;

namespace Core.Unity {

    /// <summary>
    /// note: Base class - don't place this to unity scene!
    /// </summary>
    public abstract class UnityBootstrapperBase : MonoBehaviour {
        protected SceneData sceneData;
        protected StateCollector state;

        protected virtual void Awake() {
            // Data - init first
            state = new StateCollector();
            sceneData = SceneData.Handler;
            sceneData.SetGameData(state);

            // Fill objects state
            state.objects.camera = sceneData.mainCamera;
        }

    }
}