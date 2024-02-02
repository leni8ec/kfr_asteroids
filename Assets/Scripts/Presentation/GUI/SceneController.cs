using Presentation.Unity;
using UnityEngine;
using UnityEngine.Serialization;

namespace Presentation.GUI {
    [DefaultExecutionOrder(-100)]
    public class SceneController : MonoBehaviourHandler<SceneController> {
        public Camera mainCamera;
        [FormerlySerializedAs("dataCollector")]
        public ConfigCollector configCollector;
        public PrefabCollector prefabCollector;
    }
}