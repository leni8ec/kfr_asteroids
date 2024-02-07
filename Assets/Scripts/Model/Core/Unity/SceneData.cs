using Model.Core.State;
using UnityEngine;

namespace Model.Core.Unity {
    [DefaultExecutionOrder(-100)]
    public class SceneData : MonoBehaviour {
        public static SceneData self;

        public Camera mainCamera;
        [Space]
        public ConfigCollector configCollector;
        public PrefabCollector prefabCollector;

        public StateCollector State { get; private set; }

        private void Awake() {
            self = this;
        }

        public void SetGameData(StateCollector state) {
            State = state;
        }

    }
}