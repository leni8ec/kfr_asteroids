using Presentation.Unity;
using UnityEngine;

namespace Presentation.GUI {
    [DefaultExecutionOrder(-100)]
    public class GuiController : MonoBehaviourHandler<GuiController> {
        public DataCollector dataCollector;
        public PrefabCollector prefabCollector;
    }
}