using Model.Core.Data.State.Base;
using Model.Core.Unity.Data;
using UnityEngine;

namespace UnityView.Data {
    [DefaultExecutionOrder(-100)]
    public class SceneData : MonoBehaviour {
        public static SceneData self;

        [Space]
        public ConfigsCollector configsCollector;
        public PrefabsCollector prefabsCollector;
        public AdaptersCollectorUnity adaptersCollectorUnity;


        public StatesCollector States { get; private set; }

        private void Awake() {
            self = this;
        }

        public void SetGameData(StatesCollector states) {
            States = states;
        }

    }
}