using Presenter.Collectors;
using UnityEngine;

namespace UnityView.Collectors {
    // todo: it's a god object for EntityView (presentation layer)
    [DefaultExecutionOrder(-100)]
    public class SceneDataContainer : MonoBehaviour {
        public static SceneDataContainer self;

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