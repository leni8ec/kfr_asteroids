using Core.State;
using UnityEngine;

namespace Core.Unity {

    /// <summary>
    /// Base class - don't place it to unity scene!
    /// </summary>
    public abstract class UnityBootstrapperBase : MonoBehaviour {
        protected SceneData sceneData;
        protected StateCollector states;

        protected virtual void Awake() {
            // Data - init first
            states = new StateCollector();
            sceneData = SceneData.Handler;
            sceneData.SetGameData(states);

            // Fill objects state
            states.objects.camera = sceneData.mainCamera;
        }

    }
}