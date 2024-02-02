using Core.Unity.Helpers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Unity {
    [DefaultExecutionOrder(-100)]
    public class SceneData : MonoBehaviourHandler<SceneData> {
        public Camera mainCamera;
        [FormerlySerializedAs("dataCollector")]
        public ConfigCollector configCollector;
        public PrefabCollector prefabCollector;
    }
}