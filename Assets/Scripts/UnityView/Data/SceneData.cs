using Model.Core.Data.Collectors;
using UnityEngine;

namespace UnityView.Data {
    [DefaultExecutionOrder(-100)]
    public class SceneData : MonoBehaviour {
        public static SceneData self;

        [Header("Collectors")]
        public UnityConfigCollector config;
        public UnityPrefabCollector prefab;
        public UnityAdaptersCollector adapter;


        public StateCollector States { get; private set; }

        private void Awake() {
            self = this;
        }

        public void SetGameData(StateCollector states) {
            States = states;
        }

    }
}